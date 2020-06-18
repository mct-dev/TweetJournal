import React from "react";
import { createBrowserHistory } from "history";

import { Router } from "../../routes/Router";

// const browserHistory = createBrowserHistory({ basename: "" });
// export const reactAppInsightsPlugin = new ReactPlugin();
// export const appInsights = new AppInsights({
//   config: {
//     instrumentationKey: "fb3cc23e-9c93-4969-97ef-5f96669e47e7",
//     extensions: [reactAppInsightsPlugin],
//     extensionConfig: {
//       [reactAppInsightsPlugin.identifier]: { history: browserHistory },
//     },
//   },
// });
// appInsights.Init();

const customHistory = createBrowserHistory();

function App() {
  return (
    <React.StrictMode>
      <Router history={customHistory} />
    </React.StrictMode>
  );
}

export default App;
