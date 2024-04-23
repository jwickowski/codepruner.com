namespace CodePruner.Examples.SignalR.Api.Processing;

public class FileProcessor
{
    public async Task<ProcessStatusState> ProcessFile(FileProcessId fileProcessId, ProcessStatus currentStatus)
    {
        var nextStatus = currentStatus switch
        {
            ProcessStatus.InQueue => ProcessStatus.Started,
            ProcessStatus.Started => ProcessStatus.Fetched,
            ProcessStatus.Fetched => ProcessStatus.Processed,
            ProcessStatus.Processed => ProcessStatus.Saved,
            ProcessStatus.Saved => ProcessStatus.Done,
            _ => ProcessStatus.Unknown
        };
        await Task.Delay(1000);

        return new ProcessStatusState(fileProcessId, nextStatus);

    }
}