namespace Buhlergroup.DataMapper.Condition
{
    using System;
    using System.Text.RegularExpressions;
    using Buhlergroup.DataMapper.Model;
    using Newtonsoft.Json.Linq;

    public class ValueMatchesCondition : ICondition
    {
        public string Apply(FieldConditionModel condition, JObject model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            var fieldValue = (string)model[condition.Field];

            if (fieldValue == null)
            {
                return null;
            }

            var matches = Regex.IsMatch(fieldValue, condition.EqualsValue);

            return Regex.IsMatch(fieldValue, condition.EqualsValue) ? condition.Value : null;
        }
    }
}
