namespace CriminalStatus;

public interface ICriminalStatusChecker
{
    public Task<bool> IsCriminalStatusCorrect(int passportSeries, bool criminalRecord);
}
