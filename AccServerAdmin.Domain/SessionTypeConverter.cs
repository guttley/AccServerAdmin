using System;
using AccServerAdmin.Domain.AccConfig;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain
{
    public class SessionTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var sessionType = (SessionType)value;

            switch (sessionType)
            {
                case SessionType.Practice:
                    writer.WriteValue("P");
                    break;
                case SessionType.Qually:
                    writer.WriteValue("Q");
                    break;
                case SessionType.Race:
                    writer.WriteValue("R");
                    break;
            }
            
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value.ToString() == "P")
                return SessionType.Practice;

            if (reader.Value.ToString() == "Q")
                return SessionType.Qually;

            if (reader.Value.ToString() == "R")
                return SessionType.Race;

            throw new Exception($"Unknown session type: {reader.Value}");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SessionType);
        }
    }
}
