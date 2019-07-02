using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class GetChangesRequest
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "query", Required = Newtonsoft.Json.Required.Default)]
		public ChangeQuery Query { get; set; }
	}

	internal class ChangeQuerySerializer:JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			JToken t = JToken.FromObject(value);

			
		}

		public override bool CanRead
		{
			get { return false; }
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(GetChangesRequest) == objectType;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
