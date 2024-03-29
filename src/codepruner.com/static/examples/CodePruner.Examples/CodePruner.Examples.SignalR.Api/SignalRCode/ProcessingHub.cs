using Microsoft.AspNetCore.SignalR;

namespace CodePruner.Examples.SignalR.Api.SignalRCode
{
    public class ProcessingHub: Hub
    {
        public void ProcessStatusUpdate(ProcessStatusUpdateMessage processStatusUpdateMessage)
        {
            Clients.All.SendAsync("ProcessStatusUpdate", processStatusUpdateMessage);
        }   
    }
}
