using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteGroupRequest
	{
		Task<Group> GetAsync();
		Task<Group> GetAsync(CancellationToken cancellationToken);
	}
}
