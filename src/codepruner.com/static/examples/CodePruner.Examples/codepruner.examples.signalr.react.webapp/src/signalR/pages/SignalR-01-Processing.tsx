import { useEffect, useMemo, useState } from "react";
import { getSignalRConnection } from "../SignalConnection";
export const SignalR01Processing = () => {
  const [logs, setLogs] = useState<string[]>([]);
  const connection = useMemo(() => getSignalRConnection(), []);
  const log = (logText: string) => {
    console.log(logText);
    var now = new Date();
    const hours = String(now.getHours()).padStart(2, "0");
    const minutes = String(now.getMinutes()).padStart(2, "0");
    const seconds = String(now.getSeconds()).padStart(2, "0");

    const formattedTime = `${hours}:${minutes}:${seconds}`;
    setLogs((currentLogs) => [...currentLogs, `${formattedTime}: ${logText}`]);
  };

  useEffect(() => {
    if (!connection) {
      log("[Effect] SignalR connection doesn't exist");
      return;
    }

    log("[Effect] Setting up SignalR event handlers.");
    connection.on("ProcessStatusUpdate", (message: any) => {
      log(`[Effect][SignalR] ProcessStatusUpdate: ${message.currentStatus}`);
    });

    return () => {
      log("[Effect] Removing SignalR event handlers.");
      connection.off("ProcessStatusUpdate");
    };
  }, [connection]);

  const handleClick = () => {
    log("[Click] Sending request to start processing.");
    const requestOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
    };

    var request = fetch(
      "https://localhost:7270/StartFileProcessingSync",
      requestOptions
    );
    request.then(() => {
      log(`[Click] Request is done`);
    });
  };

  return (
    <div>
      <button onClick={handleClick}>Start processing:</button>
      <div>
        {logs.map((log, index) => (
          <div key={index}>
            {" "}
            {index}: {log}
          </div>
        ))}
      </div>
    </div>
  );
};
