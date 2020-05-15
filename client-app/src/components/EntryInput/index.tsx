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

type Props = React.HTMLAttributes<HTMLInputElement>;

export function EntryInput({ placeholder, onChange, ...rest }: Props) {
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (onChange) {
      onChange(e);
    }
  };

  return <Input type="text" placeholder={placeholder} onChange={handleChange} {...rest} />;
}
