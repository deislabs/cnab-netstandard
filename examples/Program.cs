using System;
using System.IO;

using Cnab.Bundle;
using Newtonsoft.Json;

namespace Examples
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bundle = Bundle.LoadUnsigned("bundles/thin-bundle.json");
            Console.WriteLine(JsonConvert.SerializeObject(bundle));
        }
    }
}
