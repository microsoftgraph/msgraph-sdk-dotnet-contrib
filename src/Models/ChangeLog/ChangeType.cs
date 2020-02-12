using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public enum ChangeType
  {
    NoChange,
    Add,
    Update,
    DeleteObject,
    Rename,
    MoveAway,
    MoveInto,
    Restore,
    RoleAdd,
    RoleDelete,
    RoleUpdate,
    AssignmentAdd,
    AssignmentDelete,
    MemberAdd,
    MemberDelete,
    SystemUpdate,
    Navigation,
    ScopeAdd,
    ScopeDelete,
    ListContentTypeAdd,
    ListContentTypeDelete,
    Dirty,
    Activity
  }
}
