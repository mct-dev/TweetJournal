import React, { Component } from "react";
// import { withRouter } from "react-router-dom";
// import { withAITracking } from "@microsoft/applicationinsights-react-js";
import { ApplicationInsights } from "@microsoft/applicationinsights-web";

// import { reactAppInsightsPlugin } from "src/components/App/App";
import { TelemetryService } from "src/services/TelemetryService";

// eslint-disable-next-line @typescript-eslint/interface-name-prefix
interface ITelemetryContext {
  appInsights: ApplicationInsights;
}

interface Props {
  instrumentationKey: string;
  history: any;
}

export class TelemetryProvider extends Component<Props> {
  context = React.createContext<ITelemetryContext | null>(null);
  state = {
    initialized: false,
  };

  componentDidMount() {
    const { history, instrumentationKey } = this.props;
    const { initialized } = this.state;

    if (!initialized && Boolean(instrumentationKey) && Boolean(history)) {
      const telemetryService = new TelemetryService(instrumentationKey, history);
    }
  }
}

// const withai = withAITracking(reactAppInsightsPlugin, TelemetryProvider);

// export default withRouter(withAITracking(reactAppInsightsPlugin, TelemetryProvider));
