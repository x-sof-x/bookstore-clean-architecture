namespace BookStore.Application.Interfaces
{
    public interface ILogsService
    {
        bool TriggerInfo();
        bool TriggerDebug();
        bool TriggerWarning();
        bool TriggerError();
    }
}
