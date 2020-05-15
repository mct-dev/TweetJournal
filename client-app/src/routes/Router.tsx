import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";

import { Home } from "./Home/Home";

export function Router() {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/">
          <Home />
        </Route>
      </Switch>
    </BrowserRouter>
  );
}
