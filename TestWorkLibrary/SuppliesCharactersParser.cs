using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TestWorkLibrary.Models;
using TestWorkLibrary.Validators;

namespace TestWorkLibrary.Parser
{
    public static class SuppliesCharactersParser
    {
        public static IEnumerable<Character> ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл по пути {filePath} не существует");
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            XDocument xDocument = XDocument.Load(filePath);

            IEnumerable<XElement> receivedCharacters = xDocument.Root.Elements("specific_character");
            foreach (XElement receivedCharacter in receivedCharacters)
                yield return new Character(receivedCharacter.Attribute("id").Value, CreateSupplies(MatchingSupplyValues(receivedCharacter.Element("supplies").Value)));
        }
        public static void SaveFile(string fullPath, Character[] characters)
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
        public static void SaveInSameFile(string fullPath, Character[] characters)
        {
            if (!File.Exists(fullPath))
                File.Create(fullPath).Close();

            XDocument xDocument = XDocument.Load(fullPath);

            foreach (Character character in characters)
                foreach (XElement element in xDocument.Root.Elements("specific_character"))
                    if (element.Attribute("id").Value == character.Id)
                        element.Element("supplies").Value = CreateSuppliesString(character);

            xDocument.Save(fullPath);
        }

        private static MatchCollection MatchingSupplyValues(string supplyValues)
        {
            Regex regex = new Regex(@"[\s\\t\\n]*(?:\[(\w*)\][\s\\t\\n]*|#include '(.*)'[\s\\t\\n]*|(?:\s|\\n|\\t)*((?:\w|\.|-)+)(?:[\s\\t\\n]*=[\s\\t\\n]*(\d+))?(?:,(.*))?(?:\s|\\n|\\t)*)".Replace("'", '"'.ToString()));
            return regex.Matches(supplyValues);
        }
        private static Supplies CreateSupplies(MatchCollection matchedValues)
        {
            var supplyList = new List<Supply>();
            var includes = new List<string>();

            foreach (Match matchValue in matchedValues)
            {
                if (!string.IsNullOrEmpty(matchValue.Groups[2].Value))
                {
                    includes.Add(matchValue.Groups[2].Value);
                    continue;
                }
                if (!string.IsNullOrEmpty(matchValue.Groups[1].Value))
                    continue;
                if (string.IsNullOrEmpty(matchValue.Groups[3].Value) && string.IsNullOrEmpty(matchValue.Groups[1].Value))
                    continue;

                supplyList.Add(CreateSupply(matchValue));
            }

            var createdsupplies = new Supplies(supplyList, includes);
            ValidationResult suppliesValidatorResult = new SuppliesValidator().Validate(createdsupplies);
            if (!suppliesValidatorResult.IsValid)
            {
                throw new Exception($"Сущность 'Supplies' не прошла валидацию, подробнее об ошибке {string.Join(Environment.NewLine, suppliesValidatorResult.Errors)}");
            }

            return createdsupplies;
        }
        private static Supply CreateSupply(Match matchValue)
        {
            var supplyName = matchValue.Groups[3].Value;
            var supplyCount = 1;
            var supplyCondition = 1.0;
            var supplyProbability = 1.0;
            var hasFillsupplyCondition = false;
            var hasFillsupplyProbability = false;
            var supplyAddons = new List<string>();

            if (!string.IsNullOrEmpty(matchValue.Groups[4].Value))
                supplyCount = Convert.ToInt32(matchValue.Groups[4].Value);
            if (!string.IsNullOrEmpty(matchValue.Groups[5].Value))
            {
                var receivedAddons = matchValue.Groups[5].Value.Split(new string[] { ",", " ", ", ", "\\n" }, StringSplitOptions.None);
                foreach (var addon in receivedAddons)
                {
                    if (addon.Contains("cond"))
                    {
                        supplyCondition = Convert.ToDouble(addon.Substring(addon.IndexOf('=') + 1).Replace('.', ','));
                        hasFillsupplyCondition = true;
                        continue;
                    }
                    if (addon.Contains("prob"))
                    {
                        var rqrq = addon.Substring(addon.IndexOf('=') + 1);
                        supplyProbability = Convert.ToDouble(addon.Substring(addon.IndexOf('=') + 1).Replace('.', ','));
                        hasFillsupplyProbability = true;
                        continue;
                    }
                    if (!string.IsNullOrEmpty(addon))
                        supplyAddons.Add(addon);
                }
            }

            var createdsupply = new Supply(supplyName, supplyAddons, hasFillsupplyCondition, hasFillsupplyProbability, supplyProbability, supplyCondition, supplyCount);
            var supplyValidatorResult = new SupplyValidator().Validate(createdsupply);
            if (!supplyValidatorResult.IsValid)
                throw new Exception($"Сущность 'Supply' не прошла валидацию, подробнее об ошибке {string.Join(Environment.NewLine, supplyValidatorResult.Errors)}");

            return createdsupply;
        }
        private static string CreateSuppliesString(Character character)
        {
            StringBuilder stringBuilderSupplies = new StringBuilder();

            stringBuilderSupplies.AppendLine(Environment.NewLine + $"\t [spawn] \\n");
            foreach (var item in character.Supplies.Items)
            {
                StringBuilder stringBuilderSupply = new StringBuilder();

                stringBuilderSupply.Append($"\t {item.Name} = {item.Count}");
                if (item.Addons.Count != 0) stringBuilderSupply.Append($", {string.Join(", ", item.Addons)}");
                if (item.HasFillProbability) stringBuilderSupply.Append($", prob={item.Probability.ToString().Replace(',', '.')} ");
                if (item.HasFillCondition) stringBuilderSupply.Append($", cond={item.Condition.ToString().Replace(',', '.')} ");
                stringBuilderSupply.Append("\\n" + Environment.NewLine);
                stringBuilderSupplies.Append(stringBuilderSupply.ToString());
            }
            foreach (var include in character.Supplies.Includes)
                stringBuilderSupplies.AppendLine($"\t #include \"{include}\"");

            return stringBuilderSupplies.ToString();
        }
    }
}
