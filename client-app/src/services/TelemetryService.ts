import { ApplicationInsights } from "@microsoft/applicationinsights-web";
import { ReactPlugin } from "@microsoft/applicationinsights-react-js";

export class TelemetryService {
  appInsights: ApplicationInsights;
  reactPlugin: ReactPlugin;

  constructor(instrumentationKey: string, browserHistory: History) {
    this.reactPlugin = new ReactPlugin();
    this.appInsights = new ApplicationInsights({
      config: {
        instrumentationKey,
        maxBatchInterval: 0,
        disableFetchTracking: false,
        extensions: [this.reactPlugin],
        extensionConfig: {
          [this.reactPlugin.identifier]: {
            history: browserHistory,
          },
        },
      },
    });
  }

  initialize() {
    this.appInsights.addTelemetryInitializer((telemetryItem) => {
      if (telemetryItem.data) {
        // eslint-disable-next-line no-param-reassign
        telemetryItem.data.autoTrackPageVisitTime = true;
      }
    });
    this.appInsights.loadAppInsights();
  }

  getAppInsights() {
    return this.appInsights;
  }
}
