using CodePruner.Examples.SignalR.Api.Processing;

namespace CodePruner.Examples.SignalR.Api.SignalRCode;

public record ProcessStatusUpdateMessage(Guid ProcessId, ProcessStatus CurrentStatus);