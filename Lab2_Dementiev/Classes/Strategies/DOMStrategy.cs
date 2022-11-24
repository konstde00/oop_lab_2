using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab2_Dementiev.Classes.Strategies
{
    internal class DOMStrategy : IStrategy
    {
        public List<Service> Analyze(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement? xRoot = xDoc.DocumentElement;
            List<Service> services = new List<Service>();
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot) // service
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

                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        string nodeValue = childnode.InnerText;
                        switch (childnode.Name)
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
                            case "rules":
                                rules = nodeValue;
                                break;
                            case "info":
                                info = nodeValue;
                                break;
                        }
                    }
                    foreach (XmlElement childNode in xnode) // author
                    {
                        foreach (XmlNode node in childNode.ChildNodes)
                        {
                            string nodeValue = node.InnerText;
                            switch (node.Name)
                            {
                                case "name":
                                    authorName = nodeValue;
                                    break;
                                case "surname":
                                    authorSurname = nodeValue;
                                    break;
                                case "age":
                                    authorAge = int.Parse(nodeValue);
                                    break;
                            }
                        }
                    }

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
            }
            return services;
        }
    }
}
