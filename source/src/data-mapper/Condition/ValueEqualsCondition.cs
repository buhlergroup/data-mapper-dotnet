namespace Buhler.DataMapper.Condition
{
    using System;
    using Buhler.DataMapper.Model;
    using Newtonsoft.Json.Linq;

    public class ValueEqualsCondition : ICondition
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

            if (String.IsNullOrEmpty(fieldValue))
            {
                return null;
            }

            return String.Equals(fieldValue, condition.EqualsValue, StringComparison.OrdinalIgnoreCase) ? condition.Value : null;
        }
    }
}
