using System;
using System.Linq;
using NET.S._2018.Popivnenko._16.XmlDocs.Interface;

namespace NET.S._2018.Popivnenko._16.XmlDocs.Logic
{
    /// <summary>
    /// Provides service of parsing and storing values using other classes in solution.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UrlService<T>
    {
        private IUrlParser<T> urlParser;
        private IUrlRepository urlRepository;
        private IXmlRepository<T> xmlRepository;

        /// <summary>
        /// Constructor for a service.
        /// throws <see cref="ArgumentNullException"/> case any parameter is null.
        /// </summary>
        /// <param name="urlParser">Parser.</param>
        /// <param name="urlRepository">Repository for urls.</param>
        /// <param name="xmlRepository">Repository for xml.</param>
        public UrlService(IUrlParser<T> urlParser, IUrlRepository urlRepository, IXmlRepository<T> xmlRepository)
        {
            this.urlParser = urlParser ?? throw new ArgumentNullException(nameof(urlParser));
            this.urlRepository = urlRepository ?? throw new ArgumentNullException(nameof(urlRepository));
            this.xmlRepository = xmlRepository ?? throw new ArgumentNullException(nameof(xmlRepository));
        }

        /// <summary>
        /// Converts urls into xml and writes into file specified by xmlRepository.
        /// </summary>
        public void Convert()
        {
            var urls = urlRepository.GetUrls();
            var parsedUrls = urls.Select(u =>
            {
                var result = urlParser.Parse(u);
                return result;
            });

            xmlRepository.WriteUrls(parsedUrls);
        }
    }
}
