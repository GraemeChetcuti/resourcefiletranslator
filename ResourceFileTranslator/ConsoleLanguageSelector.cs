using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceFileTranslator
{

    /// <summary>
    /// Used to select the language. If I make a non-console version or non Goolge version I will separate these out.
    /// </summary>
    interface ILanguageSelector
    {
        string SelectedLanguageCode { get; set; }

        /// <summary>
        /// Provides the UI aspect of selecting a language
        /// </summary>
        /// <returns></returns>
        string SelectLanguage();
    }


    class ConsoleLanguageSelector : ILanguageSelector
    {
        public ConsoleLanguageSelector(ITranslationClient client)
        {
            this.client = client;
        }


        public string SelectedLanguageCode { get; set; }

        private ITranslationClient client;

        public string SelectLanguage()
        {
            Console.WriteLine("Please select a language");
            var langs = client.GetLanguages().Result;

            foreach (var lang in langs)
            {
                // TODO : For some reason Name appears to not be populated with data at the moment.
                Console.WriteLine($"{lang.Name} - {lang.Code}");
            }

            Console.WriteLine("Please enter code in brackets after selected language");

            var languageCode = Console.ReadLine();

            if (!langs.Where(lang => lang.Code == languageCode.Trim().ToLower()).Any())
            {
                throw new Exception("Language not found");
            }

            return SelectedLanguageCode = languageCode;
        }
    }
}
