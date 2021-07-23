namespace Buhler.DataMapper.Condition.Test
{
    using System;
    using Buhler.DataMapper.Model;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;

    [TestClass]
    public class ValueDoesNotEqualConditionTest
    {
        [TestMethod]
        public void Apply_ThrowOnNullCondition()
        {
            var jsonObject = new JObject
            {
                { "bpmnr", "123" }
            };

            var handler = new ValueDoesNotEqualCondition();

            Action act = () => handler.Apply(null, jsonObject);
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Apply_ThrowOnNullObject()
        {
            var condition = new FieldConditionModel
            {
                Type = ConditionType.VALUE_EXISTS,
                Field = "ms13actual",
                Value = "ms13",
                EqualsValue = "123"
            };

            var handler = new ValueDoesNotEqualCondition();

            Action act = () => handler.Apply(condition, null);
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Apply_Correctly()
        {
            var jsonObject = new JObject
            {
                { "bpmnr", "123" }
            };
            var condition = new FieldConditionModel
            {
                Type = ConditionType.VALUE_EXISTS,
                Field = "bpmnr",
                Value = "1234",
                EqualsValue = "123"
            };

            var result = new ValueDoesNotEqualCondition().Apply(condition, jsonObject);
            result.Should().BeNull();
        }

        [TestMethod]
        public void Apply_Correctly_2()
        {
            var jsonObject = new JObject
            {
                { "bpmnr", "123" }
            };
            var condition = new FieldConditionModel
            {
                Type = ConditionType.VALUE_EXISTS,
                Field = "bpmnr",
                Value = "1234",
                EqualsValue = "123123"
            };

            var result = new ValueDoesNotEqualCondition().Apply(condition, jsonObject);
            result.Should().Be("1234");
        }

        [TestMethod]
        public void Apply_EmptyField()
        {
            var jsonObject = new JObject
            {
                { "bpmnr", "" }
            };
            var condition = new FieldConditionModel
            {
                Type = ConditionType.VALUE_EXISTS,
                Field = "bpmnr",
                Value = "1234",
                EqualsValue = "123"
            };

            var result = new ValueDoesNotEqualCondition().Apply(condition, jsonObject);
            result.Should().BeNull();
        }

        [TestMethod]
        public void Apply_NonExistingField()
        {
            var jsonObject = new JObject();
            var condition = new FieldConditionModel
            {
                Type = ConditionType.VALUE_EXISTS,
                Field = "bpmnr",
                Value = "1234",
                EqualsValue = "123"
            };

            var result = new ValueDoesNotEqualCondition().Apply(condition, jsonObject);
            result.Should().BeNull();
        }

        [TestMethod]
        public void Apply_IgnoreCase()
        {
            var jsonObject = new JObject
            {
                { "bpmnr", "ABC" }
            };
            var condition = new FieldConditionModel
            {
                Type = ConditionType.VALUE_EXISTS,
                Field = "bpmnr",
                Value = "1234",
                EqualsValue = "abc"
            };

            var result = new ValueDoesNotEqualCondition().Apply(condition, jsonObject);
            result.Should().BeNull();
        }
    }
}