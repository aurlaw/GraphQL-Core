import React from 'react';

import withNotification from './withNotification'
import {formatDate} from '../../utils'


import './notificationView.css';


const NotificationView = (props) => (
    <React.Fragment>
    {props.error && <div className="notif-view-error">Error :(. {props.error}</div>}
    {props.message !== null && <div className="notif-view">
        {props.message} - {formatDate(props.createdDate)}
    </div>}
    </React.Fragment>
);

export default withNotification(NotificationView, true);