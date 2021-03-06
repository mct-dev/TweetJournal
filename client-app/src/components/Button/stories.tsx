import React from "react";
import styled from "styled-components";
import { action } from "@storybook/addon-actions";
import { text } from "@storybook/addon-knobs";

import { Button } from ".";
import { centered } from "src/styles/story";

export default {
  component: Button,
  decorators: [centered],
  title: "Components/EntryInput",
  parameters: {
    backgrounds: [{ name: "CRM", value: "#f5f5f5", default: true }],
  },
};

const Container = styled.div`
  width: 80%;
  display: flex;
  justify-content: center;
`;

export const Default = () => {
  return (
    <Container>
      <Button placeholder={text("Placeholder", "Enter text here...")} onChange={action("onChange")} />
    </Container>
  );
};
