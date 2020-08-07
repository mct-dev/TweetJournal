import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";

import { Home } from "./Home";
import { Login } from "./Login";

export function Router() {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/login">
          <Login />
        </Route>
        <Route path="/">
          <Home />
        </Route>
      </Switch>
    </BrowserRouter>
  );
}
