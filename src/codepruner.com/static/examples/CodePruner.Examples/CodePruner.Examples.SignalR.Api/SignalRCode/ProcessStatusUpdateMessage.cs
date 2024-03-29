namespace CodePruner.Examples.SignalR.Api.SignalRCode;

public record ProcessStatusUpdateMessage(int ProcessId, ProcessStatus CurrentStatus);