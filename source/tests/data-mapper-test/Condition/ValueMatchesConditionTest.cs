namespace Buhlergroup.DataMapper.Condition.Test
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using Newtonsoft.Json.Linq;

    [TestClass]
    public class ValueMatchesConditionTest
    {
        [TestMethod]
        public void Apply_ThrowOnNullCondition()
        {
            var jsonObject = new JObject
            {
                { "bpmnr", "123" }
            };

            var handler = new ValueMatchesCondition();

            Action act = () => handler.Apply(null, jsonObject);
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Apply_ThrowOnNullObject()
        {
            var condition = new FieldConditionModel
            {
                Type = ConditionType.VALUE_MATCHES,
                Field = "ms13actual",
                Value = "ms13",
                EqualsValue = "123"
            };

            var handler = new ValueMatchesCondition();

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
                Type = ConditionType.VALUE_MATCHES,
                Field = "bpmnr",
                Value = "1234",
                EqualsValue = "[1-9]{3}"
            };

            var result = new ValueMatchesCondition().Apply(condition, jsonObject);
            result.Should().Be("1234");
        }

        [TestMethod]
        public void Apply_DoesNotEqual()
        {
            var jsonObject = new JObject
            {
                { "bpmnr", "1234" }
            };
            var condition = new FieldConditionModel
            {
                Type = ConditionType.VALUE_MATCHES,
                Field = "bpmnr",
                Value = "1234",
                EqualsValue = "[A-Z]"
            };

            var result = new ValueMatchesCondition().Apply(condition, jsonObject);
            result.Should().BeNull();
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
                Type = ConditionType.VALUE_MATCHES,
                Field = "bpmnr",
                Value = "1234",
                EqualsValue = "[1-9]"
            };

            var result = new ValueMatchesCondition().Apply(condition, jsonObject);
            result.Should().BeNull();
        }

        [TestMethod]
        public void Apply_NonExistingField()
        {
            var jsonObject = new JObject();
            var condition = new FieldConditionModel
            {
                Type = ConditionType.VALUE_MATCHES,
                Field = "bpmnr",
                Value = "1234",
                EqualsValue = "123"
            };

            var result = new ValueMatchesCondition().Apply(condition, jsonObject);
            result.Should().BeNull();
        }
    }
}
