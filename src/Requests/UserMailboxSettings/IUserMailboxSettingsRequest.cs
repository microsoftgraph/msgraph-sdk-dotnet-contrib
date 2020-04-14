using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface IUserMailboxSettingsRequest : IBaseRequest
  {
    Task<MailboxSettings> GetAsync();
    Task<MailboxSettings> GetAsync(CancellationToken cancellationToken);
  }
}
