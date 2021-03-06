﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using HAP.Web.Configuration;
using System.Web;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using HAP.BookingSystem;
using System.Xml;
using System.Web.Security;

namespace HAP.BookingSystem
{
    [ServiceAPI("api/bookingsystem")]
    [ServiceContract(Namespace = "HAP.Web.API")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class API
    {
        [OperationContract]
        [WebInvoke(Method="POST", UriTemplate="/Search", BodyStyle=WebMessageBodyStyle.WrappedRequest, RequestFormat=WebMessageFormat.Json, ResponseFormat=WebMessageFormat.Json)]
        public JSONBooking[] Search(string Query)
        {
            List<JSONBooking> bookings = new List<JSONBooking>();
            XmlDocument doc = HAP.BookingSystem.BookingSystem.BookingsDoc;
            foreach (XmlNode n in doc.SelectNodes("/Bookings/Booking"))
            {
                Booking b = new Booking(n, false);
                if(b.Date < DateTime.Today )
                    continue;
                if (b.Username.ToLower().StartsWith(Query.ToLower()))
                    bookings.Add(new JSONBooking(b));
            }
            foreach (Booking sb in HAP.BookingSystem.BookingSystem.StaticBookings.Values)
                if (sb.Username.ToLower().StartsWith(Query.ToLower()))
                    bookings.Add(new JSONBooking(sb));
            bookings.Sort();
            return bookings.ToArray();
        }

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/Booking/{Date}/{i}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public JSONBooking[][] RemoveBooking(string Date, JSONBooking booking, string i)
        {
            HAP.Data.SQL.WebEvents.Log(DateTime.Now, "BookingSystem.Remove", ((HAP.AD.User)Membership.GetUser()).UserName, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Browser.Platform, HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version, HttpContext.Current.Request.UserHostName, "Removing " + booking.Name);
            HAP.BookingSystem.BookingSystem bs = new HAP.BookingSystem.BookingSystem(DateTime.Parse(Date));
            Booking b = bs.getBooking(booking.Room, booking.Lesson)[int.Parse(i)];
            try
            {
                BookingRules.Execute(b, hapConfig.Current.BookingSystem.Resources[b.Room], bs, BookingRuleType.Booking, true);
            }
            catch (Exception ex) { HAP.Web.Logging.EventViewer.Log("Booking System JSON API", ex.ToString() + "\nMessage:\n" + ex.Message + "\nStack Trace:\n" + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error); }
            try
            {
                if (!string.IsNullOrEmpty(b.uid) && hapConfig.Current.SMTP.Enabled)
                {
                    iCalGenerator.GenerateCancel(b, DateTime.Parse(Date));
                    if (hapConfig.Current.BookingSystem.Resources[booking.Room].EmailAdmins) iCalGenerator.GenerateCancel(b, DateTime.Parse(Date), true);
                }
            }
            catch (Exception ex) { HAP.Web.Logging.EventViewer.Log("Booking System JSON API", ex.ToString() + "\nMessage:\n" + ex.Message + "\nStack Trace:\n" + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error); }
            XmlDocument doc = HAP.BookingSystem.BookingSystem.BookingsDoc;
            XmlNodeList nodes = doc.SelectNodes("/Bookings/Booking[@date='" + DateTime.Parse(Date).ToShortDateString() + "' and @lesson[contains(., '" + booking.Lesson + "')] and @room='" + booking.Room + "']");
            doc.SelectSingleNode("/Bookings").RemoveChild(nodes[int.Parse(i)]);
            HAP.BookingSystem.BookingSystem.BookingsDoc = doc;
            return LoadRoom(Date, booking.Room);
        }

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Booking/{Date}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public JSONBooking[][] Book(string Date, JSONBooking booking)
        {
            HAP.Data.SQL.WebEvents.Log(DateTime.Now, "BookingSystem.Book", ((HAP.AD.User)Membership.GetUser()).UserName, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Browser.Platform, HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version, HttpContext.Current.Request.UserHostName, "Booking " + booking.Name);
            try
            {
                HAP.BookingSystem.BookingSystem bs = new HAP.BookingSystem.BookingSystem(DateTime.Parse(Date));
                if (booking.Static) {
                    if (!bs.isStatic(booking.Room, booking.Lesson)) {
                        bs.addStaticBooking(new Booking { Name = booking.Name, Lesson = booking.Lesson, Username = booking.Username, Room = booking.Room, Day = bs.DayNumber });
                    } else {
                        throw new Exception("Static Booking Already Exists for period " + booking.Lesson + " with resource " + booking.Room);
                    }
                }
                else
                {
                    hapConfig config = hapConfig.Current;

                    if (!config.BookingSystem.Resources[booking.Room].CanShare)
                    {
                        // if bookings can't be shared, need to check here that booking doesn't already exist
                        Booking[] existing = bs.getBooking(booking.Room, booking.Lesson);
                        if( existing[0] != null && !existing[0].Static && existing[0].Name != "FREE" ) {
                            throw new Exception("Booking Already Exists for period " + booking.Lesson + " with resource " + booking.Room);
                        }
                    }

                    XmlDocument doc = HAP.BookingSystem.BookingSystem.BookingsDoc;
                    XmlElement node = doc.CreateElement("Booking");
                    node.SetAttribute("date", DateTime.Parse(Date).ToShortDateString());
                    node.SetAttribute("lesson", booking.Lesson);
                    if (config.BookingSystem.Resources[booking.Room].Type == ResourceType.Laptops)
                    {
                        node.SetAttribute("ltroom", booking.LTRoom);
                        node.SetAttribute("ltheadphones", booking.LTHeadPhones.ToString());
                    }
                    else if (config.BookingSystem.Resources[booking.Room].Type == ResourceType.Equipment || config.BookingSystem.Resources[booking.Room].Type == ResourceType.Loan)
                        node.SetAttribute("equiproom", booking.EquipRoom);
                    node.SetAttribute("room", booking.Room);
                    node.SetAttribute("uid", booking.Username + DateTime.Now.ToString(iCalGenerator.DateFormat));
                    node.SetAttribute("username", booking.Username);
                    node.SetAttribute("count", booking.Count.ToString());
                    node.SetAttribute("name", booking.Name);
                    if (booking.Count >= 0) node.SetAttribute("count", booking.Count.ToString());
                    if (!string.IsNullOrWhiteSpace(booking.Notes)) node.SetAttribute("notes", booking.Notes);
                    doc.SelectSingleNode("/Bookings").AppendChild(node);
                    HAP.BookingSystem.BookingSystem.BookingsDoc = doc;
                    Booking[] b1 = new HAP.BookingSystem.BookingSystem(DateTime.Parse(Date)).getBooking(booking.Room, booking.Lesson);
                    Booking b = b1[b1.Length - 1];
                    if (config.SMTP.Enabled)
                    {
                        iCalGenerator.Generate(b, DateTime.Parse(Date));
                        if (config.BookingSystem.Resources[b.Room].EmailAdmins) iCalGenerator.Generate(b, DateTime.Parse(Date), true);
                    }
                    BookingRules.Execute(b, config.BookingSystem.Resources[b.Room], new HAP.BookingSystem.BookingSystem(DateTime.Parse(Date)), BookingRuleType.Booking, false);
                }
            }
            catch (Exception e)
            {
                HAP.Web.Logging.EventViewer.Log(HttpContext.Current.Request.RawUrl, e.ToString() + "\nMessage:\n" + e.Message + "\n\nStack Trace:\n" + e.StackTrace, System.Diagnostics.EventLogEntryType.Error);
            }
            return LoadRoom(Date, booking.Room);
        }

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Return/{Date}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public JSONBooking[][] Return(string Date, string Resource, string lesson)
        {
            hapConfig config = hapConfig.Current;
            HAP.BookingSystem.BookingSystem bs = new HAP.BookingSystem.BookingSystem(DateTime.Parse(Date)); 
            Booking b = new HAP.BookingSystem.BookingSystem(DateTime.Parse(Date)).getBooking(Resource, lesson)[0];

            XmlDocument doc = HAP.BookingSystem.BookingSystem.BookingsDoc;
            List<string> newlesson = new List<string>(); bool go = false;
            foreach (Lesson l in config.BookingSystem.Lessons) {
                if (b.Lesson.StartsWith(l.Name)) go = true;
                if (go) newlesson.Add(l.Name);
                if (l.Name == lesson) go = false;
            }
            doc.SelectSingleNode("/Bookings/Booking[@date='" + DateTime.Parse(Date).ToShortDateString() + "' and @lesson[contains(., '" + lesson + "')] and @room='" + Resource + "']").Attributes["lesson"].Value = string.Join(", ", newlesson.ToArray());
            b.Lesson = string.Join(", ", newlesson.ToArray()); // update b.Lesson to reflect what just went into the XML above (ready for booking rules)

            HAP.BookingSystem.BookingSystem.BookingsDoc = doc;
            try
            {
                BookingRules.Execute(b, hapConfig.Current.BookingSystem.Resources[b.Room], bs, BookingRuleType.Return, false);
            }
            catch (Exception ex) { HAP.Web.Logging.EventViewer.Log("Booking System JSON API", ex.ToString() + "\nMessage:\n" + ex.Message + "\nStack Trace:\n" + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error); }

            if (config.SMTP.Enabled)
            {
                iCalGenerator.GenerateCancel(b, DateTime.Parse(Date));
                if (config.BookingSystem.Resources[b.Room].EmailAdmins) iCalGenerator.GenerateCancel(b, DateTime.Parse(Date), true);
            }
            return LoadRoom(Date, Resource);
        }

        [OperationContract]
        [WebGet(ResponseFormat=WebMessageFormat.Json, UriTemplate="/LoadRoom/{Date}/{Resource}")]
        public JSONBooking[][] LoadRoom(string Date, string Resource)
        {
            Resource = HttpUtility.UrlDecode(Resource, System.Text.Encoding.Default).Replace("%20", " ");
            List<JSONBooking[]> bookings = new List<JSONBooking[]>();
            HAP.BookingSystem.BookingSystem bs = new HAP.BookingSystem.BookingSystem(DateTime.Parse(Date));
            foreach (Lesson lesson in hapConfig.Current.BookingSystem.Lessons)
            {
                List<JSONBooking> js = new List<JSONBooking>();
                foreach (Booking b in bs.getBooking(Resource, lesson.Name))
                {
                    object result = null;
                    JSONBooking j = null;
                    if (b.Name == "FREE")
                    {
                        result = BookingRules.Execute(b, hapConfig.Current.BookingSystem.Resources[Resource], bs, BookingRuleType.PreProcess, false);
                        if (result != null)
                        {
                            j = new JSONBooking((Booking)result);
                        }
                    }
                    
                    if (j == null)
                        j = new JSONBooking(b);
                    
                    if (b.Resource.Type == ResourceType.Loan) j.Lesson = lesson.Name;

                    if (b.Name == "CHARGING" || b.Name == "UNAVAILABLE")
                    {
                        js.Add(j);
                        break;
                    }                    
                    js.Add(j);
                }
                bookings.Add(js.ToArray());
            }
            WebOperationContext.Current.OutgoingResponse.Format = WebMessageFormat.Json;
            return bookings.ToArray();
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Load/{StartDate}/{EndDate}")]
        public JSONBooking[][] Load(string StartDate, string EndDate)
        {
            List<JSONBooking[]> bookings = new List<JSONBooking[]>();
            DateTime start = DateTime.Parse(StartDate);
            DateTime end = DateTime.Parse(EndDate);
            foreach (DateTime day in EachDay(start, end)) {
                HAP.BookingSystem.BookingSystem bs = new HAP.BookingSystem.BookingSystem(day);
                List<JSONBooking> js = new List<JSONBooking>();
                foreach (Lesson lesson in hapConfig.Current.BookingSystem.Lessons)
                {
                    DateTime d = day.Date.AddHours(lesson.StartTime.Hour).AddMinutes(lesson.StartTime.Minute).AddSeconds(lesson.StartTime.Second);
                    DateTime d2 = day.Date.AddHours(lesson.EndTime.Hour).AddMinutes(lesson.EndTime.Minute).AddSeconds(lesson.EndTime.Second);
                    int a = 0;
                    foreach (Resource r in hapConfig.Current.BookingSystem.Resources.Values)
                    {
                        if (isVisible(r.ShowTo, r.HideFrom))
                            foreach (Booking b in bs.getBooking(r.Name, lesson.Name))
                            {
                                JSONBooking j = new JSONBooking(b);
                                j.Date = d.AddSeconds(a).ToString("yyyy-MM-ddTHH:mm:ssZ");
                                j.Date2 = d2.ToString("yyyy-MM-ddTHH:mm:ssZ");
                                if (b.Resource.Type == ResourceType.Loan) j.Lesson = lesson.Name;
                                if (b.Name == "CHARGING" || b.Name == "UNAVAILABLE")
                                {
                                    js.Add(j);
                                    break;
                                } 
                                js.Add(j);
                            }
                        a++;
                    }
                }
                bookings.Add(js.ToArray());
            }
            WebOperationContext.Current.OutgoingResponse.Format = WebMessageFormat.Json;
            return bookings.ToArray();
        }

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Load/{StartDate}/{EndDate}/{Resource}")]
        public JSONBooking[][] LoadResource(string StartDate, string EndDate, string Resource)
        {
            List<JSONBooking[]> bookings = new List<JSONBooking[]>();
            DateTime start = DateTime.Parse(StartDate);
            DateTime end = DateTime.Parse(EndDate);
            foreach (DateTime day in EachDay(start, end))
            {
                HAP.BookingSystem.BookingSystem bs = new HAP.BookingSystem.BookingSystem(day);
                List<JSONBooking> js = new List<JSONBooking>();
                foreach (Lesson lesson in hapConfig.Current.BookingSystem.Lessons)
                {
                    DateTime d = day.Date.AddHours(lesson.StartTime.Hour).AddMinutes(lesson.StartTime.Minute).AddSeconds(lesson.StartTime.Second);
                    DateTime d2 = day.Date.AddHours(lesson.EndTime.Hour).AddMinutes(lesson.EndTime.Minute).AddSeconds(lesson.EndTime.Second);
                    foreach (Booking b in bs.getBooking(Resource, lesson.Name))
                    {
                        JSONBooking j = new JSONBooking(b);
                        j.Date = d.ToString("yyyy-MM-ddTHH:mm:ssZ");
                        j.Date2 = d2.ToString("yyyy-MM-ddTHH:mm:ssZ");
                        if (b.Resource.Type == ResourceType.Loan) j.Lesson = lesson.Name;
                        js.Add(j);
                    }
                }
                bookings.Add(js.ToArray());
            }
            WebOperationContext.Current.OutgoingResponse.Format = WebMessageFormat.Json;
            return bookings.ToArray();
        }

        [OperationContract]
        [WebGet(ResponseFormat=WebMessageFormat.Json, UriTemplate="/Initial/{Date}/{Username}")]
        public int[] Initial(string Username, string Date)
        {
            if (!isAdmin(Username))
            {
                XmlDocument doc = HAP.BookingSystem.BookingSystem.BookingsDoc;
                int max = hapConfig.Current.BookingSystem.MaxBookingsPerWeek;
                foreach (AdvancedBookingRight right in HAP.BookingSystem.BookingSystem.BookingRights)
                    if (right.Username.ToLower() == Username.ToLower())
                        max = right.Numperweek;
                int x = 0;
                foreach (DateTime d in getWeekDates(DateTime.Parse(Date)))
                    x += doc.SelectNodes("/Bookings/Booking[@date='" + d.ToShortDateString() + "' and @username='" + Username + "']").Count;
                return new int[] { max - x, new HAP.BookingSystem.BookingSystem(DateTime.Parse(Date)).WeekNumber };
            }
            WebOperationContext.Current.OutgoingResponse.Format = WebMessageFormat.Json;
            return new int[] { 0, new HAP.BookingSystem.BookingSystem(DateTime.Parse(Date)).WeekNumber };
        }

        private DateTime[] getWeekDates(DateTime Date)
        {
            List<DateTime> dates = new List<DateTime>();
            if (Date.DayOfWeek == DayOfWeek.Monday)
            {
                dates.Add(Date); dates.Add(Date.AddDays(1));
                dates.Add(Date.AddDays(2)); dates.Add(Date.AddDays(3));
                dates.Add(Date.AddDays(4));
            }
            else if (Date.DayOfWeek == DayOfWeek.Tuesday)
            {
                dates.Add(Date); dates.Add(Date.AddDays(1));
                dates.Add(Date.AddDays(2)); dates.Add(Date.AddDays(3));
                dates.Add(Date.AddDays(-1));
            }
            else if (Date.DayOfWeek == DayOfWeek.Wednesday)
            {
                dates.Add(Date); dates.Add(Date.AddDays(1));
                dates.Add(Date.AddDays(2)); dates.Add(Date.AddDays(-1));
                dates.Add(Date.AddDays(-2));
            }
            else if (Date.DayOfWeek == DayOfWeek.Tuesday)
            {
                dates.Add(Date); dates.Add(Date.AddDays(1));
                dates.Add(Date.AddDays(-1)); dates.Add(Date.AddDays(-2));
                dates.Add(Date.AddDays(-3));
            }
            else
            {
                dates.Add(Date); dates.Add(Date.AddDays(-1));
                dates.Add(Date.AddDays(-2)); dates.Add(Date.AddDays(-3));
                dates.Add(Date.AddDays(-4));
            }
            return dates.ToArray();
        }

        private bool isAdmin(string Username)
        {
            if (new HAP.AD.User(Username).IsMemberOf(GroupPrincipal.FindByIdentity(AD.ADUtils.GetPContext(), "Domain Admins"))) return true;
            return isBSAdmin(Username);
        }

        private bool isVisible(string showto, string hidefrom)
        {
            if (showto == "All" || isBSAdmin(((HAP.AD.User)Membership.GetUser()).UserName) || HttpContext.Current.User.IsInRole("Domain Admins")) return true;
            foreach (string s in hidefrom.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                if (((HAP.AD.User)Membership.GetUser()).UserName.ToLower().Equals(s.ToLower().Trim())) return false;
                else if (HttpContext.Current.User.IsInRole(s.Trim())) return false;
            foreach (string s in showto.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                if (((HAP.AD.User)Membership.GetUser()).UserName.ToLower().Equals(s.ToLower().Trim())) return true;
                else if (HttpContext.Current.User.IsInRole(s.Trim())) return true;
            return false;
        }

        private bool isBSAdmin(string Username)
        {
            foreach (string s in hapConfig.Current.BookingSystem.Admins.Split(new char[] { ',' }))
                if (s.Trim().ToLower().Equals(Username.ToLower())) return true;
                else if (HttpContext.Current.User.IsInRole(s.Trim())) return true;
            return false;
        }
    }
}
