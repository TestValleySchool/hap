﻿using HAP.Web.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace HAP.BookingSystem
{
    public class BookingRules : List<BookingRule>
    {
        public BookingRules() : base()
        {
            if (!File.Exists(HttpContext.Current.Server.MapPath("~/app_data/bookingrules.xml")))
            {
                StreamWriter sw = File.CreateText(HttpContext.Current.Server.MapPath("~/app_data/bookingrules.xml"));
                sw.WriteLine("<?xml version=\"1.0\"?>");
                sw.WriteLine("<bookingrules />");
                sw.Close();
                sw.Dispose();
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("~/app_data/bookingrules.xml"));
            foreach (XmlNode node in doc.SelectNodes("/bookingrules/rule")) this.Add(new BookingRule(node));
        }

        public static void Execute(Booking b, Resource r, BookingSystem bs, bool IsRemoveEvent)
        {
            BookingRules br = new BookingRules();
            foreach (BookingRule rule in br) rule.ExecuteRule(b, r, bs, IsRemoveEvent);
        }
    }

    public class BookingRule
    {
        public BookingRule(XmlNode node)
        {
            
            this.Action1 = node.Attributes["action1"].Value;
            this.Action2 = node.Attributes["action2"].Value;
            List<BookingCondition> Conditions = new List<BookingCondition>();
            foreach (XmlNode n in node.ChildNodes)
                Conditions.Add(new BookingCondition(n));
        }
        private List<BookingCondition> Conditions;
        public string Action1 { get; private set; }
        public string Action2 { get; private set; }

        public void ExecuteRule(Booking b, Resource r, BookingSystem bs, bool IsRemoveEvent)
        {
            bool good = false;
            foreach (BookingCondition con in Conditions)
                switch (con.MasterOperation)
                {
                    case BookingConditionOperation.And:
                        good = good && con.IsConditionMet(b, r, bs);
                        break;
                    case BookingConditionOperation.Or:
                        good = good || con.IsConditionMet(b, r, bs);
                        break;
                }
            if (good)
            {
                HAP.Web.Logging.EventViewer.Log("BookingSystem.BookingRule", "TODO: Good Conditions for Booking Rule: " + Action1, System.Diagnostics.EventLogEntryType.Information);
            }
            else HAP.Web.Logging.EventViewer.Log("BookingSystem.BookingRule", "Failed Conditions for Booking Rule: " + Action1, System.Diagnostics.EventLogEntryType.Information);
        }


        
    }
    public enum BookingConditionOperation { And, Or, Not, Equals, Null, GT, LT, GTE, LTE, NotNull }

    public class BookingCondition
    {
        public BookingCondition(XmlNode node)
        {
            this.Condition1 = node.Attributes["condition1"].Value;
            this.Condition2 = node.Attributes["condition2"].Value;
            this.Operation = (BookingConditionOperation)Enum.Parse(typeof(BookingConditionOperation), node.Attributes["operation"].Value);
            this.MasterOperation = (BookingConditionOperation)Enum.Parse(typeof(BookingConditionOperation), node.Name);
        }

        public string Condition1 { get; private set; }
        public string Condition2 { get; private set; }
        public BookingConditionOperation Operation { get; private set; }
        public BookingConditionOperation MasterOperation { get; private set; }

        public bool IsConditionMet(Booking b, Resource r, BookingSystem bs)
        {
            object comp1 = processCondition(Condition1, b, r, bs), comp2 = processCondition(Condition1, b, r, bs);
            switch (Operation)
            {
                case BookingConditionOperation.Equals:
                    return comp1 == comp2;
                case BookingConditionOperation.GT:
                    if (comp1 is int)
                        return (int)comp1 > int.Parse(comp2.ToString());
                    else if (comp1 is DateTime)
                        return (DateTime)comp1 > DateTime.Parse(comp2.ToString());
                    return false;
                case BookingConditionOperation.GTE:
                    if (comp1 is int)
                        return (int)comp1 >= int.Parse(comp2.ToString());
                    else if (comp1 is DateTime)
                        return (DateTime)comp1 >= DateTime.Parse(comp2.ToString());
                    return false;
                case BookingConditionOperation.LT:
                    if (comp1 is int)
                        return (int)comp1 < int.Parse(comp2.ToString());
                    else if (comp1 is DateTime)
                        return (DateTime)comp1 < DateTime.Parse(comp2.ToString());
                    return false;
                case BookingConditionOperation.LTE:
                    if (comp1 is int)
                        return (int)comp1 <= int.Parse(comp2.ToString());
                    else if (comp1 is DateTime)
                        return (DateTime)comp1 <= DateTime.Parse(comp2.ToString());
                    return false;
                case BookingConditionOperation.Not:
                    return comp1 != comp2;
                case BookingConditionOperation.Null:
                    return comp1 == null;
                case BookingConditionOperation.NotNull:
                    return comp1 != null;
            }


            return false;
        }

        public object processCondition(string Condition, Booking b, Resource r, BookingSystem bs)
        {
            int x = 0; bool bo = false;
            if (Condition.ToLower().StartsWith("resource."))
                return processCondition(r, Condition.Remove(0, "resource.".Length));
            else if (Condition.ToLower().StartsWith("booking.")) return processCondition(r, Condition.Remove(0, "booking.".Length));
            else if (Condition.ToLower().StartsWith("bookingsystem.")) return processCondition(r, Condition.Remove(0, "bookingsystem.".Length));
            else if (Condition.ToLower().StartsWith("user.")) return processCondition(HttpContext.Current.User, Condition.Remove(0, "user.".Length));
            else if (int.TryParse(Condition, out x)) return x;
            else if (bool.TryParse(Condition, out bo)) return bo;
            else return Condition;
        }

        public object processCondition(object o, string Condition)
        {
            if (Condition == "") return o;
            string cons1 = Condition.IndexOf('.') == -1 ? Condition : Condition.Substring(0, Condition.IndexOf('.')).TrimEnd(new char[] { '.' });
            string cons2 = Condition.IndexOf('.') == -1 ? "" : Condition.Substring(Condition.IndexOf('.')).TrimStart(new char[] { '.' });
            if (cons1.Contains('('))
            {
                string[] conditions = cons1.Remove(0, cons1.IndexOf('(')).TrimStart(new char[] { '(' }).TrimEnd(new char[] { ')' }).Split(new char[] { ',' });
                cons1 = cons1.Substring(0, cons1.IndexOf('(')).TrimEnd(new char[] { '(' });

                List<object> objects = new List<object>();
                foreach (string s in conditions)
                {
                    int x = 0;
                    if (int.TryParse(s, out x)) objects.Add(x);
                    else objects.Add(s);
                }
                return processCondition(o.GetType().GetMethod(cons1).Invoke(o, objects.ToArray()), cons2);
            }
            else return processCondition(o.GetType().GetProperty(cons1).GetValue(o, null), cons2);
        }
    }
}
