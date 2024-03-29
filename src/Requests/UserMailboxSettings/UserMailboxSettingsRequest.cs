using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class UserMailboxSettingsRequest : BaseRequest, IUserMailboxSettingsRequest
  {
    public UserMailboxSettingsRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base(requestUrl, client, options)
    {
    }
    public async Task<MailboxSettings> GetAsync()
    {
      return await this.GetAsync(CancellationToken.None);
    }

    public async Task<MailboxSettings> GetAsync(CancellationToken cancellationToken)
    {
      var mailboxSettings = await this.SendAsync<MailboxSettings>(null, cancellationToken).ConfigureAwait(false);
      return mailboxSettings;
    }
  }
}
