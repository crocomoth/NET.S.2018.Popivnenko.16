using System.Collections.Generic;

namespace NET.S._2018.Popivnenko._16.XmlDocs.Interface
{
    public interface IUrlRepository
    {
        IEnumerable<string> GetUrls();
    }
}
