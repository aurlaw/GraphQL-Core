import ApolloClient from "apollo-boost";

import {localState} from './localState';


// console.log(localState);
export const client = new ApolloClient({
  uri: "/graphql",
  ...localState
});