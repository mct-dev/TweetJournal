import { Machine, assign } from "xstate";

import { Entry } from "src/domain/Entry";

export interface NEContext {
  inputValue: string;
  entries: Entry[];
  isLoading: boolean;
  errorMessage: string;
}

export interface NEStateSchema {
  states: {
    idle: {};
    submitting: {};
    fetching: {};
    failure: {};
  };
}

export type NEEvent =
  | { type: "FETCH" }
  | { type: "SUBMIT" }
  | { type: "FAIL" }
  | { type: "SUCCESS" }
  | { type: "CLEAR" }
  | { type: "SET_INPUT_VALUE"; payload: string };

export const entryMachine = Machine<NEContext, NEStateSchema, NEEvent>(
  {
    id: "newEntry",
    initial: "idle",
    context: {
      inputValue: "",
      entries: [],
      isLoading: false,
      errorMessage: "",
    },
    states: {
      idle: {
        on: {
          FETCH: "fetching",
          SET_INPUT_VALUE: {
            actions: "setInputValue",
          },
          SUBMIT: "submitting",
        },
      },
      submitting: {
        invoke: {
          src: "submitEntry",
          onDone: {
            target: "idle",
            actions: ["resetInputValue"],
          },
          onError: {
            target: "failure",
            actions: [
              "resetInputValue",
              assign({
                errorMessage: (_, { data }) => data,
              }),
            ],
          },
        },
      },
      fetching: {
        invoke: {
          src: "fetchEntries",
          onDone: {
            target: "idle",
            actions: "setEntries",
          },
          onError: {
            target: "failure",
            actions: assign({
              errorMessage: (_, { data }) => data,
            }),
          },
        },
      },
      failure: {
        on: {
          CLEAR: "idle",
          SUBMIT: "submitting",
        },
      },
    },
  },
  {
    actions: {
      setInputValue: assign<NEContext, any>({
        inputValue: (_, { payload }) => payload,
      }),
      resetInputValue: assign<NEContext, any>({
        inputValue: "",
      }),
      setEntries: assign<NEContext, any>({
        entries: (_, { data }) => data,
      }),
    },
  }
);
