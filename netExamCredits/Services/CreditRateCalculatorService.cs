namespace netExamCredits.Services;

public class CreditRateCalculatorService
{
    public decimal CalculateCreditRate(int score) =>
        score switch
        {
            >= 100 => 12.5m,
            >= 96 => 15,
            >= 92 => 19,
            >= 88 => 22,
            >= 84 => 26,
            >= 80 => 30,
            _ => throw new ArgumentException("score can not be less then 80")
        };
}
