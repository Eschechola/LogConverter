using System.Threading.Tasks;

namespace LogConverter.Data.Interfaces
{
    public interface ILogConverterService
    {
        bool ConvertLog(string logUrl, string newLogOutputDirectory);
    }
}
