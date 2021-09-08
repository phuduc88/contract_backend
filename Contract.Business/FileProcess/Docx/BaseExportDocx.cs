using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Contract.Business
{
    public class BaseExportDocx
    {
        public static void ReplaceMergeFieldWithText(IEnumerable<FieldCode> fields, string mergeFieldName, string replacementText)
        {
            try
            {
                var field = fields
                    .Where(f => f.InnerText.Contains(mergeFieldName))
                    .FirstOrDefault();

                if (field != null)
                {
                    // Get the Run that contains our FieldCode
                    // Then get the parent container of this Run
                    Run rFldCode = (Run)field.Parent;

                    // Get the three (3) other Runs that make up our merge field
                    Run rBegin = rFldCode.PreviousSibling<Run>();
                    Run rSep = rFldCode.NextSibling<Run>();
                    Run rText = rSep.NextSibling<Run>();
                    Run rEnd = rText.NextSibling<Run>();

                    // Get the Run that holds the Text element for our merge field
                    // Get the Text element and replace the text content 
                    Text t = rText.GetFirstChild<Text>();
                    t.Text = replacementText;

                    // Remove all the four (4) Runs for our merge field
                    rFldCode.Remove();
                    rBegin.Remove();
                    rSep.Remove();
                    rEnd.Remove();
                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public static Dictionary<string, string> GetFieldConfig(IEnumerable<FieldCode> fields)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (FieldCode item in fields)
            {
                Run rFldCode = (Run)item.Parent;
                Run rSep = rFldCode.NextSibling<Run>();
                Run rText = rSep.NextSibling<Run>();
                Text t = rText.GetFirstChild<Text>();
                string value = t.Text.Replace("«", "").Replace("»", "");
                if (result.ContainsKey(value))
                {
                    continue;
                }
                result.Add(value, "");
            }
            return result;
        }

        public static string GetFullPathFileExport(string fullPathFile, string fileName)
        {
            string path = Path.Combine(fullPathFile, "Exports");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return string.Format("{0}\\{1}", path, fileName);
        }

        public static RunProperties GetRunPropertyFromTableCell(TableRow rowCopy, int cellIndex)
        {
            var runProperties = new RunProperties();
            var fontname = "Times New Roman";
            var fontSize = "20";
            try
            {
                fontname = rowCopy.Descendants<TableCell>()
                       .ElementAt(cellIndex)
                       .GetFirstChild<Paragraph>()
                       .GetFirstChild<ParagraphProperties>()
                       .GetFirstChild<ParagraphMarkRunProperties>()
                       .GetFirstChild<RunFonts>()
                       .Ascii;
            }
            catch
            {
                //swallow
            }
            try
            {
                fontSize = rowCopy.Descendants<TableCell>()
                          .ElementAt(cellIndex)
                          .GetFirstChild<Paragraph>()
                          .GetFirstChild<ParagraphProperties>()
                          .GetFirstChild<ParagraphMarkRunProperties>()
                          .GetFirstChild<FontSize>()
                          .Val;
            }
            catch
            {
                //swallow
            }
            runProperties.AppendChild(new RunFonts() { Ascii = fontname });
            runProperties.AppendChild(new FontSize() { Val = fontSize });
            return runProperties;
        }
       
    }



}
