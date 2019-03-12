import gql from "graphql-tag";


export const notificationQuery = gql`
      {
        notification  @client {
            message
            created
        }
      }    
`;


export const addNotificationMutation = gql`
    mutation AddNotification($message: String!, $created:String!) {
        addNotification(message: $message, created: $created) @client
    }
`;