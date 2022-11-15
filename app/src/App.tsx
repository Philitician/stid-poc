import { useEffect, useState } from "react";
import reactLogo from "./assets/react.svg";
import "./App.css";
import { useMsal, useAccount } from "@azure/msal-react";

import axios from "axios";
import { useQuery } from "@tanstack/react-query";
const env = import.meta.env;

// const scopes = [env.VITE_API_SCOPE_READ, env.VITE_API_SCOPE_WRITE];
const scopes = [env.VITE_API_SCOPE_READ];

const createHeaders = (accessToken: string) => ({
  headers: {
    Authorization: `Bearer ${accessToken}`,
  },
});

function App() {
  const { instance } = useMsal();
  console.log("instance", instance);
  const account = useAccount();
  console.log("account", account);
  const { data, refetch } = useQuery<string>(
    ["data"],
    async () => {
      const url = import.meta.env.VITE_API_URL;
      const token = await instance.acquireTokenSilent({ scopes });
      const headers = createHeaders(token.accessToken);
      console.log("scopes", scopes);
      console.log("headers", headers);
      const { data } = await axios.get(url, headers);
      return data;
    },
    {
      enabled: false,
    }
  );

  return (
    <div className="App">
      {account && (
        <div style={{ margin: 10 }}>
          <div style={{ display: "flex", margin: 4 }}>
            <button
              style={{ margin: 4 }}
              onClick={() => instance.logoutRedirect()}
            >
              Logg ut {account.username ?? account.name}
            </button>
            <button style={{ margin: 4 }} onClick={() => refetch()}>
              Fetch data
            </button>
          </div>
          {data && (
            <div style={{ maxWidth: 300 }}>Protected API Message: {data}</div>
          )}
        </div>
      )}
      {!account && (
        <button onClick={() => instance.loginRedirect()}>Login</button>
      )}
    </div>
  );
}

export default App;
