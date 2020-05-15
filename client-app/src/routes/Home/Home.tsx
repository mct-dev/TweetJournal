import React from "react";

import GlobalStyles from "src/styles/GlobalStyles";
import { NewEntry } from "./NewEntry";

export function Home() {
  return (
    <div>
      <GlobalStyles />
      <NewEntry />
    </div>
  );
}
