using LogConverter.Data.Interfaces;
using LogConverter.Data.Services;
using LogConverter.Tests.Fakers;
using FluentAssertions;
using System;
using System.Net;
using Xunit;

namespace LogConverter.Tests.Services
{
    public class LogReaderServiceTests
    {
        private readonly WebClient _webClient = new WebClient();
        private readonly ILogReaderService _sut;

        public LogReaderServiceTests()
        {
            _sut = new LogReaderService(
                webClient: _webClient);
        }

        [Fact(DisplayName = "ReadFileContentFromUrl return file content")]
        [Trait("Category", "Services")]
        public void ReadFileContentFromUrl_WhenLogUrlExists_ReturnFileContent()
        {
            //Arrange
            string logUrl = LogUrlFaker.GetValidLogUrl();

            //Act
            var response = _sut.ReadFileContentFromUrl(logUrl);

            //Assert
            response.Should()
                .NotBeNull()
                .And
                .NotBeEmpty();
        }

        [Fact(DisplayName = "ReadFileContentFromUrl return empty string")]
        [Trait("Category", "Services")]
        public void ReadFileContentFromUrl_WhenLogUrlIsNotValid_ThrowsException()
        {
            //Arrange
            string logUrl = LogUrlFaker.GetInvalidLogUrl();

            //Act
            Func<string> act = () => _sut.ReadFileContentFromUrl(logUrl);

            //Assert
            act.Should()
                .Throw<Exception>();
        }
    }
}
