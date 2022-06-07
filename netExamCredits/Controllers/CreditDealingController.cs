using Microsoft.AspNetCore.Mvc;
using netExamCredits.Models;
using netExamCredits.Services;

// ReSharper disable StringLiteralTypo

namespace netExamCredits.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CreditDealingController : ControllerBase
{
    private readonly CriminalStatusCheckerAdapter _criminalStatusChecker;
    private readonly CreditScoresService _creditScores;
    private readonly CreditRateCalculatorService _creditRateCalculator;

    public CreditDealingController(CriminalStatusCheckerAdapter criminalStatusChecker, CreditScoresService creditScores,
        CreditRateCalculatorService creditRateCalculator)
    {
        _criminalStatusChecker = criminalStatusChecker;
        _creditScores = creditScores;
        _creditRateCalculator = creditRateCalculator;
    }

    [HttpPost]
    public async Task<IActionResult> GetCredit([FromBody]Questionnaire questionnaire)
    {
        var criminalStatusIsCorrect = await _criminalStatusChecker.IsCriminalStatusCorrect(questionnaire);
        if (!criminalStatusIsCorrect)
            return new JsonResult(new CreditResultModel(null, false,
                "Статус судимости не соответствует действительности", null));
        
        var score = _creditScores.CountScore(questionnaire);
        
        if (score < 80)
            return new JsonResult(new CreditResultModel(score, false,
                "Кредит не будет выдан, так как набрано 80 или менее очков", null));
        
        var creditRate = _creditRateCalculator.CalculateCreditRate(score);
        return new JsonResult(new CreditResultModel(score, true,
            "Кредит будет выдан, так как набрано более 80 очков", creditRate));
    }
}
