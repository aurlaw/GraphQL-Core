import React from 'react';
import { Query } from "react-apollo";
import Helmet from "react-helmet/lib/Helmet";


import {seasonsQuery} from '../graphql'
import Season from './season'


export const SeasonFeed = () => (
  <Query query={seasonsQuery}>
    {({ loading, error, data }) => {
      if (loading) return <p>Loading...</p>;
      if (error) return <p>Error :(</p>;
       return (
        <React.Fragment>
          <Helmet title="Seasons" />    
          <section>
              <h2>Seasons</h2>
          </section>
          <div className="row p-2">
            {data.seasons.map(l =>
              <Season key={l.id} {...l}/>
            )}
        </div>
        </React.Fragment>
       ); 
    }}
  </Query>
);
