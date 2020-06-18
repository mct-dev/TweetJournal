import React from "react";
import { RouterProps } from "react-router";
import { Router as ReactRouter, Switch, Route } from "react-router-dom";

import { Home } from "./Home";
import { About } from "./About";
import { Contact } from "./Contact";

export const Router: React.FC<RouterProps> = ({ history }) => {
  return (
    <ReactRouter history={history}>
      <Switch>
        <Route path="/contact">
          <>
            <Contact />
          </>
        </Route>
        <Route path="/about">
          <>
            <About />
          </>
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
