using System;
using Microsoft.Graph;

namespace Graph.Community
{
  public class Form : BaseItem
  {
    public PageType FormType { get; set; }

    public new Guid Id { get; set; }

    public string ServerRelativeUrl { get; set; }
  }

  public enum PageType
  {
    Invalid = -1,
    DefaultView = 0,
    NormalView = 1,
    DialogView = 2,
    View = 3,
    DisplayForm = 4,
    DisplayFormDialog = 5,
    EditForm = 6,
    EditFormDialog = 7,
    NewForm = 8,
    NewFormDialog = 9,
    SolutionForm = 10,
    PAGE_MAXITEMS = 11
  }
}
