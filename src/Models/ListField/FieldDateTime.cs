﻿namespace Graph.Community
{
  public class FieldDateTime : Field
  {
    public string DateFormat { get; set; }
    public CalendarType DateTimeCalendarType { get; set; }
    public DateTimeFieldFormatType DisplayFormat { get; set; }
    public DateTimeFieldFriendlyFormatType FriendlyDisplayFormat { get; set; }
    public string TimeFormat { get; set; }
  }

  public enum DateTimeFieldFormatType
  {
    DateOnly,
    DateTime
  }

  public enum DateTimeFieldFriendlyFormatType
  {
    Unspecified,
    Disabled,
    Relative
  }

  public enum CalendarType
  {
    None = 0,
    Gregorian = 1,
    Japan = 3,
    Taiwan = 4,
    Korea = 5,
    Hijri = 6,
    Thai = 7,
    Hebrew = 8,
    GregorianMEFrench = 9,
    GregorianArabic = 10,
    GregorianXLITEnglish = 11,
    GregorianXLITFrench = 12,
    KoreaJapanLunar = 14,
    ChineseLunar = 15,
    SakaEra = 16,
    UmAlQura = 23
  }
}
