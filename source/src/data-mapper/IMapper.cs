namespace Buhler.DataMapper
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Buhler.DataMapper.Model;
    using Newtonsoft.Json.Linq;

    public interface IMapper
    {
        /// <summary>
        /// Get a field value from the model using the map
        /// </summary>
        /// <param name="model">Model containing all the fields</param>
        /// <param name="map">Mapping definition for the field</param>
        /// <returns>The value of the field as an object</returns>
        object GetFieldValue(JObject model, FieldMappingModel map);

        /// <summary>
        /// Maps the model to using the mapping
        /// </summary>
        /// <param name="model">Model containing all the fields</param>
        /// <param name="map">Mapping definition for the field</param>
        /// <returns>The target schema as a dictionary</returns>
        Dictionary<string, object> MapToDictionary(JObject model, IEnumerable<FieldMappingModel> mapping);

        /// <summary>
        /// Gets the field mappings from the field mapping file
        /// </summary>
        /// <param name="mappingDirectory">Where the field mapping file is stored</param>
        /// <returns>The field mappings</returns>
        IEnumerable<FieldMappingModel> GetFileMapping(string mappingDirectory);
    }
}
