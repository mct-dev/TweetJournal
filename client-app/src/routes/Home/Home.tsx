import React from "react";

import { EntryInput } from "src/components/EntryInput";
import GlobalStyles from "src/styles/GlobalStyles";

export function Home() {
  return (
    <div>
      <GlobalStyles />
      <h1>Home page!</h1>
      <EntryInput />
    </div>
  );
}
