namespace CodePruner.Examples.SignalR.Api.Processing;

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