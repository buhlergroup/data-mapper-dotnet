namespace Buhlergroup.DataMapper.Condition
{
    using Buhlergroup.DataMapper.Model;
    public static class ConditionHandlerFactory
    {
        public static ICondition GetConditionHandler(ConditionType type) => type switch
        {
            ConditionType.VALUE_EXISTS => new ValueExistsCondition(),
            ConditionType.VALUE_EQUALS => new ValueEqualsCondition(),
            ConditionType.VALUE_MATCHES => new ValueMatchesCondition(),
            ConditionType.VALUE_DOES_NOT_EQUAL => new ValueDoesNotEqualCondition(),
            _ => new DefaultCondition(),
        };
    }
}
