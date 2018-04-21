using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.S._2018.Popivnenko._16.XmlDocs.Interface;
using NET.S._2018.Popivnenko._16.XmlDocs.Logic;
using NET.S._2018.Popivnenko._16.XmlDocs.Repository;

namespace NET.S._2018.Popivnenko._16
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var urlRepository = new UrlRepository(@".\urls.txt");
            IXmlRepository<UrlParser> xmlRepository = new XmlUrlWriter(@".\xml.txt");
            UrlParser urlParser = new UrlParser();
            UrlService<UrlParser> service = new UrlService<UrlParser>(urlParser,urlRepository,xmlRepository);
            service.Convert();
        }
    }
}
