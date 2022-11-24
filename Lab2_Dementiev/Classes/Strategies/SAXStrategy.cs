using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab2_Dementiev.Classes.Strategies
{
    internal class SAXStrategy : IStrategy
    {
        public List<Service> Analyze(string path)
        {
            List<Service> services = new List<Service>();
            using (XmlReader xr = XmlReader.Create(path))
            {
                var title = "";
                var description = "";
                var type = "";
                var version = "";
                var authorName = "";
                var authorSurname = "";
                var authorAge = 0;
                var rules = "";
                var info = "";

                DateTime registered = DateTime.Now;
                string element = "";
                while (xr.Read())
                {
                    // reads the element
                    if (xr.NodeType == XmlNodeType.Element)
                    {
                        element = xr.Name; // the name of the current element
                    }
                    // reads the element value
                    else if (xr.NodeType == XmlNodeType.Text)
                    {
                        string nodeValue = xr.Value;
                        switch (element)
                        {
                            case "title":
                                title = nodeValue;
                                break;
                            case "description":
                                description = nodeValue;
                                break;
                            case "type":
                                type = nodeValue;
                                break;
                            case "version":
                                version = nodeValue;
                                break;
                            case "name":
                                authorName = nodeValue;
                                break;
                            case "surname":
                                authorSurname = nodeValue;
                                break;
                            case "age":
                                authorAge = int.Parse(nodeValue);
                                break;
                            case "rules":
                                rules = nodeValue;
                                break;
                            case "info":
                                info = nodeValue;
                                break;
                        }
                    }
                    // reads the closing element
                    else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "service"))
                        services.Add(new Service(
                            title,
                            description,
                            type,
                            version,
                            new Author(
                                authorName,
                                authorSurname,
                                authorAge),
                            rules,
                            info));
                }
                return services;
            }
        }
    }
}
