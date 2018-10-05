using Newtonsoft.Json;
using System;

namespace JsonRounding
{
    class Program
    {
        static void Main(string[] args)
        {
            var target = new Target();

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new RoundingJsonConverter(2));

            var result = JsonConvert.SerializeObject(target, settings);

            Console.WriteLine(result);
            Console.Read();
        }
    }

    public class Target
    {
        public Target()
        {
            PI = (decimal)Math.PI;
        }

        public decimal PI { get; set; } 
    }

    public class RoundingJsonConverter : JsonConverter
    {
        int _precision;
        MidpointRounding _rounding;

        public RoundingJsonConverter()
            : this(2)
        {
        }

        public RoundingJsonConverter(int precision)
            : this(precision, MidpointRounding.AwayFromZero)
        {
        }

        public RoundingJsonConverter(int precision, MidpointRounding rounding)
        {
            _precision = precision;
            _rounding = rounding;
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(Math.Round((decimal)value, _precision, _rounding));
        }
    }
}
