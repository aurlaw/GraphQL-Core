import React, { Component } from 'react';
import { Query, Mutation } from "react-apollo";

import {notificationQuery,addNotificationMutation} from '../../graphql/local'
import {convertToDate} from '../../utils'


function withNotification(WrappedComponent, useQuery) {
    return class extends Component {
        constructor(props) {
            super(props);
            this.state = {
                useQuery: useQuery
            };
          }        

          render() {
            let showQuery = this.state.useQuery;

            return <Mutation mutation={addNotificationMutation}>
                {(addNotification, { loading, error }) =>  (
                    <React.Fragment>
                     {showQuery && 
                    <Query query={notificationQuery} >
                        {({ loading, error, data }) => {
                            let {message, created} = data.notification;
                            let createdDate = new Date();
                            if(created !== null) {
                                createdDate = convertToDate(created);
                            }
                        return (
                            <WrappedComponent message={message} createdDate={createdDate} error={error} handleUpdateNotification={(m,c) => {
                                console.log(m);
                                console.log(c);
                                addNotification({variables: {message: m, created: c}});
                            }}  {...this.props}  />
                        ); 
                        }}
                    </Query>
                     }
                     {!showQuery && 
                            <WrappedComponent handleUpdateNotification={(m,c) => {
                                console.log(m);
                                console.log(c);
                                addNotification({variables: {message: m, created: c}});
                            }}  {...this.props}  />
                    }
                    </React.Fragment>
                )}
            </Mutation>
          }
    };
}

export default withNotification;