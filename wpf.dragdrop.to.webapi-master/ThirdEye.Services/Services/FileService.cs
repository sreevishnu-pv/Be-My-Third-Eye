using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace ThirdEye.Services.Services
{
    public class FileService
    {
        string docPath = ConfigurationManager.AppSettings["TextProcessingPath"];

        public void WriteParagraphs(List<string> paragraphs, string fileName, FileTypeEnum fileType)
        {
            var absoluteFileName = $"{docPath}/{fileType.ToString()}/{fileName}";
            File.WriteAllText(absoluteFileName, "");
            foreach (string paragraph in paragraphs)
            {
                File.AppendAllText(absoluteFileName, $"{paragraph}{Environment.NewLine}");
            }
        }

        public void WriteText(string text, string fileName, FileTypeEnum fileType)
        {
            ClearText($"{docPath}/{fileType.ToString()}/{fileName}");
            using (StreamWriter outputFile = new StreamWriter(Path.Combine($"{docPath}/{fileType.ToString()}", fileName)))
            {
                outputFile.WriteLine($"{text}{Environment.NewLine}");
            }
        }

        public string ReadFile(string fileName, FileTypeEnum fileType)
        {
            var absolutePath = $"{docPath}/{fileType.ToString()}/{fileName}";           
            var text = File.ReadAllText(absolutePath);
            return text;
        }

        public void ClearText(string fileName)
        {
            File.WriteAllText(fileName, "");
        }
    }
}
