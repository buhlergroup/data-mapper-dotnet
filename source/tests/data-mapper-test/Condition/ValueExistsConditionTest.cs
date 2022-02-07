namespace Buhlergroup.DataMapper.Condition.Test
{
    using System;
    using Buhlergroup.DataMapper.Model;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;

    [TestClass]
    public class ValueExistsConditionTest
    {
        [TestMethod]
        public void Apply_ThrowOnNullCondition()
        {
            var jsonObject = new JObject
            {
                { "bpmnr", "123" }
            };

            var handler = new ValueExistsCondition();

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
                Value = "ms13"
            };

            var handler = new ValueExistsCondition();

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
                Value = "1234"
            };

            var result = new ValueExistsCondition().Apply(condition, jsonObject);
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
                Value = "1234"
            };

            var result = new ValueExistsCondition().Apply(condition, jsonObject);
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
                Value = "1234"
            };

            var result = new ValueExistsCondition().Apply(condition, jsonObject);
            result.Should().BeNull();
        }
    }
}
