import React from "react";
import { RouterProps } from "react-router";
import { Router as ReactRouter, Switch, Route } from "react-router-dom";

import { Home } from "./Home";
import { Login } from "./Login";

export const Router: React.FC<RouterProps> = ({ history }) => {
  return (
    <ReactRouter history={history}>
      <Switch>
        <Route path="/login">
          <Login />
        </Route>
        <Route path="/">
          <>
            <Home />
          </>
        </Route>
      </Switch>
    </ReactRouter>
  );
};
