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
            var bundle = LoadBundleFile("bundles/thick-bundle.json");
            Console.WriteLine(bundle.ToString());
        }

        public static Bundle LoadBundleFile (string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Bundle>(json);
            }
        }
    }
}
