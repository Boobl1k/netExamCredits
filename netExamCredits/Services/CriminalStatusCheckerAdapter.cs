using CriminalStatus;
using netExamCredits.Models;

namespace netExamCredits.Services;

public class CriminalStatusCheckerAdapter
{
    private readonly ICriminalStatusChecker _criminalStatusChecker;

    public CriminalStatusCheckerAdapter(ICriminalStatusChecker criminalStatusChecker) =>
        _criminalStatusChecker = criminalStatusChecker;

    public async Task<bool> IsCriminalStatusCorrect(Questionnaire questionnaire) =>
        await _criminalStatusChecker.IsCriminalStatusCorrect(
            questionnaire.PassportSeries!.Value,
            questionnaire.CriminalRecord!.Value);
}
