namespace Buhlergroup.DataMapper.Condition
{
    using System;
    using Buhlergroup.DataMapper.Model;
    using Newtonsoft.Json.Linq;

    public class ValueDoesNotEqualCondition : ICondition
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

            return String.Equals(fieldValue, condition.EqualsValue, StringComparison.OrdinalIgnoreCase) ? null : condition.Value;
        }
    }
}
