namespace Buhler.DataMapper.Operation
{
    using Buhler.DataMapper.Model;

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
