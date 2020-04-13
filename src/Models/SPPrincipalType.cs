using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [Flags]
  public enum SPPrincipalType
  {
    /// <summary>
    /// Enumeration whose value specifies no principal type
    /// </summary>
    None = 0,

    /// <summary>
    /// Enumeration whose value specifies a user as the principal type
    /// </summary>
    User = 1,

    /// <summary>
    /// Enumeration whose value specifies a distribution list as the principal type
    /// </summary>
    DistributionList = 2,

    /// <summary>
    /// Enumeration whose value specifies a security group as the principal type
    /// </summary>
    SecurityGroup = 4,

    /// <summary>
    /// Enumeration whose value specifies a group as the principal type
    /// </summary>
    SharePointGroup = 8,

    /// <summary>
    /// Enumeration whose value specifies all principal types
    /// </summary>
    All = 15
  }
}
