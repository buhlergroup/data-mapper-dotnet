namespace Buhler.DataMapper.Condition
{
    using System;
    using Buhler.DataMapper.Model;
    using Newtonsoft.Json.Linq;

    public class DefaultCondition : ICondition
    {
        public string Apply(FieldConditionModel condition, JObject model)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition.Value;
        }
    }
}
