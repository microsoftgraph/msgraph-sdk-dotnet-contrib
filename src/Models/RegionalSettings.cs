using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community.Models
{
  public class RegionalSettings
  {
    public short AdjustHijriDays { get; set; }
    public short AlternateCalendarType { get; set; }
    public string AM { get; set; }
    public short CalendarType { get; set; }
    public short Collation { get; set; }
    public uint CollationLCID { get; set; }
    public uint DateFormat { get; set; }
    public string DateSeparator { get; set; }
    public string DecimalSeparator { get; set; }
    public string DigitGrouping { get; set; }
    public uint FirstDayOfWeek { get; set; }
    public short FirstWeekOfYear { get; set; }
    public bool IsEastAsia { get; set; }
    public bool IsRightToLeft { get; set; }
    public bool IsUIRightToLeft { get; set; }
    public string ListSeparator { get; set; }
    public uint LocaleId { get; set; }
    public string NegativeSign { get; set; }
    public uint NegNumberMode { get; set; }
    public string PM { get; set; }
    public string PositiveSign { get; set; }
    public bool ShowWeeks { get; set; }
    public string ThousandSeparator { get; set; }
    public bool Time24 { get; set; }
    public uint TimeMarkerPosition { get; set; }
    public string TimeSeparator { get; set; }
    public TimeZone TimeZone { get; set; }
    public short WorkDayEndHour { get; set; }
    public short WorkDays { get; set; }
    public short WorkDayStartHour { get; set; }
  }

  public class TimeZone
  {
    public string Description { get; set; }
    public int Id { get; set; }
    public TimeZoneInformation Information { get; set; }
  }

  public class TimeZoneInformation
  {
    public int Bias { get; set; }

    public int DaylightBias { get; set; }

    public int StandardBias { get; set; }
  }
}
