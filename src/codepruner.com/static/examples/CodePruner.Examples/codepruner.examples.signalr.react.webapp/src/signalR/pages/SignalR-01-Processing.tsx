import { useEffect, useMemo } from "react";
import { SignalRConnectionInstance } from "../SignalRConnectionInstance";

export const SignalR01Processing = () => {
  // #region get_signalr_connection
  const connection = useMemo(() => SignalRConnectionInstance, []);
  // #endregion

  // #region invoke_server_processing
  const handleClick = () => {
    console.log("[Click] Sending request to start processing.");
    const requestOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
    };

    var request = fetch(
      "https://localhost:7270/StartFileProcessingSync",
      requestOptions
    );
    request.then(() => {
      console.log(`[Click] Request is done`);
    });
  };

  return (
    <div>
      <button onClick={handleClick}>Start processing:</button>
    </div>
  );

  // #endregion

};

