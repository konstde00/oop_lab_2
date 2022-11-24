using System.Xml.Xsl;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualBasic.ApplicationServices;
using System.Xml.Linq;
using Lab2_Dementiev.Classes;
using System.Xml.Serialization;
using Lab2_Dementiev.Classes.Strategies;

namespace Lab2_Dementiev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const string XML_PATH = @".\data.xml";
        const string XSL_PATH = @".\doc.xsl";
        const string OUTPUT_PATH = @".\output.html";

        public static string TransformXMLToHTML(string inputXml, string xsltString)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            using (XmlReader reader = XmlReader.Create(new StringReader(xsltString)))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }

        public static List<Service> AnalyzeSAX(string path)
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

        public static List<Service> AnalyzeDOM(string path)
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

        public static List <Service> AnalyzeLINQ(string path)
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

            foreach (var elem in listOfServices) {
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

        private void button1_Click(object sender, EventArgs e)
        {
            string xmlData = File.ReadAllText(XML_PATH);
            string xslData = File.ReadAllText(XSL_PATH);

            string htmlData = TransformXMLToHTML(xmlData, xslData);

            File.WriteAllText(OUTPUT_PATH, htmlData);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            List < Service > services = new List<Service>();

            var xmlContext = new XMLContext();

            xmlContext.path = XML_PATH;

            if (radioButton1.Checked)
            {
                xmlContext.SetStrategy(new SAXStrategy());
                listBox1.Items.Add("Deserialized using SAX");
            }
            else if (radioButton2.Checked)
            {
                xmlContext.SetStrategy(new DOMStrategy());
                listBox1.Items.Add("Deserialized using DOM");
            }
            else
            {
                xmlContext.SetStrategy(new LINQStrategy());
                listBox1.Items.Add("Deserialized using LINQ");
            }

            foreach (Service service in xmlContext.ExecuteStrategy())
            {
                if (service != null)
                {
                    listBox1.Items.Add(service.GetAllString());
                }
            }
        }
    }
}