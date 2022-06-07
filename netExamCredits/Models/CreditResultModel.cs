namespace netExamCredits.Models;

public record CreditResultModel(
    int? Score,
    bool Result,
    string Message,
    decimal? CreditRate);
    