namespace Buhlergroup.DataMapper.Condition.Test
{
    using Buhlergroup.DataMapper.Model;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConditionHandlerFactoryTest
    {
        [TestMethod]
        public void GetConditionHandler_ValueExists_Correctly()
        {
            var type = ConditionType.VALUE_EXISTS;

            var handler = ConditionHandlerFactory.GetConditionHandler(type);

            handler.Should().BeOfType<ValueExistsCondition>();
        }

        [TestMethod]
        public void GetConditionHandler_ValueEquals_Correctly()
        {
            var type = ConditionType.VALUE_EQUALS;

            var handler = ConditionHandlerFactory.GetConditionHandler(type);

            handler.Should().BeOfType<ValueEqualsCondition>();
        }

        [TestMethod]
        public void GetConditionHandler_ValueDoesNotEqual_Correctly()
        {
            var type = ConditionType.VALUE_DOES_NOT_EQUAL;

            var handler = ConditionHandlerFactory.GetConditionHandler(type);

            handler.Should().BeOfType<ValueDoesNotEqualCondition>();
        }

        [TestMethod]
        public void GetConditionHandler_Default_Correctly()
        {
            var type = ConditionType.DEFAULT;

            var handler = ConditionHandlerFactory.GetConditionHandler(type);

            handler.Should().BeOfType<DefaultCondition>();
        }
    }
}
