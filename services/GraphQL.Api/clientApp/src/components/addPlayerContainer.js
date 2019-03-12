import React from 'react';
import { Mutation } from "react-apollo";
import Helmet from "react-helmet/lib/Helmet";


import {playersQuery, createPlayerMutation} from '../graphql'

import PlayerAdd from "./playerAdd"


import "./addPlayerContainer.css"

export const AddPlayerContainer = (props) => (
        <React.Fragment>
            <Helmet title="Add Player" />  
            <section>
                <h2>Add Player</h2>
            </section>
            <div className="row p-2">
                <Mutation 
                    mutation={createPlayerMutation}
                    update={(cache, { data: { createPlayer } }) => {
                        const query = playersQuery;
                        const { players } = cache.readQuery({ query: query });
                        let updatePlayers =players.concat([createPlayer]);
                        // console.log('cache.readQuery', players);
                        // console.log('cache add', createPlayer);
                        // console.log('cache add', updatePlayers);
                        cache.writeQuery({
                          query: query,
                          data: {players:updatePlayers}
                        });
                      }}    

                      onCompleted={(data) => {
                        console.log('post-success', data);
                        props.history.push('/');
                      }}            
                    >
                    {(createPlayer, { loading, error }) =>  (
                        <React.Fragment>
                        <PlayerAdd onHandleSubmission={playerAdd => {
                            // console.log('pre-onHandleSubmission', playerAdd);
                            createPlayer({variables: {player: playerAdd.player, skaterStats: playerAdd.skaterStats}});
                        }} />
                        {loading && <p>Saving...</p>}
                        {error && <div className="panel panel-danger">
                                <div className="panel-heading">
                                    <h3 className="panel-title">Error</h3>
                                </div>
                                <div className="panel-body">
                                    <p>Error: {error}</p>
                                </div>
                            </div>
                        }

                        </React.Fragment>
                    )}
                </Mutation>
            </div>
        </React.Fragment>
);