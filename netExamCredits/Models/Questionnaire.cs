using System.ComponentModel.DataAnnotations;

namespace netExamCredits.Models;

public record Questionnaire(
    [Required, RegularExpression("([А-ЯЁ][а-яё]+[\\-\\s]?){3,}")] string Fio,
    [Required, Range(1000, 9999)] int? PassportSeries,
    [Required, Range(100000, 999999)] int? PassportNumber,
    [Required, StringLength(40, MinimumLength = 10)] string PassportGiven,
    [Required, Range(typeof(DateTime), "1/1/1900", "1/1/2008")] DateTime? PassportGivenDate,
    [Required, StringLength(40, MinimumLength = 5)] string PassportRegistration,
    [Required, Range(18, 100)] int? Age,
    [Required] bool? CriminalRecord,
    [Required, Range(10000, 100000000)] decimal? Sum,
    [Required] Goal? Goal,
    [Required] Employment? Employment,
    [Required] bool? OtherCredits,
    [Required] Pledge? Pledge,
    int? AutoAge
);

public enum Goal
{
    Consumer = 1,
    RealEstate = 2,
    OnLending = 3
}

public enum Employment
{
    Unemployed = 1,
    Ip = 2,
    Tk = 3,
    NonTk = 4,
    Pensioner = 5
}

public enum Pledge
{
    RealEstate = 1,
    Auto = 2,
    Guarantee = 3,
    NonPledge = 4
}
