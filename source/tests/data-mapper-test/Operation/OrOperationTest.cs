namespace Buhler.DataMapper.Operation.Test
{
    using System;
    using System.Collections.Generic;
    using Buhler.DataMapper.Model;
    using Buhler.DataMapper.Validation;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json.Linq;

    [TestClass]
    public class OrOperationTest
    {
        private readonly Mock<IFieldValidation> _mockFieldValidation = new Mock<IFieldValidation>();

        [TestMethod]
        public void Execute_NullModel()
        {
            var operation = new OrOperation();

            Action act = () => operation.Execute(null, new FieldMappingModel(), new FieldValidation());

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Execute_NullMap()
        {
            var operation = new OrOperation();

            Action act = () => operation.Execute(new JObject(), null, new FieldValidation());

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Execute_NullValidation()
        {
            var operation = new OrOperation();

            Action act = () => operation.Execute(new JObject(), new FieldMappingModel(), null);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Execute_EmptySourceFields()
        {
            var operation = new OrOperation();
            var mapping = new FieldMappingModel {
                SourceFields = new List<string>()
            };

            var result = operation.Execute(new JObject(), mapping, new FieldValidation());

            result.Should().BeNull();
        }

        [TestMethod]
        public void Execute_ValueNull()
        {
            var operation = new OrOperation();
            var mapping = new FieldMappingModel {
                SourceFields = new List<string> {
                    "nr"
                }
            };

            var result = operation.Execute(new JObject(), mapping, new FieldValidation());

            result.Should().BeNull();
        }

        [TestMethod]
        public void Execute_ValueInValid()
        {
            _mockFieldValidation.Setup(x => x.ValidateField(It.IsAny<FieldMappingModel>(), It.IsAny<object>())).Returns(false);
            var operation = new OrOperation();
            var mapping = new FieldMappingModel {
                SourceFields = new List<string> {
                    "nr"
                }
            };
            var jsonObject = new JObject
            {
                { "nr", "413143" }
            };
            
            var result = operation.Execute(jsonObject, mapping, _mockFieldValidation.Object);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Execute_ValueValid()
        {
            var operation = new OrOperation();
            var mapping = new FieldMappingModel {
                SourceFields = new List<string> {
                    "nr"
                }
            };
            var jsonObject = new JObject
            {
                { "nr", "413143" }
            };
            
            var result = operation.Execute(jsonObject, mapping, new FieldValidation());

            result.Should().Be("413143");
        }

        [TestMethod]
        public void Execute_ValueValid_MultipleFields()
        {
            var operation = new OrOperation();
            var mapping = new FieldMappingModel {
                SourceFields = new List<string> {
                    "nr",
                    "txt"
                }
            };
            var jsonObject = new JObject
            {
                { "txt", "Hello" }
            };
            
            var result = operation.Execute(jsonObject, mapping, new FieldValidation());

            result.Should().Be("Hello");
        }
    }
}
