namespace Buhler.DataMapper.Validation.Test
{
    using System;
    using System.Collections.Generic;
    using Buhler.DataMapper.Model;
    using Buhler.DataMapper.Validation;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class FieldValidationTest
    {
        private readonly Mock<IFieldValidation> _mockValidationHelper = new Mock<IFieldValidation>();

        [TestInitialize]
        public void TestInitialize()
        {
            _mockValidationHelper.Setup(x => x.ValidateField(It.IsAny<FieldMappingModel>(), It.IsAny<object>())).Returns(It.IsAny<bool>);
        }

        [TestMethod]
        public void ValidateField_ThorwOnNullMapping() {
            var validation = new FieldValidation();
            Action act = () => validation.ValidateField(null, "123");

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ValidateField_NullField() {
            var validation = new FieldValidation();
            var result = validation.ValidateField(new FieldMappingModel { Type = FieldType.UNKNOWN }, null);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void ValidateField_TypeDate_ValidDate() {
            var validation = new FieldValidation();
            var result = validation.ValidateField(new FieldMappingModel { Type = FieldType.DATE }, "2020-01-02");

            result.Should().BeTrue();
        }

        [TestMethod]
        public void ValidateField_TypeDate_InValidDate() {
            var validation = new FieldValidation();
            var result = validation.ValidateField(new FieldMappingModel { Type = FieldType.DATE }, "2020-01-00");

            result.Should().BeFalse();
        }

        [TestMethod]
        public void ValidateField_TypeDate_InValidDate_2() {
            var validation = new FieldValidation();
            var result = validation.ValidateField(new FieldMappingModel { Type = FieldType.DATE }, "abc");

            result.Should().BeFalse();
        }

        [TestMethod]
        public void ValidateSelect_TypeNumber_Valid() {
            var validation = new FieldValidation();
            var result = validation.ValidateField(new FieldMappingModel { Type = FieldType.NUMBER }, "123.012");

            result.Should().BeTrue();
        }

        [TestMethod]
        public void ValidateSelect_TypeNumber_InValid() {
            var validation = new FieldValidation();
            var result = validation.ValidateField(new FieldMappingModel { Type = FieldType.NUMBER }, "abc");

            result.Should().BeFalse();
        }

        [TestMethod]
        public void ValidateSelect_TypeText_Valid() {
            var validation = new FieldValidation();
            var result = validation.ValidateField(new FieldMappingModel { Type = FieldType.TEXT }, "123.012");

            result.Should().BeTrue();
        }

        [TestMethod]
        public void ValidateSelect_TypeDefault_Valid() {
            var validation = new FieldValidation();
            var result = validation.ValidateField(new FieldMappingModel { }, "abc");

            result.Should().BeTrue();
        }

        [TestMethod]
        public void ValidateSelect_TypeSelect_Valid()
        {
            var mapping = new FieldMappingModel
            {
                Type = FieldType.SELECT,
                SelectValues = new List<SelectFieldMappingModel> {
                    new SelectFieldMappingModel {
                        Origin = "CN",
                        Destination = "CN - Cancelled"
                    }
                },
            };

            var helper = new FieldValidation();
            var result = helper.ValidateField(mapping, "CN");

            result.Should().BeTrue();
        }

        [TestMethod]
        public void ValidateSelect_TypeSelect_InValid()
        {
            var mapping = new FieldMappingModel
            {
                Type = FieldType.SELECT,
                SelectValues = new List<SelectFieldMappingModel> {
                    new SelectFieldMappingModel {
                        Origin = "CN",
                        Destination = "CN - Cancelled"
                    }
                },
            };

            var helper = new FieldValidation();
            var result = helper.ValidateField(mapping, "DO");
            result.Should().BeFalse();
        }

        [TestMethod]
        public void ValidateSelect_TypeSelect_InValid_2()
        {
            var mapping = new FieldMappingModel
            {
                Type = FieldType.SELECT
            };

            var helper = new FieldValidation();
            var result = helper.ValidateField(mapping, "DO");
            result.Should().BeFalse();
        }
    }
}
