using Microsoft.AspNetCore.SignalR;

namespace CodePruner.Examples.SignalR.Api.SignalRCode;

public class StronglyTypedProcessingHub : Hub<IProcessingClient>
{

}


public interface IProcessingClient
{
    Task ProcessStatusUpdate(ProcessStatusUpdateMessage processStatusUpdateMessage);
}
