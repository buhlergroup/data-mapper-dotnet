namespace Buhlergroup.DataMapper.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Buhlergroup.DataMapper.Helper;
    using Buhlergroup.DataMapper.Model;
    using Buhlergroup.DataMapper.Validation;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json.Linq;

    [TestClass]
    public class MapperTest
    {
        private readonly Mock<IStreamHelper> _mockStreamHelper = new Mock<IStreamHelper>();
        private readonly Mock<IFieldValidation> _mockFieldValidation = new Mock<IFieldValidation>();

        [TestInitialize]
        public void Initialize()
        {
            _mockStreamHelper.Setup(x => x.ReadFileToString(It.IsAny<string>())).Returns(@"
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
        public void GetFieldMapping_WithoutDirectory_Async()
        {
            var mapper = new Mapper(_mockStreamHelper.Object, _mockFieldValidation.Object);
            var mapping = mapper.GetFileMapping(null);

            mapping.Should().HaveCount(1);
            mapping.First().TargetField.Should().Be("ID");
            mapping.First().SourceFields.Should().HaveCount(1);
            mapping.First().SourceFields.First().Should().Be("nr");
        }

        [TestMethod]
        public void GetFieldMapping_WithDirectory_Async()
        {
            var mapper = new Mapper(_mockStreamHelper.Object, _mockFieldValidation.Object);
            var mapping = mapper.GetFileMapping("TestDirectory");

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

        [TestMethod]
        public void MapToDictionary_MapDeepObjectCorrectly()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" },
                { "dsm", 
                    new JObject()
                    {
                        { "descriptionShrt", "DS01" },
                        { "description", "Gateway Ordered" },
                    }
                    
                }
            };

            var mapping = new List<FieldMappingModel> {
                new FieldMappingModel {
                    SourceFields = new List<string> {"nr"},
                    TargetField = "ID"
                },
                new FieldMappingModel {
                    SourceFields = new List<string> {"dsm.description"},
                    TargetField = "Title"
                }
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());

            var result = mapper.MapToDictionary(jsonObject, mapping);

            result.Should().HaveCount(2);
            result["ID"].Should().Be("413143");
            result["Title"].Should().Be("Gateway Ordered");
        }

        [TestMethod]
        public void MapToDictionary_MapDeepArrayCorrectly()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" },
                { "dsm", new JArray()
                {
                    new JObject()
                    {
                        { "descriptionShrt", "DS01" },
                        { "description", "Gateway Ordered" },
                    }
                } }
            };

            var mapping = new List<FieldMappingModel> {
                new FieldMappingModel {
                    SourceFields = new List<string> {"nr"},
                    TargetField = "ID"
                },
                new FieldMappingModel {
                    SourceFields = new List<string> {"dsm[0].description"},
                    TargetField = "Title"
                }
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());

            var result = mapper.MapToDictionary(jsonObject, mapping);

            result.Should().HaveCount(2);
            result["ID"].Should().Be("413143");
            result["Title"].Should().Be("Gateway Ordered");
        }

        [TestMethod]
        public void MapToDictionary_QueryArrayCorrectly()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" },
                { "dsm", new JArray()
                {
                    new JObject()
                    {
                        { "descriptionShrt", "DS01" },
                        { "description", "Gateway Ordered" },
                    },
                    new JObject()
                    {
                        { "descriptionShrt", "DS02" },
                        { "description", "IT requirements sent to customer" },
                    }
                } }
            };

            var mapping = new List<FieldMappingModel> {
                new FieldMappingModel {
                    SourceFields = new List<string> {"nr"},
                    TargetField = "ID"
                },
                new FieldMappingModel {
                    SourceFields = new List<string> {"dsm[?(@.descriptionShrt == 'DS02')].description"},
                    TargetField = "Title"
                }
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());

            var result = mapper.MapToDictionary(jsonObject, mapping);

            result.Should().HaveCount(2);
            result["ID"].Should().Be("413143");
            result["Title"].Should().Be("IT requirements sent to customer");
        }

        [TestMethod]
        public void GetFieldValue_EmptyModel()
        {
            var map = new FieldMappingModel();
            var mapper = new Mapper(_mockStreamHelper.Object, _mockFieldValidation.Object);
            Action act = () => mapper.GetFieldValue(null, map);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void GetFieldValue_EmptyMap()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" }
            };
            var mapper = new Mapper(_mockStreamHelper.Object, _mockFieldValidation.Object);
            Action act = () => mapper.GetFieldValue(jsonObject, null);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void GetFieldValue_DisabledField()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { "nr" },
                TargetField = "ID",
                Disabled = true
            };

            var mapper = new Mapper(_mockStreamHelper.Object, _mockFieldValidation.Object);
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetFieldValue_TrueCondition()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { "nr" },
                TargetField = "ID",
                Conditions = new List<FieldConditionModel>{
                    new FieldConditionModel {
                        Type = ConditionType.VALUE_EQUALS,
                        Field = "nr",
                        Value = "This Project",
                        EqualsValue = "413143"
                    }
                }
            };

            var mapper = new Mapper(_mockStreamHelper.Object, _mockFieldValidation.Object);
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().NotBeNull();
            result.Should().Be("This Project");
        }

        [TestMethod]
        public void GetFieldValue_FieldCombination()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" },
                { "txt", "hello" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { "nr", "txt" },
                TargetField = "Title",
                Combination = FieldCombination.AND
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().NotBeNull();
            result.Should().Be("413143, hello");
        }

        [TestMethod]
        public void GetFieldValue_EmptySourceField()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { },
                TargetField = "ID"
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetFieldValue_NoSourceField()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" }
            };
            var map = new FieldMappingModel
            {
                TargetField = "ID"
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetFieldValue_InvalidField()
        {
            _mockFieldValidation.Setup(x => x.ValidateField(It.IsAny<FieldMappingModel>(), It.IsAny<object>())).Returns(false);
            var jsonObject = new JObject
            {
                { "nr", "413143" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { "nr" },
                TargetField = "ID"
            };

            var mapper = new Mapper(_mockStreamHelper.Object, _mockFieldValidation.Object);
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetFieldValue_TypeSelect()
        {
            var jsonObject = new JObject
            {
                { "st", "DO" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { "st" },
                TargetField = "Status",
                Type = FieldType.SELECT,
                SelectValues = new List<SelectFieldMappingModel> {
                    new SelectFieldMappingModel {
                        Origin = "DO",
                        Destination = "DO - Done"
                    }
                }
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().NotBeNull();
            result.Should().Be("DO - Done");
        }

        [TestMethod]
        public void GetFieldValue_TypeNumber()
        {
            var jsonObject = new JObject
            {
                { "nr", "00413143" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { "nr" },
                TargetField = "ID",
                Type = FieldType.NUMBER
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().NotBeNull();
            result.Should().Be(413143);
        }

        [TestMethod]
        public void GetFieldValue_TypeDate_WithoutFormat()
        {
            var jsonObject = new JObject
            {
                { "date", "2021-02-07" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { "date" },
                TargetField = "Start Date",
                Type = FieldType.DATE
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().NotBeNull();
            result.Should().Be("2021-02-07");
        }

        [TestMethod]
        public void GetFieldValue_TypeDate_WithInFormat()
        {
            var jsonObject = new JObject
            {
                { "date", "20210207105203" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { "date" },
                TargetField = "Start Date",
                Type = FieldType.DATE,
                InFormat = "yyyyMMddHHmmss",
                OutFormat = "yyyy-MM-dd"
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().NotBeNull();
            result.Should().Be("2021-02-07");
        }

        [TestMethod]
        public void GetFieldValue_TypeDate_WithInFormat_WithoutOutFormat()
        {
            var jsonObject = new JObject
            {
                { "date", "20210207105203" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { "date" },
                TargetField = "Start Date",
                Type = FieldType.DATE,
                InFormat = "yyyyMMddHHmmss"
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().NotBeNull();
            result.Should().Be("02/07/2021 10:52:03");
        }

        [TestMethod]
        public void GetFieldValue_TypeDefault()
        {
            var jsonObject = new JObject
            {
                { "nr", "00413143" }
            };
            var map = new FieldMappingModel
            {
                SourceFields = new List<string> { "nr" },
                TargetField = "ID"
            };

            var mapper = new Mapper(_mockStreamHelper.Object, new FieldValidation());
            var result = mapper.GetFieldValue(jsonObject, map);

            result.Should().NotBeNull();
            result.Should().Be("00413143");
        }
    }
}
