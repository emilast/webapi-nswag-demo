using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApiClientTestApp.Api.Serialization
{
    /// <summary>
    /// Serliazes a DateTime to a string with just the date part.
    /// </summary>
    /// <remarks>
    /// Credits: https://stackoverflow.com/a/58103218/736684
    /// </remarks>
    public class ShortDateConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-dd"));
        }
    }
}