using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceFileTranslator
{
    class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLanguageSelector>().As<ILanguageSelector>();
            builder.RegisterType<GoogleTranslateClient>().As<ITranslationClient>();
            builder.RegisterType<ResourceFileReader>().As<IResourceFileReader>();
            builder.RegisterType<ResourceFileWriter>().As<IResourceFileWriter>();
            builder.RegisterType<TranslationProcess>().As<ITranslationProcess>();
            builder.RegisterType<ResourceFileTranslatorConsoleUi>().As<IResourceFileTranslatorUi>();
        }
    }
}
