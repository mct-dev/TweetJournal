import React from "react";
import styled from "styled-components";
import { action } from "@storybook/addon-actions";
import { text } from "@storybook/addon-knobs";

import { EntryInput } from ".";
import { centered } from "src/styles/story";

export default {
  component: EntryInput,
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
      <EntryInput
        placeholder={text("Placeholder", "Enter text here...")}
        handleChange={action("handleChange")}
        handleSubmit={action("handleSubmit")}
      />
    </Container>
  );
};
