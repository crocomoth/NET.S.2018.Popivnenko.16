using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NET.S._2018.Popivnenko._16.XmlDocs.Interface;
using NET.S._2018.Popivnenko._16.XmlDocs.Logic;

namespace NET.S._2018.Popivnenko._16.XmlDocs.Repository
{
    /// <summary>
    /// Implements <see cref="IXmlRepository"/>  and allows to store parsed urls as xml.
    /// </summary>
    public class XmlUrlWriter : IXmlRepository<UrlParser>
    {
        private string _filePath;

        /// <summary>
        /// Basic constructor.
        /// </summary>
        public XmlUrlWriter()
        {
            this._filePath = "/xmlurl.txt";
        }

        /// <summary>
        /// Constructor with parameter to specify a destination.
        /// throws <see cref="ArgumentNullException"/> if parameter is null.
        /// </summary>
        /// <param name="filePath">Path to a file.</param>
        public XmlUrlWriter(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
        }

        /// <summary>
        /// Writes sets of urls to a file.
        /// </summary>
        /// <param name="urls">Parsed urls.</param>
        public void WriteUrls(IEnumerable<UrlParser> urls)
        {
            var writer = new XmlTextWriter(_filePath, Encoding.UTF8);
            writer.WriteStartDocument();
            writer.WriteStartElement("adresses");
            foreach (var url in urls)
            {
                if (url == null)
                {
                    continue;
                }

                WriteUrl(url, writer);
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }

        private void WriteUrl(UrlParser url, XmlTextWriter writer)
        {
            writer.WriteStartElement("urlAddress");
            writer.WriteAttributeString("scheme", url.Scheme);
            WriteHost(writer, url);
            WritePath(writer, url);
            WriteParameters(writer, url);
            writer.WriteEndElement();
        }

        private void WriteParameters(XmlTextWriter writer, UrlParser url)
        {
            if (url.Parameters?.Length > 0)
            {
                writer.WriteStartElement("parameters");
                foreach (var param in url.Parameters)
                {
                    writer.WriteStartElement("parameter");
                    writer.WriteAttributeString("key", param.Key);
                    writer.WriteAttributeString("value", param.Value);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        private void WritePath(XmlTextWriter writer, UrlParser url)
        {
            if (url.Segments?.Length > 0)
            {
                writer.WriteStartElement("uri");
                foreach (var segment in url.Segments)
                {
                    writer.WriteElementString("segment", segment);
                }

                writer.WriteEndElement();
            }
        }

        private void WriteHost(XmlTextWriter writer, UrlParser url)
        {
            writer.WriteStartElement("host");
            writer.WriteAttributeString("name", url.Host);
            writer.WriteEndElement();
        }
    }
}
