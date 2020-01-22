using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ResourceFileTranslator
{
    interface ITranslationClient
    {
        Task<IList<Language>> GetLanguages();
        string GetTranslation(string english, string targetLanguageCode, string sourceLanguageCode = "en");
    }


    class GoogleTranslateClient : ITranslationClient
    {

        private TranslationClient InnerTranslationClient;

        public GoogleTranslateClient()
        {
            TranslationClientBuilder tsb = new TranslationClientBuilder();
            tsb.ApiKey = "[Enter your api key here]";
            InnerTranslationClient = tsb.Build();
        }

        public async Task<IList<Language>> GetLanguages()
        {
            return await InnerTranslationClient.ListLanguagesAsync();
        }


        public string GetTranslation(string english, string targetLanguageCode, string sourceLanguageCode = "en")
        {
            var translationResult = InnerTranslationClient.TranslateText(text:english, targetLanguage:targetLanguageCode, sourceLanguage:sourceLanguageCode);
            return translationResult.TranslatedText;
        }
    }
}
