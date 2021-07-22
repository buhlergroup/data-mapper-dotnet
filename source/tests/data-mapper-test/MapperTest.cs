namespace Buhler.DataMapper
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Buhler.DataMapper.Helper;
    using Buhler.DataMapper.Model;
    using Buhler.DataMapper.Validation;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [TestClass]
    public class MapperTest
    {
        private readonly Mock<IStreamHelper> _mockStreamHelper = new Mock<IStreamHelper>();
        private readonly Mock<IFieldValidation> _mockFieldValidation = new Mock<IFieldValidation>();

        [TestInitialize]
        public void Initialize()
        {
            _mockStreamHelper.Setup(x => x.FileToStringAsync(It.IsAny<string>())).ReturnsAsync(@"
            [
                {
                    ""target-field"": ""ID"",
                    ""source-fields"": [
                        ""nr""
                    ]
                },
            ]
            ");
        }

        [TestMethod]
        public async Task GetFieldMapping_WithoutDirectory_Async()
        {
            var mapper = new Mapper(_mockStreamHelper.Object, _mockFieldValidation.Object);
            var mapping = await mapper.GetFieldMappingsAsync(null);

            mapping.Should().HaveCount(1);
            mapping.First().TargetField.Should().Be("ID");
            mapping.First().SourceFields.Should().HaveCount(1);
            mapping.First().SourceFields.First().Should().Be("nr");
        }

        [TestMethod]
        public async Task GetFieldMapping_WithDirectory_Async()
        {
            var mapper = new Mapper(_mockStreamHelper.Object, _mockFieldValidation.Object);
            var mapping = await mapper.GetFieldMappingsAsync("TestDirectory");

            mapping.Should().HaveCount(1);
            mapping.First().TargetField.Should().Be("ID");
            mapping.First().SourceFields.Should().HaveCount(1);
            mapping.First().SourceFields.First().Should().Be("nr");
        }

        [TestMethod]
        public void MapToDictionary_Correctly()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" }
            };

            var mapping = new List<FieldMappingModel> {
                new FieldMappingModel {
                    SourceFields = new List<string> {"nr"},
                    TargetField = "ID"
                },
                new FieldMappingModel {
                    SourceFields = new List<string> {"ttl"},
                    TargetField = "Title"
                }
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());

            var result = mapper.MapToDictionary(jsonObject, mapping);

            result.Should().HaveCount(1);
            result["ID"].Should().Be("413143");
        }
    }
}
