using System;
using System.Collections.Generic;
using System.Linq;
using NET.S._2018.Popivnenko._16.XmlDocs.Exception;
using NET.S._2018.Popivnenko._16.XmlDocs.Interface;

namespace NET.S._2018.Popivnenko._16.XmlDocs.Logic
{
    /// <summary>
    /// Implements <see cref="IUrlParser"/> and provides parsing for uri's.
    /// </summary>
    public class UrlParser : IUrlParser<UrlParser>
    {
        public string SourceString { get; protected set; }

        public string Scheme { get; protected set; }

        public string Host { get; protected set; }

        public string[] Segments { get; protected set; }

        public KeyValuePair<string, string>[] Parameters { get; protected set; }

        /// <summary>
        /// Tries to parse uri and store its value inside of this object.
        /// throws <see cref="ParametersException"/> case url is bad.
        /// throws <see cref="ArgumentException"/> case it cannot be parsed in any way.
        /// </summary>
        /// <param name="url">Url to be parsed.</param>
        /// <returns>Itself upon sucessfull completion.</returns>
        public UrlParser Parse(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                SourceString = uri.OriginalString;
                Scheme = uri.Scheme;
                Host = uri.Host;
                Segments = uri.Segments.Length > 0 ? uri.Segments.Skip(1).Select(str => new string(str.TakeWhile(ch => ch != '/').ToArray())).ToArray() : null;
                Parameters = uri.Query.Length > 0 ? new string(uri.Query.Skip(1).ToArray()).Split('&').Select(str => new KeyValuePair<string, string>(new string(str.TakeWhile(ch => ch != '=').ToArray()), new string(str.SkipWhile(ch => ch != '=').Skip(1).ToArray()))).ToArray() : null;

                if (Parameters != null && !Parameters.All(kvp => kvp.Key != string.Empty && kvp.Value != string.Empty))
                {
                    throw new ParametersException("wrong parameters in url!");
                }

                return this;
            }

            throw new ArgumentException(nameof(url));
        }
    }
}
