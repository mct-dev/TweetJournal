import React from "react";
import styled from "styled-components";

import theme from "src/styles/theme";

const Btn = styled.button`
  background-color: ${theme.white};
  border: 1px solid ${theme.backgroundColor};
  border-radius: 3px;
  box-shadow: 0 2px 4px -1px ${theme.grayLight};
  color: ${theme.textColor};
  cursor: pointer;
  padding: 10px 20px;

  &:active {
    box-shadow: 0 2px 3px -2px ${theme.grayLight};
  }
`;

type Props = React.HTMLAttributes<HTMLButtonElement>;

export function Button({ children, onClick, ...rest }: Props) {
  const handleClick = (e: React.MouseEvent<HTMLButtonElement>) => {
    if (onClick) {
      onClick(e);
    }
  };

  return (
    <Btn onClick={handleClick} {...rest}>
      {children}
    </Btn>
  );
}
