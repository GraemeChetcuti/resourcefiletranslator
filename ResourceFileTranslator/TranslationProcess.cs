using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ResourceFileTranslator
{

    interface ITranslationProcess
    {
        void Run();
    }

    class TranslationProcess : ITranslationProcess
    {
        private IResourceFileReader ResourceFileReader;
        private ILanguageSelector LanguageSelector;
        private ITranslationClient TranslationClient;
        private IResourceFileWriter ResourceFileWriter;
        private IResourceFileTranslatorUi Ui;

        public TranslationProcess(
            IResourceFileReader resourceFileReader,
            ILanguageSelector languageSelector,
            ITranslationClient translateClient,
            IResourceFileWriter resourceFileWriter,
            IResourceFileTranslatorUi ui
        ){

            ResourceFileReader = resourceFileReader;
            LanguageSelector = languageSelector;
            TranslationClient = translateClient;
            ResourceFileWriter = resourceFileWriter;
            Ui = ui;
        }

        public void Run()
        {
            Ui.Greet();

            LanguageSelector.SelectLanguage();

            Ui.TranslationBeginning();

            var file = ResourceFileReader.GetXmlDocument();
            XmlNodeList dataAttributes = GetNodesToTranslate(file);

            foreach (XmlNode tag in dataAttributes)
            {
                XmlNode valueNode = GetValueNode(tag);

                TranslatePhrase(tag, valueNode);
            }

            ResourceFileWriter.SaveFile(file, $"Resource.{LanguageSelector.SelectedLanguageCode}.resx");

            Console.WriteLine("Complete");
        }

        /// <summary>
        /// Replace the inner text with the translation
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="valueNode"></param>
        /// <param name="english"></param>
        private void TranslatePhrase(XmlNode tag, XmlNode valueNode)
        {
            var english = valueNode.InnerText;
            try
            {
                valueNode.InnerText = TranslationClient.GetTranslation(english, LanguageSelector.SelectedLanguageCode);
                Console.WriteLine($"{english} => {valueNode.InnerText}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to translate {english} because {e}");
                Console.ReadKey();
                throw;
            }
        }

        /// <summary>
        /// Within the Data Node there should be at least one Value tag which has the phrase-to-translate as innerText
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static XmlNode GetValueNode(XmlNode tag)
        {
            XmlNode valueNode = null;
            for (int x = 0; x < tag.ChildNodes.Count; x++)
            {
                if (tag.ChildNodes.Item(x).Name == "value")
                {
                    valueNode = tag.ChildNodes.Item(x);
                }
            }

            if (valueNode == null)
                throw new Exception($"no value tag in current data tag {tag.OuterXml}");
            return valueNode;
        }

        /// <summary>
        /// The Xml documents are full of intro data we need to grab the data tags which have the phrases we need to translate
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static XmlNodeList GetNodesToTranslate(XmlDocument file)
        {
            return file.GetElementsByTagName("data");
        }
    }
}
