using System;
using System.Collections.Generic;
using System.IO;
using NET.S._2018.Popivnenko._16.XmlDocs.Interface;

namespace NET.S._2018.Popivnenko._16.XmlDocs.Repository
{
    /// <summary>
    /// Implements <see cref="IUrlRepository"/> and provides funtionality to get urls from file.
    /// </summary>
    public class UrlRepository : IUrlRepository
    {
        private string _filePath;

        /// <summary>
        /// Basic constructor.
        /// </summary>
        public UrlRepository()
        {
            this._filePath = "/urls.txt";
        }

        /// <summary>
        /// Contstructor with a parameter to specify source.
        /// throws <see cref="ArgumentNullException"/> if parameter is <see langword="null"/>
        /// </summary>
        /// <param name="filePath">Path to the file with urls.</param>
        public UrlRepository(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
        }

        /// <summary>
        /// Represetns urls in file as a IEnumerable
        /// </summary>
        /// <returns>Urls as stings.</returns>
        public IEnumerable<string> GetUrls()
        {
            using (var reader = new StreamReader(File.OpenRead(_filePath)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
