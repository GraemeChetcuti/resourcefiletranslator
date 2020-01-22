using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceFileTranslator
{
    interface IResourceFileTranslatorUi
    {
        void Greet();

        void TranslationBeginning();
    }

    class ResourceFileTranslatorConsoleUi : IResourceFileTranslatorUi
    {

        public void Greet()
        {
            Console.WriteLine("Resource File translator by Graeme Chetcuti 0.1.0");
        }

        public void TranslationBeginning()
        {
            Console.WriteLine("Beginning translation process");
        }

    }
}
