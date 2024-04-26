import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";

const configureSignalRClient = () => {
  const hubUrl = "https://localhost:7270/StronglyTypedProcessingHub";
  console.log("[SignalConnection] Initializing SignalR connection.");

  var connection = new HubConnectionBuilder()
    .withUrl(hubUrl)
    .configureLogging(LogLevel.Information)
    .build();

  console.log("[SignalConnection] Connecting to SignalR hub");
  connection
    .start()
    .then(() => {
      console.log("[SignalConnection] Connected to SignalR hub");
    })
    .catch((err) => {
      console.log(`[SignalConnection] Error connecting to SignalR hub: ${err}`);
    });

    connection.on("ProcessStatusUpdate", (message: {processId: string, currentStatus: string}) => {
        console.log(`${message.processId} - ${message.currentStatus}`);
    });

  return connection;
};

 export const SignalRConnectionInstance: HubConnection = configureSignalRClient();
