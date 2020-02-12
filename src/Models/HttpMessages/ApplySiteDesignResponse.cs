using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class ApplySiteDesignResponse
  {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "value", Required = Newtonsoft.Json.Required.Default)]
    public List<SiteScriptActionResult> ActionOutcomes { get; }

    public ApplySiteDesignResponse()
    {
      this.ActionOutcomes = new List<SiteScriptActionResult>();
    }
  }

  [DebuggerDisplay("{Title, nq}")]
  public class SiteScriptActionResult
  {
    public SiteScriptActionOutcome Outcome { get; set; }

    public string OutcomeText { get; set; }

    public string Title { get; set; }
  }

  public enum SiteScriptActionOutcome
  {
    Success,
    Failure,
    NoOp,
    SucceededWithException
  }
}
