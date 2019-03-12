import React from 'react';
import { Query } from "react-apollo";
import Helmet from "react-helmet/lib/Helmet";
import Link from 'react-router-dom/Link';

import {playersQuery} from '../graphql'
import Player from "./player";

//fetchPolicy="cache-and-network"
export const PlayersFeed = () => (
  <Query query={playersQuery} >
    {({ loading, error, data }) => {
      if (loading) return <p>Loading...</p>;
      if (error) return <p>Error :(</p>;
       return (
          <React.Fragment>
            <Helmet title="Players" />    
            <section>
                <h2>Players <Link to={'/add'} className="btn btn-primary pull-right">+ Player</Link>
                </h2>
              </section>
                <div className="row">
                {data.players.map(p => 
                  <div key={p.id} className="col-md-4">
                    <Player {...p} />
                  </div>
                )}
              </div>
          </React.Fragment>
       ); 
    }}
  </Query>
);
