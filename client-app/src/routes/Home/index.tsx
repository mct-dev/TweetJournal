import React from "react";

import GlobalStyles from "src/styles/GlobalStyles";
import { NewEntry } from "./NewEntry";

const _Home = () => {
  return (
    <div>
      <GlobalStyles />
      <NewEntry />
    </div>
  );
};

export const Home = _Home;
