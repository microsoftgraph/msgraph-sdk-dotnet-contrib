using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class ApplySiteDesignResponse
  {
    [JsonPropertyName("value")]
    public List<SiteScriptActionResult> ActionOutcomes { get; }

    public ApplySiteDesignResponse()
    {
      this.ActionOutcomes = new List<SiteScriptActionResult>();
    }
  }

  [DebuggerDisplay("{Title, nq}")]
  public class SiteScriptActionResult
  {
    [JsonPropertyName("Outcome")]
    [JsonConverter(typeof(SPEnumIntAsStringConverter<SiteScriptActionOutcome>))]
    public SiteScriptActionOutcome Outcome { get; set; }

    [JsonPropertyName("OutcomeText")]
    public string OutcomeText { get; set; }

    [JsonPropertyName("Title")]
    public string Title { get; set; }

    public SiteScriptActionResult()
    {
    }
  }

  public enum SiteScriptActionOutcome
  {
    Success,
    Failure,
    NoOp,
    SucceededWithException
  }
}
