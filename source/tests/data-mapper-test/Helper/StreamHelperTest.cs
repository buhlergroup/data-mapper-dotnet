namespace Buhler.DataMapper.Helper.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.IO.Abstractions.TestingHelpers;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class StreamHelperTest
    {
        private readonly Mock<IFileSystem> _mockFileSystem = new Mock<IFileSystem>();

        [TestMethod]
        public async Task ToStringAsync_ReadStreamCorrectly_Async()
        {
            var requestBodyString = "[{'bpmnr':'123'}]";

            using var contentStream = await new StringContent(requestBodyString).ReadAsStreamAsync().ConfigureAwait(false);

            var reader = new StreamHelper(_mockFileSystem.Object);
            var result = await reader.ToStringAsync(contentStream).ConfigureAwait(false);

            result.Should().Contain("[{'bpmnr':'123'}]");
        }

        [TestMethod]
        public async Task ToStringAsync_ReadEmptyStreamCorrectly_Async()
        {
            var reader = new StreamHelper(_mockFileSystem.Object);
            var result = await reader.ToStringAsync(new MemoryStream()).ConfigureAwait(false);

            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task ToStringAsync_ThrowOnNullStream_Async()
        {
            var reader = new StreamHelper(_mockFileSystem.Object);

            Func<Task> act = async () => await reader.ToStringAsync(null).ConfigureAwait(false);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [TestMethod]
        public void To()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Content") }
            });

            var reader = new StreamHelper(fileSystem);
            var result = reader.ReadFileToString(@"c:\myfile.txt");

            result.Should().Be("Content");
        }
    }
}
