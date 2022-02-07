namespace Buhlergroup.DataMapper.Condition
{
    using System;
    using Buhlergroup.DataMapper.Model;
    using Newtonsoft.Json.Linq;

    public class ValueExistsCondition : ICondition
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
            return string.IsNullOrEmpty(fieldValue) ? null : condition.Value;
        }
    }
}
