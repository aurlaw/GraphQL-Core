import React, { Component } from 'react';
import {HubConnectionBuilder} from '@aspnet/signalr/dist/browser/signalr';

import withNotification from './withNotification'


class NotificationService extends Component {
    // constructor(props) {
    //     super(props);
    //     // this.state = {
    //     //     connection: null
    //     // };
    // }
    componentDidMount() {
        const connection = new HubConnectionBuilder()
            .withUrl("/notifications")
            .build();
        connection.on("broadcastNotification", data => {
            console.log(data);
            this.props.handleUpdateNotification(data, new Date().toISOString());
        });
        connection.start();
        //  this.setState({connection: connection}); 
    }
    render() {
        return (
            <React.Fragment>
                {this.props.children}
            </React.Fragment>
        )
    }

}
export default withNotification(NotificationService, false);