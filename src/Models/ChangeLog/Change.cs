using Microsoft.Graph;
using System;
using System.Diagnostics;

namespace Graph.Community
{
	[DebuggerDisplay("{ODataType, nq}")]
	public class Change : BaseItem, IChange
	{
		public ChangeToken ChangeToken { get; set; }
		public ChangeType ChangeType { get; set; }
		public Guid SiteId { get; set; }
		public DateTime Time { get; set; }

		public Change()
			: base()
		{
			var paul = "debug";
		}
	}
}
