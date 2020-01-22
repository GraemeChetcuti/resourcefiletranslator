using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ResourceFileTranslator
{
    interface IResourceFileReader
    {
        XmlDocument GetXmlDocument();
    }

    class ResourceFileReader : IResourceFileReader
    {
        public string ResourceFilePath = "Resource.resx";

        public XmlDocument GetXmlDocument()
        {
            XmlDocument resourceFile = new XmlDocument();
            resourceFile.Load(ResourceFilePath);
            return resourceFile;
        }
    }
}
