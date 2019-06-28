using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
	public interface ISiteRequest : IBaseRequest
	{
		Task<Site> GetAsync();
		Task<Site> GetAsync(CancellationToken cancellationToken);

		Task<ICollectionPage<Change>> GetChangesAsync(ChangeQuery query);
		Task<ICollectionPage<Change>> GetChangesAsync(ChangeQuery query, CancellationToken cancellationToken);
	}
}
