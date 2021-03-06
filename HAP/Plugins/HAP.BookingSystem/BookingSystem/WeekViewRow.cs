﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HAP.Web.Configuration;
using HAP.BookingSystem;

namespace HAP.Web.BookingSystem
{
    [DefaultProperty("Date")]
    [ToolboxData("<{0}:WeekViewRow runat=\"server\"></{0}:WeekViewRow>")]
    public class WeekViewRow : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        public DateTime Date { get; set; }

        public HtmlTextWriterTag Tag { get; set; }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.Write("<{1} onclick=\"location.href='./#{0}';\" title=\"Make a booking on this day ({0})\">", Date.ToShortDateString(), Tag.ToString().ToLower());
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            hapConfig config = hapConfig.Current;
            HAP.BookingSystem.BookingSystem bs = new HAP.BookingSystem.BookingSystem(Date);
            writer.WriteLine("<div class=\"lessoncol\" style=\"border-left-width: 1px;\">");
            writer.Write("<h2>");
            writer.Write("Lesson");
            writer.WriteLine("</h2>");
            bool alt = false;
            foreach (Lesson les in config.BookingSystem.Lessons)
            {
                writer.Write("<div{0}>", alt ? " class=\"alt\"" : "");
                writer.Write(les.Name);
                writer.WriteLine("</div>");
                alt = !alt;
            }
            writer.WriteLine("</div>");
            alt = false;
            foreach (Resource res in config.BookingSystem.Resources.Values)
            {
                string Room = res.Name;
                writer.WriteLine("<div class=\"lessoncol\">");
                writer.Write("<h2>");
                writer.Write(res.Name);
                writer.WriteLine("</h2>");
                foreach (Lesson lesson in config.BookingSystem.Lessons)
                    foreach (Booking b in bs.getBooking(Room, lesson.Name))
                    {
                        string lessonname = b.Name;
                        if (lessonname.Length > 17) lessonname = lessonname.Remove(17) + "...";
                        if (lessonname.Length > 16 && b.Static) lessonname = lessonname.Remove(14) + "...";
                        if (b.Name == "FREE") writer.Write("<div{0}>FREE</div>", alt ? " class=\"alt\"" : "");
                        else writer.Write("<div{2}><span>{0}<i>with {1}</i></span></div>", lessonname, b.User.Notes, alt ? " class=\"alt\"" : "");
                        alt = !alt;
                    }
                alt = false;
                writer.Write("</div>");
            }
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write("</{0}>", Tag.ToString().ToLower());
        }
    }
}
