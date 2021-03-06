﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.Security;
using HAP.Web.Configuration;

namespace HAP.Data.ComputerBrowser
{
    public class FileIcon
    {
        public string Icon { get; set; }
        public string Extension { get; set; }
        public string Type { get; set; }
        public string ContentType { get; set; }
        public static bool TryGet(string extension, out FileIcon icon)
        {
            if (extension.StartsWith(".")) extension = extension.Remove(0, 1);
            extension = extension.ToLower();
            XmlDocument doc = hapConfig.Current.MyFiles.KnownIcons;
            icon = null;
            if (doc.SelectSingleNode("/Icons/Icon[@extension='" + extension + "']") == null) return false;
            else
            {
                icon = new FileIcon();
                icon.Extension = extension;
                XmlNode n = doc.SelectSingleNode("/Icons/Icon[@extension='" + extension + "']");
                icon.ContentType = n.Attributes["contenttype"].Value;
                icon.Icon = n.Attributes["icon"].Value;
                icon.Type = n.Attributes["type"].Value;
                return true;
            }
        }
    }
}
