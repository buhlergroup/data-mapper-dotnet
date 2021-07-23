namespace Buhler.DataMapper.Operation
{
    using Buhler.DataMapper.Model;
    using Buhler.DataMapper.Validation;
    using Newtonsoft.Json.Linq;

    public interface IOperation
    {

        /// <summary>
        /// Executes a mapping operation
        /// </summary>
        /// <param name="model">Model containing all fields to map</param>
        /// <param name="map">Map for a single field</param>
        /// <param name="validationHelper">Used to validate the field according to the map.</param>
        /// <returns>The result of the operation as a string</returns>
        string Execute(JObject model, FieldMappingModel map, IFieldValidation validationHelper);
    }
}
