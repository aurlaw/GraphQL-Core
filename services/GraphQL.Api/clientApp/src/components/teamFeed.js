import React from 'react';
import { Query } from "react-apollo";
import Helmet from "react-helmet/lib/Helmet";


import {teamsQuery} from '../graphql'
import Team from './team'

export const TeamFeed = () => (
  <Query query={teamsQuery}>
    {({ loading, error, data }) => {
      if (loading) return <p>Loading...</p>;
      if (error) return <p>Error :(</p>;
       return (
        <React.Fragment>
          <Helmet title="Teams" />    
          <section>
              <h2>Teams</h2>
            </section>
            <div className="row p-2">
              {data.teams.map(t =>
                <Team key={t.id} {...t}/>
              )}
            </div>
        </React.Fragment>
       ); 
    }}
  </Query>
);
