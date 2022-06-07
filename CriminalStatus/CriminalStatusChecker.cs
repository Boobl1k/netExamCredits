namespace CriminalStatus;

public class CriminalStatusChecker : ICriminalStatusChecker
{
    public async Task<bool> IsCriminalStatusCorrect(int passportSeries, bool criminalRecord)
    {
        //если серия паспорта четная, человек должен быть судим
        return (passportSeries % 2 == 0) == criminalRecord;
    }
}
