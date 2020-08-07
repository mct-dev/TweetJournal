import React, { useEffect } from "react";
import { assign } from "xstate";
import { useMachine } from "@xstate/react";

import { EntryInput } from "src/components/EntryInput";
import { Button } from "src/components/Button";
import { Entry } from "src/domain/Entry";
import { entryMachine } from "./machine";
import { postEntry } from "src/access/entry.access";

const entries: Entry[] = [
  {
    id: "123",
    content: "fake content",
    createdDate: "2019-12-04",
    modifiedDate: "2020-02-01",
  },
];

const mockFetchEntries = () => {
  return Promise.resolve([]);
};

const submitEntry = async (content: string) => {
  const response = await postEntry(content);
};

export function NewEntry() {
  const [state, send, service] = useMachine(entryMachine, {
    services: {
      fetchEntries: () => mockFetchEntries(),
      submitEntry: (context, _) => submitEntry(context.inputValue),
    },
  });

  useEffect(() => {
    const subscription = service.subscribe((state) => {
      console.log(state);
    });

    return subscription.unsubscribe;
  }, [service]);

  useEffect(() => {
    send({ type: "FETCH" });
  }, [send]);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) =>
    send({ type: "SET_INPUT_VALUE", payload: e.target.value });

  return (
    <div>
      <EntryInput onChange={handleInputChange} />
      <Button onClick={() => send({ type: "SUBMIT" })}>Submit</Button>
      <EntryList entries={state.context.entries} />
    </div>
  );
}

interface EntryListProps extends React.HTMLAttributes<HTMLUListElement> {
  entries: Entry[];
}
const EntryList: React.FC<EntryListProps> = ({ entries }) => (
  <ul>
    {entries.map((entry, ix) => (
      <div key={ix}>
        <li>{entry.content}</li>
        <p>Created: {entry.createdDate}</p>
        <p>Modified: {entry.modifiedDate}</p>
      </div>
    ))}
  </ul>
);
