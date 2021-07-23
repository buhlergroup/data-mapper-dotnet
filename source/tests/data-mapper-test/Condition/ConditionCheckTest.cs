namespace Buhler.DataMapper.Condition.Test
{
    using System.Collections.Generic;
    using Buhler.DataMapper.Model;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;

    [TestClass]
    public class ConditionCheckTest
    {
        [TestMethod]
        public void ApplyFristMatchingCondition_NullConditions()
        {
            var result = ConditionCheck.ApplyFristMatchingCondition(null, new JObject());
            result.Should().BeNull();
        }

        [TestMethod]
        public void ApplyFristMatchingCondition_SingleCondition()
        {
            var jsonObject = new JObject
            {
                { "nr", "413143" }
            };

            var conditions = new List<FieldConditionModel> {
                new FieldConditionModel {
                    Type = ConditionType.VALUE_EXISTS,
                    Field = "nr",
                    Value = "Out"
                }
            };

            var result = ConditionCheck.ApplyFristMatchingCondition(conditions, jsonObject);
            result.Should().Be("Out");
        }

        [TestMethod]
        public void ApplyFristMatchingCondition_SingleNonMatchingCondition()
        {
            var jsonObject = new JObject();

            var conditions = new List<FieldConditionModel> {
                new FieldConditionModel {
                    Type = ConditionType.VALUE_EXISTS,
                    Field = "nr",
                    Value = "Out"
                }
            };

            var result = ConditionCheck.ApplyFristMatchingCondition(conditions, jsonObject);
            result.Should().BeNull();
        }
    }
}
