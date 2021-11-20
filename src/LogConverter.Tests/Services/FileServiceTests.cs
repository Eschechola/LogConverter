using Bogus.DataSets;
using LogConverter.Data.Interfaces;
using LogConverter.Data.Services;
using FluentAssertions;
using Xunit;

namespace LogConverter.Tests.Services
{
    public class FileServiceTests
    {
        private readonly IFileService _sut;

        public FileServiceTests()
        {
            _sut = new FileService();
        }

        [Fact(DisplayName = "CreateTextFile upload file to directory")]
        [Trait("Category", "Services")]
        public void CreateTextFile_WhenFileContentIsValid_UploadFileToDirectory()
        {
            //Arrange
            string fileContent = new Lorem().Paragraphs(2);
            string fakeFilePath = "/output";
            string fakeFileName = "file.txt";

            //Act
            var response = _sut.CreateTextFile(fakeFilePath, fileContent, fakeFileName);

            //Assert
            response.Should()
                .BeTrue();
        }
    }
}
