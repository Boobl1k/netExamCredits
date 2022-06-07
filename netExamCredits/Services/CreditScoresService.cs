using netExamCredits.Models;

namespace netExamCredits.Services;

public class CreditScoresService
{
    public int CountScore(Questionnaire questionnaire)
    {
        var (fio, passportSeries, passportNumber, passportGiven, passportGivenDate, passportRegistration, age,
            criminalRecord, sum, goal, employment, otherCredits, pledge, autoAge) = questionnaire;

        var score = 0;

        //Возраст
        score += age switch
        {
            >= 21 and <= 28 => sum switch
            {
                < 1000000 => 12,
                < 3000000 => 9,
                _ => 0
            },
            >= 29 and <= 59 => 14,
            >= 60 and <= 72 => pledge is not Pledge.NonPledge ? 8 : 0,
            _ => 0
        };

        //Сведения о судимости
        score += criminalRecord!.Value ? 0 : 15;

        //Трудоустройство
        score += employment switch
        {
            Employment.Unemployed => 0,
            Employment.Ip => 12,
            Employment.Tk => 14,
            Employment.NonTk => 8,
            Employment.Pensioner => age < 70 ? 5 : 0,
            _ => throw new ArgumentOutOfRangeException(nameof(questionnaire), employment,
                "questionnaire.employment out of range")
        };

        //Цель
        score += goal switch
        {
            Goal.Consumer => 14,
            Goal.RealEstate => 8,
            Goal.OnLending => 12,
            _ => throw new ArgumentOutOfRangeException(nameof(questionnaire), goal,
                "questionnaire.goal out of range")
        };

        //Залог
        score += pledge switch
        {
            Pledge.RealEstate => 14,
            Pledge.Auto => autoAge < 3 ? 8 : 3,
            Pledge.Guarantee => 12,
            Pledge.NonPledge => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(questionnaire), pledge,
                "questionnaire.pledge out of range")
        };

        //Наличие других кредитов
        score += otherCredits!.Value
            ? 0
            : goal is Goal.OnLending
                ? 0
                : 15;

        //Сумма
        score += sum switch
        {
            <= 1000000 => 12,
            <= 5000000 => 14,
            <= 10000000 => 8,
            _ => 0
        };

        return score;
    }
}
