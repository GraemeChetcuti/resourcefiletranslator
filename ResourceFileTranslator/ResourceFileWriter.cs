using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ResourceFileTranslator
{
    interface IResourceFileWriter
    {
        void SaveFile(XmlDocument doc, string path);
    }

    class ResourceFileWriter : IResourceFileWriter
    {

        public void SaveFile(XmlDocument doc, string path)
        {
            doc.Save(path);
        }
    }
}
