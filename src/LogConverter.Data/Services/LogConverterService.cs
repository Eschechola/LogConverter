using LogConverter.Data.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace LogConverter.Data.Services
{
    public class LogConverterService : ILogConverterService
    {
        private readonly IFileService _fileService;
        private readonly ILogReaderService _logReaderService;

        public LogConverterService(
            IFileService fileService,
            ILogReaderService logReaderService)
        {
            _fileService = fileService;
            _logReaderService = logReaderService;
        }

        public bool ConvertLog(string logUrl, string newLogOutputDirectory)
        {
            try
            {
                string logContent = _logReaderService.ReadFileContentFromUrl(logUrl);

                Console.WriteLine("\n\nLog to convert is...");
                Console.WriteLine(logContent);
                
                string newLogContent = BuildNewLog(logContent);
                Console.WriteLine("\n\nLog converted in...");
                Console.WriteLine(newLogContent);

                SaveNewLogFile(newLogOutputDirectory, newLogContent);
                Console.WriteLine("\n\nLog converted with success!");
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }
        

        private bool SaveNewLogFile(string newLogOutputDirectory, string fileContent)
        {
            string fileName = newLogOutputDirectory.Split("/")
                .Last();

            string filePathWithoutFileName = newLogOutputDirectory.Replace(fileName, "");

            return _fileService.CreateTextFile(filePathWithoutFileName, fileContent, fileName);
        }

        private string BuildNewLog(string logContent)
        {
            var builder = new StringBuilder();
            builder.Append(BuildNewLogHeader());
            builder.Append(BuildNewLogContent(logContent));

            return builder.ToString();
        }

        private string BuildNewLogHeader()
        {
            var builder = new StringBuilder();
            builder.AppendLine("#Version: 1.0");
            builder.AppendLine("#Date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            builder.AppendLine("#Fields: provider http-method status-code uri-path time-taken response-size cache-status");

            return builder.ToString();
        }

        private string BuildNewLogContent(string logContent)
        {
            var builder = new StringBuilder();
            string[] logContentLines = logContent.Split("\n");

            foreach(string line in logContentLines)
            {
                if (line != string.Empty)
                {
                    string lineFormatted = string.Empty;
                    lineFormatted += "\"MINHA CDN\" ";
                    lineFormatted += GetHttpMethodFromLogLine(line) + " ";
                    lineFormatted += GetHttpStatusCodeFromLogLine(line) + " ";
                    lineFormatted += GetUriPathFromLogLine(line) + " ";
                    lineFormatted += GetTimeTakenFromLogLine(line) + " ";
                    lineFormatted += GetResponseSizeFromLogLine(line) + " ";
                    lineFormatted += GetCacheStatusFromLogLine(line);

                    builder.AppendLine(lineFormatted);
                }
            }

            return builder.ToString();
        }

        private string GetHttpMethodFromLogLine(string logLine)
        {
            string[] lineElements = logLine.Split("|");

            string[] httpElements = lineElements[3]
                .Replace("\"", "")
                .TrimStart()
                .TrimEnd()
                .Split(" ");

            return httpElements[0];
        }

        private string GetHttpStatusCodeFromLogLine(string logLine)
        {
            string[] lineElements = logLine.Split("|");
            return lineElements[1];
        }

        private string GetUriPathFromLogLine(string logLine)
        {
            string[] lineElements = logLine.Split("|");

            string[] httpElements = lineElements[3]
                .Replace("\"", "")
                .TrimStart()
                .TrimEnd()
                .Split(" ");

            return httpElements[1];
        }

        private string GetTimeTakenFromLogLine(string logLine)
        {
            string[] lineElements = logLine.Split("|");
            return string.Format("{0:0}", double.Parse(lineElements[4]));
        }

        private string GetResponseSizeFromLogLine(string logLine)
        {
            string[] lineElements = logLine.Split("|");
            return lineElements[0];
        }

        private string GetCacheStatusFromLogLine(string logLine)
        {
            string[] lineElements = logLine.Split("|");
            return lineElements[2];
        }
    }
}
