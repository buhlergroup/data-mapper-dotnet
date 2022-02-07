namespace Buhlergroup.DataMapper.Condition
{
    using Buhlergroup.DataMapper.Model;
    using Newtonsoft.Json.Linq;

    public interface ICondition
    {
        string Apply(FieldConditionModel condition, JObject model);
    }
}
