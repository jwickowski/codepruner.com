import { useEffect, useState } from "react";
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
export const SignalR01Processing = () => {
  const [connection, setConnection] = useState<HubConnection>();
  const [logs, setLogs] = useState<string[]>([]);

  const log = (logText: string) => {
    console.log(logText);
    var now = new Date();
    const hours = String(now.getHours()).padStart(2, "0");
    const minutes = String(now.getMinutes()).padStart(2, "0");
    const seconds = String(now.getSeconds()).padStart(2, "0");

    const formattedTime = `${hours}:${minutes}:${seconds}`;
    setLogs([...logs, `${formattedTime}: ${logText}]`]);
  };

  const hubUrl = "https://localhost:7270/StronglyTypedProcessingHub";
  const handleClick = () => {
    log("Start processing");
  };

  useEffect(() => {
    console.log("Initializing SignalR connection.");
    const connection = new HubConnectionBuilder()
      .withUrl(hubUrl)
      .configureLogging(LogLevel.Information)
      .build();

    console.log("Initialized SignalR connection.");
    setConnection(connection);

    console.log("Cconnection set in state.");
  }, []);

  useEffect(() => {
    if (!connection) {
      log("Connection is not set.");
      return;
    }

    asyncHandleSignalrconnection();
  }, [connection]);
  const asyncHandleSignalrconnection = async () => {
    try {
      log("Try connecting to SignalR hub");
      await connection!.start();
      log("Connected to SignalR hub");
    } catch (err) {
      log(`Error connecting to SignalR hub: ${err}`);
    }
  };

  return (
    <div>
      <button onClick={handleClick}>Start processing:</button>
      <div>
        {logs.map((log, index) => (
          <div key={index}>index: {log}</div>
        ))}
      </div>
    </div>
  );
};
