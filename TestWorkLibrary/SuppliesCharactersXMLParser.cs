using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TestWorkLibrary.Models;

namespace TestWorkLibrary.Parser
{
    public class SuppliesCharactersXMLParser
    {
        public IEnumerable<Character> ReadFile(string filePath)
        {
            CheckFile(filePath);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var xDocument = XDocument.Load(filePath);
            var xRoot = xDocument.Root;

            var receivedCharacters = xRoot.Elements("specific_character");
            var characters = new List<Character>();
            foreach (var receivedCharacter in receivedCharacters)
                yield return new Character(receivedCharacter.Attribute("id").Value, CreateSupplies(MatchingSupplyValues(receivedCharacter.Element("supplies").Value)));
        }
        public void SaveFile(string fullPath, params Character[] characters)
        {
            if (!File.Exists(fullPath))
                File.Create(fullPath).Close();

            XDocument xDocument = new XDocument(new XElement("xml"))
            {
                Declaration = new XDeclaration("1.0", "windows-1251", null)
            };

            foreach (var character in characters)
            {
                var xmlCharacter = new XElement("specific_character", new XElement("supplies", CreateSuppliesString(character)));
                xmlCharacter.SetAttributeValue("id", character.Id);
                xDocument.Root.Add(xmlCharacter);
            }

            xDocument.Save(fullPath);
        }
        public void SaveInSameFile(string fullPath, params Character[] characters)
        {
            if (!File.Exists(fullPath))
                File.Create(fullPath).Close();

            XDocument xDocument = XDocument.Load(fullPath);
            var xRoot = xDocument.Root;
            foreach (var character in characters)
                foreach (var element in xRoot.Elements("specific_character"))
                    if (element.Attribute("id").Value == character.Id)
                        element.Element("supplies").Value = CreateSuppliesString(character);

            xDocument.Save(fullPath);
        }

        protected MatchCollection MatchingSupplyValues(string supplyValues)
        {
            Regex regex = new Regex(@"[\s\\t\\n]*(?:\[(\w*)\][\s\\t\\n]*|#include '(.*)'[\s\\t\\n]*|(?:\s|\\n|\\t)*((?:\w|\.|-)+)(?:[\s\\t\\n]*=[\s\\t\\n]*(\d+))?(?:,(.*))?(?:\s|\\n|\\t)*)".Replace("'", '"'.ToString()));
            return regex.Matches(supplyValues);
        }
        protected Supplies CreateSupplies(MatchCollection matchedValues)
        {
            var supplyList = new List<Supply>();
            var includes = new List<string>();
            var spawn = "";

            foreach (Match matchValue in matchedValues)
            {
                if (matchValue.Groups[1].Value != "")
                {
                    spawn = matchValue.Groups[1].Value;
                    continue;
                }
                if (matchValue.Groups[2].Value != "")
                {
                    includes.Add(matchValue.Groups[2].Value);
                    continue;
                }
                if (matchValue.Groups[3].Value == "" && matchValue.Groups[1].Value == "")
                {
                    continue;
                }

                supplyList.Add(CreateSupply(matchValue));
            }

            return new Supplies(spawn, supplyList, includes);
        }
        protected Supply CreateSupply(Match matchValue)
        {
            var supplyName = matchValue.Groups[3].Value;
            var supplyCount = 1;
            var supplyCondition = 0.0;
            var supplyProbability = 0.0;
            var supplyAddons = new List<string>();
            if (matchValue.Groups[4].Value != "")
            {
                supplyCount = Convert.ToInt32(matchValue.Groups[4].Value);
            }
            if (matchValue.Groups[5].Value != "")
            {
                var receivedAddons = matchValue.Groups[5].Value.Split(new string[] { ",", " ", ", ", @"\n" }, StringSplitOptions.None);
                foreach (var addon in receivedAddons)
                {
                    if (addon.Contains("cond"))
                    {
                        supplyCondition = Convert.ToDouble(addon.Substring(addon.IndexOf('=') + 1));
                        continue;
                    }
                    if (addon.Contains("prob"))
                    {
                        supplyProbability = Convert.ToDouble(addon.Substring(addon.IndexOf('=') + 1));
                        continue;
                    }
                    if (!string.IsNullOrEmpty(addon))
                    {
                        supplyAddons.Add(addon);
                    }
                }
            }

            return new Supply(supplyName, supplyAddons, supplyCount, supplyCondition, supplyProbability);
        }
        protected string CreateSuppliesString(Character character)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (character.Supplies.Spawn != "" && character.Supplies.Spawn != null)
            {
                stringBuilder.AppendLine(Environment.NewLine + $"\t [{character.Supplies.Spawn}] \\n");
            }
            foreach (var item in character.Supplies.Items)
            {
                if (item.Addons.Count != 0)
                    stringBuilder.AppendLine($"\t {string.Join(", ", $"{item.Name} = {item.Count}", string.Join(", ", item.Addons), @$"cond={item.Condition}", @$"prob={item.Probability}")} \\n");
                else
                    stringBuilder.AppendLine($"\t {string.Join(", ", $"{item.Name} = {item.Count}", $"cond={item.Condition}", $"prob={item.Probability}")} \\n");
            }
            foreach (var include in character.Supplies.Includes)
            {
                stringBuilder.AppendLine($"\t #include '{include}' \\n").Replace("'", '"'.ToString());
            }
            return stringBuilder.ToString();
        }
        protected void CheckFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл по пути {filePath} не существует");
            }
            if (!new FileInfo(filePath).Name.EndsWith(".xml"))
            {
                throw new FileLoadException($"Файл по пути {filePath} не в формате XML");
            }
        }
    }
}
