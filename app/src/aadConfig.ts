import {
  AuthenticationResult,
  Configuration,
  EventMessage,
  EventType,
  PublicClientApplication,
} from "@azure/msal-browser";

const env = import.meta.env;
const signUpSignIn = "B2C_1_signup_signin";
const tenant = env.VITE_AAD_TENANT;
export const b2cPolicies = {
  authorities: {
    signUpSignIn: `https://${tenant}.b2clogin.com/${tenant}.onmicrosoft.com/${signUpSignIn}`,
  },
};

const msalConfig: Configuration = {
  auth: {
    clientId: env.VITE_AAD_CLIENT_ID,
    authority: b2cPolicies.authorities.signUpSignIn, // Choose SUSI as your default authority.
    knownAuthorities: [`${tenant}.b2clogin.com`], // Mark your B2C tenant's domain as trusted.
    redirectUri: "/", // You must register this URI on Azure Portal/App Registration. Defaults to window.location.origin
    // postLogoutRedirectUri: "/", // Indicates the page to navigate after logout.
    // navigateToLoginRequestUrl: false, // If "true", will navigate back to the original request location before processing the auth code response.
  },
  cache: {
    cacheLocation: "sessionStorage", // This configures where your cache will be stored
    storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
  },
};

const msalInstance = new PublicClientApplication(msalConfig);

msalInstance.addEventCallback((event: EventMessage) => {
  if (event.eventType === EventType.LOGIN_SUCCESS && event.payload) {
    const payload = event.payload as AuthenticationResult;
    msalInstance.setActiveAccount(payload.account);
  }
});

export default msalInstance;
