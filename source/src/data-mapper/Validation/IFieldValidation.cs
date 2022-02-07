namespace Buhlergroup.DataMapper.Validation
{
    using Buhlergroup.DataMapper.Model;

    public interface IFieldValidation
    {
        bool ValidateField(FieldMappingModel mapping, object field);
    }
}
