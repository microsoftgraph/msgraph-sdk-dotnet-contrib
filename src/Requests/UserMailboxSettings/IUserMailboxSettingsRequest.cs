using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IUserMailboxSettingsRequest : IBaseRequest
  {
    Task<MailboxSettings> GetAsync();
    Task<MailboxSettings> GetAsync(CancellationToken cancellationToken);
  }
}
