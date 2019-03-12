import React from 'react';
import { Query } from "react-apollo";
import Helmet from "react-helmet/lib/Helmet";

import {leaguesQuery} from '../graphql'
import League from './league'



export const LeagueFeed = () => (
  <Query query={leaguesQuery}>
    {({ loading, error, data }) => {
      if (loading) return <p>Loading...</p>;
      if (error) return <p>Error :(</p>;
       return (
          <React.Fragment>
          <Helmet title="Leagues" />    
          <section>
              <h2>Leagues</h2>
            </section>
            <div className="row p-2">
              {data.leagues.map(l =>
                  <League key={l.id} {...l}/>
              )}
            </div>
        </React.Fragment>
       ); 
    }}
  </Query>
);
