using Newtonsoft.Json;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class List
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "CurrentChangeToken", Required = Newtonsoft.Json.Required.Default)]
		public ChangeToken CurrentChangeToken { get; set; }
	}
}
