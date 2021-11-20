using LogConverter.Data.Interfaces;
using System.IO;
using System.Text;

namespace LogConverter.Data.Services
{
    public class FileService : IFileService
    {
        public bool CreateTextFile(string filePath, string fileContent, string fileName)
        {
            string fileDirectory = Directory.GetCurrentDirectory() + filePath;
            string fileFullPath = fileDirectory + "/" + fileName;
            byte[] fileContentBytes = Encoding.ASCII.GetBytes(fileContent);

            CreateDirectoryIfNotExists(fileDirectory);
            WriteContentInFile(fileContentBytes, fileFullPath);

            return true;
        }

        public void CreateDirectoryIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public void WriteContentInFile(byte[] fileContent, string filePath)
        {
            using FileStream stream = File.Create(filePath);
            stream.Write(fileContent, 0, fileContent.Length);
        }
    }
}
