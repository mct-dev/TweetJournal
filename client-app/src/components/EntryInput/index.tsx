import React from "react";
import styled from "styled-components";

import theme from "src/styles/theme";

const Input = styled.input`
  padding: 10px 20px;
  border: none;
  font-size: 30px;
  color: ${theme.textColor};

  @media screen and (max-width: 550px) {
    max-width: calc(100% - 40px);
  }
`;

interface Props extends React.HTMLAttributes<HTMLInputElement> {
  handleSubmit: (val: string) => void;
  handleChange: (val: string) => void;
}

export function EntryInput({ placeholder, handleChange, handleSubmit, ...rest }: Props) {
  const [value, setValue] = React.useState("");
  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setValue(e.target.value);

    if (handleChange) {
      handleChange(e.target.value);
    }
  };
  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      handleSubmit((e.target as HTMLInputElement).value);
      setValue("");
    }
  };

  return (
    <Input
      type="text"
      value={value}
      placeholder={placeholder}
      onChange={handleInputChange}
      onKeyDown={handleKeyDown}
      {...rest}
    />
  );
}
