using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Securibox.ParseXtract.Models.Processing;
using Securibox.ParseXtract.Models.Processing.Invoice;
using System;
using System.Linq;
using System.Reflection;

namespace Securibox.ParseXtract.Models.JsonConverters
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessedDataJsonConverter : JsonConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var json = JToken.ReadFrom(reader);
            var typeToken = json.SelectToken("ProcessedType"); // or ??json.SelectToken("Type");
            if (typeToken == null) return null;         //  
            var type = ResolveInterfaceTypeRuntime(typeToken.ToString());
            var servicePayload = DeserializeLocationRuntime(json, type);
            return servicePayload;
        }

        private ProcessedData DeserializeLocationRuntime(JToken json, Type payloadType)
        {
            MethodInfo mi = typeof(JToken)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == "ToObject" && m.GetParameters().Length == 0 && m.IsGenericMethod)
                .FirstOrDefault()
                ?.MakeGenericMethod(payloadType);
            var location = mi?.Invoke(json, null);
            return (ProcessedData)location;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        private Type ResolveInterfaceTypeRuntime(string type)
        {
            if (type == typeof(PostProcessedInvoice).Name)
            {
                return typeof(PostProcessedInvoice);
            }
            else
            {
                throw new NotSupportedException("Cannot Deserialize " + type);
            }
        }
    }
}
