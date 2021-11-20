using LogConverter.Data.Interfaces;
using LogConverter.Data.Services;
using LogConverter.Tests.Fakers;
using FluentAssertions;
using Moq;
using System.Net.WebSockets;
using Xunit;

namespace LogConverter.Tests.Services
{
    public class LogConverterServiceTests
    {
        private readonly ILogConverterService _sut;
        private readonly Mock<IFileService> _fileServiceMock = new Mock<IFileService>();
        private readonly Mock<ILogReaderService> _logReaderServiceMock = new Mock<ILogReaderService>();

        public LogConverterServiceTests()
        {
            _sut = new LogConverterService(
                fileService: _fileServiceMock.Object,
                logReaderService: _logReaderServiceMock.Object);
        }

        [Fact(DisplayName = "ConvertLog return true")]
        [Trait("Category", "Services")]
        public void ConvertLog_WhenAllIsValid_ReturnsTrue()
        {
            //Arrange
            var logUrl = LogUrlFaker.GetValidLogUrl();
            var fileContent = LogContentFaker.GetValidContent();
            var filePath = "/output/file.txt";

            _fileServiceMock.Setup(x => x.CreateTextFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _logReaderServiceMock.Setup(x => x.ReadFileContentFromUrl(It.IsAny<string>()))
                .Returns(fileContent);

            //Act
            var response = _sut.ConvertLog(logUrl, filePath);

            //Assert
            response.Should()
                .BeTrue();
        }

        [Fact(DisplayName = "ConvertLog when logurl is invalid")]
        [Trait("Category", "Services")]
        public void ConvertLog_WhenLogUrlIsInvalid_ReturnsFalse()
        {
            //Arrange
            var logUrl = LogUrlFaker.GetInvalidLogUrl();
            var fileContent = LogContentFaker.GetValidContent();
            var filePath = "/output/file.txt";

            _logReaderServiceMock.Setup(x => x.ReadFileContentFromUrl(It.IsAny<string>()))
                .Throws(new WebSocketException());

            _fileServiceMock.Setup(x => x.CreateTextFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            //Act
            var response = _sut.ConvertLog(logUrl, filePath);

            //Assert
            response.Should()
                .BeFalse();
        }
    }
}
