using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.IO;
using System.Xml.Xsl;

namespace task2
{
    class Program
    {
        static void Main()
        {
            StringBuilder log_builder = new StringBuilder();
            DateTime local_time = DateTime.Now;

            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add("http://tempuri.org/XMLSchema.xsd", "XMLSchema.xsd");
                settings.ValidationType = ValidationType.Schema;

                XmlReader reader = XmlReader.Create("XMLFile.xml", settings);
                XmlReader readerXSD = XmlReader.Create("XMLFile.xml");
                XmlDocument document = new XmlDocument();
                document.Load(reader);

                Console.WriteLine("Document " + reader.BaseURI + " is loaded | " + local_time);
                log_builder.Append("Document " + reader.BaseURI + " is loaded | " + local_time + "\n");
                File.AppendAllText("log.txt", log_builder.ToString());
                log_builder.Clear();

                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);

                document.Validate(eventHandler);

                Console.WriteLine("The document was successfully validated by XSD | " + local_time);
                log_builder.Append("The document was successfully validated by XSD | " + local_time + "\n");
                File.AppendAllText("log.txt", log_builder.ToString());
                log_builder.Clear();

                XPathDocument myXPathDoc = new XPathDocument(readerXSD);
                XslCompiledTransform myXslTrans = new XslCompiledTransform();
                myXslTrans.Load("XSLTFile.xslt");
                XmlTextWriter myWriter = new XmlTextWriter("result.xml", null);
                myXslTrans.Transform(myXPathDoc, null, myWriter);

                document.Load("result.xml");

                Console.WriteLine("The document was successfully transformed by XSLT | " + local_time);
                log_builder.Append("The document was successfully transformed by XSLT | " + local_time + "\n");
                File.AppendAllText("log.txt", log_builder.ToString());
                log_builder.Clear();

                document.Validate(eventHandler);

                Console.WriteLine("Transformed document was successfully validated by XSD | " + local_time);
                log_builder.Append("Transformed document was successfully validated by XSD | " + local_time + "\n");
                File.AppendAllText("log.txt", log_builder.ToString());
                log_builder.Clear();

                /*XPathNavigator navigator = document.CreateNavigator();
                navigator.MoveToFollowing("Customer", "http://tempuri.org/XMLSchema.xsd");
                XmlWriter writer = navigator.InsertAfter();
                writer.WriteStartElement("anotherNode", "http://tempuri.org/XMLSchema.xsd");
                writer.WriteEndElement();
                writer.Close();*/

                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    Console.ReadKey();
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    Console.ReadKey();
                    break;
            }
        }
    }
}
