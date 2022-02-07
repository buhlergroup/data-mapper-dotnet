namespace Buhlergroup.DataMapper.Operation
{
    using Buhlergroup.DataMapper.Model;

    public static class OperationFactory
    {
        public static IOperation GetFieldOperation(FieldCombination combination)
        {
            return combination switch
            {
                FieldCombination.AND => new AndOperation(),
                FieldCombination.OR => new OrOperation(),
                _ => null,
            };
        }
    }
}
