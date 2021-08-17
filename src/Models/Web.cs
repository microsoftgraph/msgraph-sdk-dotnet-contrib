using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
#pragma warning disable CA1724 //Type names should not match namespaces

  public class Web : BaseItem
  {
    //public ChangeToken CurrentChangeToken { get; set; }

    public string UsersNavigationLink { get; set; }

    public List<User> Users { get; }

    public string AssociatedMemberGroupNavigationLink { get; set; }

    public Group AssociatedMemberGroup { get; set; }

    public string AssociatedOwnerGroupNavigationLink { get; set; }

    public Group AssociatedOwnerGroup { get; set; }

    public string AssociatedVisitorGroupNavigationLink { get; set; }

    public Group AssociatedVisitorGroup { get; set; }
  }
#pragma warning restore CA1724 //Type names should not match namespaces
}
