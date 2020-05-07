import React from 'react'

export const TextInput: React.FC<TextInputProps> = ({ placeholder = '', onChange = () => null }) => {
  return (
    <input type="text" placeholder={placeholder} onChange={onChange} />
  )
}

export interface TextInputProps {
  placeholder: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => any;
}
