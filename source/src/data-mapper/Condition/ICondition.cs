namespace Buhler.DataMapper.Condition
{
    using Buhler.DataMapper.Model;
    using Newtonsoft.Json.Linq;

    public interface ICondition
    {
        string Apply(FieldConditionModel condition, JObject model);
    }
}
