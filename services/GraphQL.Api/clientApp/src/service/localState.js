const notificationDefaults = {
    notification: {
        __typename: 'Notification',
        message: null,
        created: null
    }
}

const notificationResolver = {
    Mutation: {
        addNotification: (_, { message, created }, { cache }) => {
            // const previous = cache.readQuery({ notificationQuery });
            const data = {notification: { message: message, created: created, __typename: 'Notification' }}; 
            cache.writeData({data});
          return null;
        }
      }
}


export const localState = {
    clientState:  { defaults: {...notificationDefaults}, resolvers: { ...notificationResolver}}
}