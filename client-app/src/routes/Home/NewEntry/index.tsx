import React, { useEffect } from "react";
import { assign } from "xstate";
import { useMachine } from "@xstate/react";

import { EntryInput } from "src/components/EntryInput";
import { Button } from "src/components/Button";
import { Entry } from "src/domain/Entry";
import { entryMachine } from "./machine";

const entries: Entry[] = [
  {
    id: "123",
    content: "fake content",
    createdDate: "2019-12-04",
    modifiedDate: "2020-02-01",
  },
];

const mockFetchEntries = () => {
  return Promise.resolve(entries);
};

const mockSubmitEntry = async (entry: Entry) => {
  entries.push(entry);
  return Promise.resolve(entry);
};

const buildEntry = (content: string): Entry => ({
  id: String(Math.random() * 100000),
  content,
  createdDate: new Date().toLocaleString(),
  modifiedDate: new Date().toLocaleString(),
});

export function NewEntry() {
  const [state, send, service] = useMachine(entryMachine, {
    services: {
      fetchEntries: () => mockFetchEntries(),
      submitEntry: (context, _) => mockSubmitEntry(buildEntry(context.inputValue)),
    },
  });

  useEffect(() => {
    const subscription = service.subscribe((state) => {
      console.log(state.context);
    });

    // eslint-disable-next-line @typescript-eslint/unbound-method
    return subscription.unsubscribe;
  }, [service]);

  useEffect(() => {
    send({ type: "FETCH" });
  }, [send]);

  const handleInputChange = (value: string) => send({ type: "SET_INPUT_VALUE", payload: value });

  return (
    <div>
      <EntryInput handleChange={handleInputChange} handleSubmit={() => send({ type: "SUBMIT" })} />
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
