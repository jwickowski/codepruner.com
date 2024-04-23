using System.Collections.Concurrent;

namespace CodePruner.Examples.SignalR.Api.Processing;

public class FileProcessingStore
{
    private readonly ConcurrentQueue<ProcessStatusState> _processQueue = new();

    public bool HasNext => _processQueue.Count > 0;
    public ProcessStatusState Next()
    {
        if (_processQueue.TryDequeue(out var processStatusState))
        {
            return processStatusState;
        }

        return ProcessStatusState.Empty;
    }

    public void Add(FileProcessId processId)
    {
        _processQueue.Enqueue(new ProcessStatusState(processId, ProcessStatus.InQueue));
    } 
    public void Add(ProcessStatusState processState)
    {
        _processQueue.Enqueue(processState);
    }
}

public record ProcessStatusState(FileProcessId Id, ProcessStatus Status)
{
    public static readonly ProcessStatusState Empty
        = new ProcessStatusState(new FileProcessId(Guid.Empty), ProcessStatus.Unknown);
};