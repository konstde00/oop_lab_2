using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab2_Dementiev.Classes.Strategies
{
    internal class LINQStrategy : IStrategy
    {
        public List<Service> Analyze(string path)
        {
            var xdoc = XDocument.Load(path);

            var listOfServices = (from lvl1Child in xdoc.Descendants("service")
                                  from lvl2Child in lvl1Child.Descendants("author")
                                  select new
                                  {
                                      Title = lvl1Child.Element("title")!.Value,
                                      Description = lvl1Child.Element("description")!.Value,
                                      Type = lvl1Child.Element("type")!.Value,
                                      Version = lvl1Child.Element("version")!.Value,
                                      AuthorName = lvl2Child.Element("name")!.Value,
                                      AuthorSurname = lvl2Child.Element("surname")!.Value,
                                      AuthorAge = int.Parse(lvl2Child.Element("age")!.Value),
                                      Rules = lvl1Child.Element("rules")!.Value,
                                      Info = lvl1Child.Element("info")!.Value
                                  }
                           ).ToList();

            List<Service> services = new List<Service>();

            foreach (var elem in listOfServices)
            {
                Service service = new Service(
                        elem.Title,
                        elem.Description,
                        elem.Type,
                        elem.Version,
                        new Author(
                            elem.AuthorName,
                            elem.AuthorSurname,
                            elem.AuthorAge
                            ),
                        elem.Rules,
                        elem.Info
                    );

                services.Add(service);
            }
            return services;
        }
    }
}
