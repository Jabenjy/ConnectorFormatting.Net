using System;
using System.IO;
using Newtonsoft.Json;
using ConnectorFormating;

namespace ConnectorFormatting
{
    class Formatter
    {
        private static dynamic connector;
        private static bool useConnectorFields;

        public Formatter(string path)
        {
            connector = ReadFile(path);
            useConnectorFields = false;
        }

        public Formatter(string path, bool useConnectorFields)
        {
            connector = ReadFile(path);
            Formatter.useConnectorFields = useConnectorFields;
        }

        private dynamic ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.Write("No File Found...");
                return null;
            }
            using (StreamReader r = File.OpenText(path))
            {
                string json = r.ReadToEnd();
                dynamic connector = JsonConvert.DeserializeObject(json);
                return connector;
            }
        }

        public void FormatDescriptions()
        {
            dynamic temp = connector;
            foreach (var i in temp.Methods)
            {
                try
                {
                    i.Description = FormatDescription(i.Description.ToString());
                }
                catch { MethodDescriptionWarning(); }
            }
            try
            {
                temp.Description = FormatDescription(temp.Description.ToString());
            }
            catch { ConnectorDescriptionWarning(); }
            connector = temp;
        }

        private string FormatDescription(string description)
        {
            description = description.Trim();
            if (!description.EndsWith(".")) description += ".";
            return description;
        }

        private string FormatSingleDisplayName(string displayName) => displayName.RemoveLeadingUnderscores()
          .PeriodsUnderscoresToSpaces()
          .TitleCaseWords()        
          .FormatDisplayNameIds()
          .SplitWords()
          .SplitAcronym();

        private string FormatSingleSystemField(string systemField) => systemField.PeriodsUnderscoresToSpaces()
                    .RemoveSquareBraces()
                    .SplitWords()
                    .TitleCaseWords()
                    .RemoveSpaces()
                    .FormatSystemFieldIds();


        private void MethodDescriptionWarning()
        {
        }

        private void ConnectorDescriptionWarning()
        {
        }

        public void FormatSystemFields()
        {
            string[] format = { "RequestFormat", "ResponseFormat" };
            dynamic temp = connector;
            foreach (var i in temp.Methods)
            {
                for (var f = 0; f < 2; f++) {
                    try
                    {
                        foreach (var j in i[format[f]].Fields)
                        {
                            j.SystemField = FormatSingleSystemField(j.SystemField.ToString());
                        }
                    }catch(NullReferenceException e) { }
                }
            }
            connector = temp;
        }

        public void FormatDisplayNames()
        {
            FormatParameterDisplayNames();
            FormatFieldDisplayNames();
        }

        private void FormatParameterDisplayNames()
        {
            dynamic temp = connector;
            foreach (var i in temp.Methods)
            {
                foreach (var j in i.Parameters)
                {
                    j.DisplayName = FormatSingleDisplayName(j.DisplayName.ToString());
                }
            }
            connector = temp;
        }

        private void FormatFieldDisplayNames()
        {
            string[] format = { "RequestFormat", "ResponseFormat" };
            dynamic temp = connector;
            foreach (var i in temp.Methods)
            {
                for (var f = 0; f < 2; f++)
                {
                    foreach (var j in i[format[f]].Fields)
                    {
                        j.DisplayName = FormatSingleDisplayName(j.DisplayName.ToString());
                    }
                }
            }
            connector = temp;
        }

        public void CheckMethodStatus()
        {
            dynamic temp = connector;

            foreach(var i in temp.Methods)
            {
                if (i.Status.ToString().Equals("Planned")) MethodStatusWarning();
            }

            connector = temp;
        }

        private void MethodStatusWarning()
        {
        }

        public void WriteFormattedJson()
        {
            using (StreamWriter file = File.CreateText(@"C:\Users\Ben Ashing\Documents\res.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, connector);
            }
        }
    }
}
