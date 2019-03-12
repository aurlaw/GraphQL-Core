import React from 'react';

import withNotification from './withNotification'


function NotificationButton(props) {
    const { handleUpdateNotification, ...rest } = props;
    return     <button {...rest} onClick={e => {
        e.preventDefault();
        props.handleUpdateNotification("test message", new Date().toISOString());
    }}>Test Notification</button>

}

// const NotificationButton = (props) => (
//     <button {...props} onClick={e => {
//         e.preventDefault();
//         props.handleUpdateNotificati on("test message", new Date().toISOString());
//     }}>Test Notification</button>
// );

export default withNotification(NotificationButton, false);

//    {};
