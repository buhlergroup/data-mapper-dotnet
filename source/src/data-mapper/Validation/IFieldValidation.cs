namespace Buhler.DataMapper.Validation
{
    using Buhler.DataMapper.Model;

    public interface IFieldValidation
    {
        bool ValidateField(FieldMappingModel mapping, object field);
    }
}
