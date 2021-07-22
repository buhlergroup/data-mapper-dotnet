namespace Buhler.DataMapper.Condition
{
    using System.Collections.Generic;
    using Buhler.DataMapper.Model;
    using Newtonsoft.Json.Linq;

    public static class ConditionCheck
    {
        public static string ApplyFristMatchingCondition(IEnumerable<FieldConditionModel> conditions, JObject model)
        {
            if (conditions == null)
            {
                return null;
            }

            foreach (var condition in conditions)
            {
                var conditionHandler = ConditionHandlerFactory.GetConditionHandler(condition.Type);
                var result = conditionHandler.Apply(condition, model);
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            return null;
        }
    }
}
