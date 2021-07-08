using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace tryWpfass
{
    [XmlRoot("ArrayOfPerson1")]
    public class Person1
    {
        [XmlElement("Person1")]
        public List<Person1> personList = new List<Person1>();

       
        [XmlElement("FName")]
        public string FName { get; set; }

        public string LName { get; set; }
        public string Compny { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Twitter { get; set; }
        public string IM { get; set; }
        public string Saveas { get; set; }
    
      
    }
}

