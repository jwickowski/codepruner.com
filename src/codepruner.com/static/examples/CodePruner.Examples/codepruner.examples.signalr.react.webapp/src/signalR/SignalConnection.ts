import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";

const prepareConnection = () => {
  const hubUrl = "https://localhost:7270/StronglyTypedProcessingHub";
  console.log("[SignalConnection] Initializing SignalR connection.");

  var newConnection = new HubConnectionBuilder()
    .withUrl(hubUrl)
    .configureLogging(LogLevel.Information)
    .build();

  console.log("[SignalConnection] Connecting to SignalR hub");
  newConnection
    .start()
    .then(() => {
      console.log("[SignalConnection] Connected to SignalR hub");
    })
    .catch((err) => {
      console.log(`[SignalConnection] Error connecting to SignalR hub: ${err}`);
    });

  return newConnection;
};

let connectionInstance: HubConnection = prepareConnection();

export const getSignalRConnection = () => {
  if (connectionInstance) {
    return connectionInstance;
  }

  connectionInstance = prepareConnection();
  const events = new Map<string, ((message: any) => void)[]>();
  events.set("ProcessStatusUpdate", []);

  connectionInstance.on("ProcessStatusUpdate", (message: any) => {
    events.get("ProcessStatusUpdate")?.forEach((callback) => {
      callback(message);
    });
  });

  return connectionInstance;
};
