namespace LogConverter.Data.Interfaces
{
    public interface IFileService
    {
        bool CreateTextFile(string filePath, string fileContent, string fileName);
    }
}
