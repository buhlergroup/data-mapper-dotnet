namespace Buhler.DataMapper.Operation
{
    using Buhler.DataMapper.Model;
    using Buhler.DataMapper.Validation;
    using Newtonsoft.Json.Linq;

    public interface IFieldOperation
    {
        /// <summary>
        /// Executes mapping operation and returns string
        /// </summary>
        /// <param name="model">the model from BPM request</param>
        /// <param name="map">the model to map BPM request to devops fields</param>
        /// <param name="validationHelper">used to valid date mapping type</param>
        ///
        string Execute(JObject model, FieldMappingModel map, IFieldValidation validationHelper);
    }
}
