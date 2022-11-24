using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Dementiev.Classes
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Service
    {
        public Service(string? title, string? description, string? type, string? version, Author? author, string? rules, string? info)
        {
            Title = title;
            Description = description;
            Type = type;
            Version = version;
            Author = author;
            Rules = rules;
            Info = info;
        }

        public string GetAllString()
        {
            string allData =
                Title + " " +
                Description + " " +
                Type + " " +
                Version + " " +
                Author!.GetAllString() + " " +
                Rules + " " +
                Info;
            return allData;
        }

        public string? Title { get; private set; }

        public string? Description { get; private set; }

        public string? Type { get; private set; }

        public string? Version { get; private set; }

        public Author? Author { get; private set; }

        public string? Rules { get; private set; }

        public string? Info { get; private set; }
    }
}
