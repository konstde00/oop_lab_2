using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Dementiev.Classes
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Author
    {
        public Author(string? name, string? surname, int age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }
        public string GetAllString()
        {
            string allData =
                Name + " " +
                Surname + " " +
                Age + " ";
            return allData;
        }

        public string? Name { get; private set; }

        public string? Surname { get; private set; }

        public int Age { get; private set; }


    }
}
