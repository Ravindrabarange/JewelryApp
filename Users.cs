using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JewelryApp
{
    [XmlRoot("Users")]
    public class Users
    {
        [XmlElement("crendential")]
        public List<crendential> Credential = new List<crendential>();


    }

    
    public class crendential
    {
        [XmlElement("username")]
        public string UserName { get; set; }

        [XmlElement("password")]
        public string Password { get; set; }
    }
}
