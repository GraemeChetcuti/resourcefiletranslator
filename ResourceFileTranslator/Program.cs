using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ResourceFileTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                ContainerBuilder iocBuilder = new ContainerBuilder();
                iocBuilder.RegisterModule<AutofacModule>();
                var iocContainer = iocBuilder.Build();

                var process = iocContainer.Resolve<ITranslationProcess>();
                process.Run();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadKey();
        }
    }
}
