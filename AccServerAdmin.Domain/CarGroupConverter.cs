using System;
using System.Linq;
using AccServerAdmin.Domain.AccConfig;
using Newtonsoft.Json;

namespace AccServerAdmin.Domain
{
    public class CarGroupConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var carGroup = EnumHelper.GetValues<CarGroup>()
                .Where(e => e == (CarGroup)value)
                .Select(g => g.GetDescription())
                .FirstOrDefault();

            writer.WriteValue(carGroup);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return EnumHelper.GetValues<CarGroup>().FirstOrDefault(e => e.GetDescription() == existingValue.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CarGroup);
        }
    }
}
