using System;
using System.IO;
using System.Threading.Tasks;
using Cnab.Bundle;
using Newtonsoft.Json;

namespace Examples
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var bundle = await Bundle.LoadUnsignedAsync("bundles/thin-bundle.json");

            Console.WriteLine(
                JsonConvert.SerializeObject(bundle, Formatting.Indented));
        }
    }
}
