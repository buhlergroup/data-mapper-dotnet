namespace Buhlergroup.DataMapper.Operation.Test
{
    using Buhlergroup.DataMapper.Model;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OperationFactoryTest
    {
        [TestMethod]
        public void GetFieldOperation_UnknownCombination()
        {
            var result = OperationFactory.GetFieldOperation(FieldCombination.UNKNOWN);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetFieldOperation_AndCombination()
        {
            var result = OperationFactory.GetFieldOperation(FieldCombination.AND);

            result.Should().BeOfType<AndOperation>();
        }

        [TestMethod]
        public void GetFieldOperation_OrCombination()
        {
            var result = OperationFactory.GetFieldOperation(FieldCombination.OR);

            result.Should().BeOfType<OrOperation>();
        }
    }
}
