namespace CodePruner.Examples.SignalR.Api.SignalRCode;

public enum ProcessStatus
{
    Unknown = 0,
    InQueue,
    Started,
    Fetched,
    Processed,
    Saved,
    Done
}