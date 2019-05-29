using System;
using System.IO;
using Newtonsoft.Json;

namespace ConnectorFormatting
{
    class Program
    {
        static void Main(string[] args)
        {
            Formatter f = new Formatter(@"C:\Users\Ben Ashing\Documents\test.json");
            f.FormatSystemFields();
            f.FormatDisplayNames();
            f.FormatDescriptions();
            f.WriteFormattedJson();
        }
    }
}
