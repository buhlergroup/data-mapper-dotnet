namespace Buhlergroup.DataMapper.Condition.Test
{
    using System;
    using Buhlergroup.DataMapper.Model;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;

    [TestClass]
    public class DefaultConditionTest
    {
        [TestMethod]
        public void Apply_ThrowOnNullCondition()
        {
            var jsonObject = new JObject
            {
                { "bpmnr", "123" }
            };

            var handler = new DefaultCondition();

            Action act = () => handler.Apply(null, jsonObject);
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Apply_NotThrowOnNullObject()
        {
            var condition = new FieldConditionModel
            {
                Type = ConditionType.DEFAULT,
                Field = "ms13actual",
                Value = "ms13"
            };

            var handler = new DefaultCondition();

            Action act = () => handler.Apply(condition, null);
            act.Should().NotThrow();
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
                Type = ConditionType.DEFAULT,
                Value = "1234",
            };

            var result = new DefaultCondition().Apply(condition, jsonObject);
            result.Should().Be("1234");
        }
    }
}
