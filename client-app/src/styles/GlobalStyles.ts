import { createGlobalStyle } from "styled-components";

import theme from "./theme";

export default createGlobalStyle`
  html, body {
    background-color: ${theme.backgroundColor}
  }
`;
