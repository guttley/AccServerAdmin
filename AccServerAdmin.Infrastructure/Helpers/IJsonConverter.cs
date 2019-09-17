namespace AccServerAdmin.Infrastructure.Helpers
{
    public interface IJsonConverter
    {
        /// <summary>
        /// Serializes the object to Json
        /// </summary>
        /// <typeparam name="T">Type to serialize from</typeparam>
        /// <param name="obj">Object to serialize</param>
        string SerializeObject<T>(T obj);

        /// <summary>
        /// DeSerializes the json to the specified type
        /// </summary>
        /// <typeparam name="T">Type to deserialize to</typeparam>
        /// <param name="json">JSON containing the serialized object</param>
        T DeserializeObject<T>(string json);
    }
}
