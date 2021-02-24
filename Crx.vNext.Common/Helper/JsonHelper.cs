using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Crx.vNext.Common.Base;

namespace Crx.vNext.Common.Helper
{
    public static class JsonHelper
    {
        public static TValue Deserialize<TValue>(ReadOnlySpan<byte> utf8Json)
        {
            return JsonSerializer.Deserialize<TValue>(utf8Json, SystemTextJsonConvert.GetJsonSerializerOptions());
        }

        public static object Deserialize(ReadOnlySpan<byte> utf8Json, Type returnType)
        {
            return JsonSerializer.Deserialize(utf8Json,returnType, SystemTextJsonConvert.GetJsonSerializerOptions());
        }
        
        public static TValue Deserialize<TValue>(string json)
        {
            return JsonSerializer.Deserialize<TValue>(json, SystemTextJsonConvert.GetJsonSerializerOptions());
        }
        
        public static object Deserialize(string json, Type returnType)
        {
            return JsonSerializer.Deserialize(json, returnType, SystemTextJsonConvert.GetJsonSerializerOptions());
        }
        
        public static TValue Deserialize<TValue>(ref Utf8JsonReader reader)
        {
            return JsonSerializer.Deserialize<TValue>(ref reader, SystemTextJsonConvert.GetJsonSerializerOptions());
        }
        
        public static object Deserialize(ref Utf8JsonReader reader, Type returnType)
        {
            return JsonSerializer.Deserialize(ref reader, returnType, SystemTextJsonConvert.GetJsonSerializerOptions());
        }
        
        public static ValueTask<TValue> DeserializeAsync<TValue>(Stream utf8Json, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.DeserializeAsync<TValue>(utf8Json, SystemTextJsonConvert.GetJsonSerializerOptions(), cancellationToken);
        }
        
        public static ValueTask<object> DeserializeAsync(Stream utf8Json, Type returnType, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.DeserializeAsync(utf8Json, returnType, SystemTextJsonConvert.GetJsonSerializerOptions(), cancellationToken);
        }

        public static string Serialize<TValue>(TValue value)
        {
            return JsonSerializer.Serialize<TValue>(value, SystemTextJsonConvert.GetJsonSerializerOptions());
        }

        public static string Serialize(object value, Type inputType)
        {
            return JsonSerializer.Serialize(value, inputType, SystemTextJsonConvert.GetJsonSerializerOptions());
        }
        
        public static void Serialize<TValue>(Utf8JsonWriter writer, TValue value)
        {
            JsonSerializer.Serialize<TValue>(writer, value, SystemTextJsonConvert.GetJsonSerializerOptions());
        }
        
        public static void Serialize(Utf8JsonWriter writer, object value, Type inputType)
        {
            JsonSerializer.Serialize(writer, value, inputType, SystemTextJsonConvert.GetJsonSerializerOptions());
        }
        public static Task SerializeAsync<TValue>(Stream utf8Json, TValue value, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.SerializeAsync<TValue>(utf8Json, value, SystemTextJsonConvert.GetJsonSerializerOptions(), cancellationToken);
        }
        
        public static Task SerializeAsync(Stream utf8Json, object value, Type inputType, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.SerializeAsync(utf8Json, value, inputType, SystemTextJsonConvert.GetJsonSerializerOptions(), cancellationToken);
        }
        
        public static byte[] SerializeToUtf8Bytes<TValue>(TValue value)
        {
            return JsonSerializer.SerializeToUtf8Bytes<TValue>(value, SystemTextJsonConvert.GetJsonSerializerOptions());
        }

        public static byte[] SerializeToUtf8Bytes(object value, Type inputType)
        {
            return JsonSerializer.SerializeToUtf8Bytes(value, inputType, SystemTextJsonConvert.GetJsonSerializerOptions());
        }
    }
}
