import React from "react";
import ReactDOM, { createRoot } from "react-dom/client";
import { Auth0Provider } from "@auth0/auth0-react";
import App from "./App";
import "bootstrap/dist/css/bootstrap.min.css";

const root = createRoot(document.getElementById("root")!);

root.render(
    <Auth0Provider
        domain="dev-r617abi65lh733n3.us.auth0.com"
        clientId="34JBPw7oQpk6YzQXg8WusCyWV52fXHLH"
        authorizationParams={{
            redirect_uri: window.location.origin,
        }}
    >
        <App />
    </Auth0Provider>,
);
