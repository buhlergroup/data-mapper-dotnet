namespace Buhler.DataMapper.Helper.Test
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StreamHelperTest
    {
        [TestMethod]
        public async Task ToStringAsync_ReadStreamCorrectly_Async()
        {
            var requestBodyString = "[{'bpmnr':'123'}]";

            using var contentStream = await new StringContent(requestBodyString).ReadAsStreamAsync().ConfigureAwait(false);

            var reader = new StreamHelper();
            var result = await reader.ToStringAsync(contentStream).ConfigureAwait(false);

            result.Should().Contain("[{'bpmnr':'123'}]");
        }

        [TestMethod]
        public async Task ToStringAsync_ReadEmptyStreamCorrectly_Async()
        {
            var reader = new StreamHelper();
            var result = await reader.ToStringAsync(new MemoryStream()).ConfigureAwait(false);

            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task ToStringAsync_ThrowOnNullStream_Async()
        {
            var reader = new StreamHelper();

            Func<Task> act = async () => await reader.ToStringAsync(null).ConfigureAwait(false);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
