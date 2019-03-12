import React, { Component } from 'react';
import { Mutation } from "react-apollo";

import {DeleteBtn} from "./deleteBtn"
import {playersQuery, deletePlayerMutation} from '../graphql'



import './player.css';

function adjustHeight(num) {
    return num + 100;
}

function generateStyle(id, arr) {
   return arr.map((stat, index) => `
        .details${id} .card .bar.bar${index} {
            height: ${adjustHeight(stat.points)}px;
            transition-delay: 0.3s;
            margin-top: -${adjustHeight(stat.points)}px;
        }
    `
    ).join(' ');
}

function getPhoto(id) {
    return {
        background: `url("/images/${id}.jpg") no-repeat center center`,
        backgroundSize: 'cover'
      };
}

class Player extends Component {
    constructor(props) {
        super(props);
        this.state = {
            tilted: false,
          }        
        this.handleToggle = this.handleToggle.bind(this);
        // this.handleDelete = this.handleDelete.bind(this);
    }
    handleToggle() {
        this.setState((prevState, props) => {
            return {tilted: !prevState.tilted};
          })        
    }
    // handleDelete() {
    //     console.log("delete", this.props.id);
    // }


    render() {
        var detailsClass = this.state.tilted ? "details details" + this.props.id : "";
        var totalPoints = this.props.skaterSeasonStats.reduce(function (acc, obj) { return acc + obj.points; }, 0);;
        return (
            <React.Fragment>
                <style dangerouslySetInnerHTML={{__html: generateStyle(this.props.id, this.props.skaterSeasonStats)}}/>
                <div className={detailsClass}>
                    <div className="card" onClick={() => this.handleToggle()}>
                        <div className="photo" style={getPhoto(this.props.id)}></div>
                        <h2>{this.props.name}</h2>
                        <p>{this.props.height} - {this.props.weightLbs}</p>
                        <div className="chart">
                            {this.props.skaterSeasonStats.map((stat, index) => 
                                <div key={stat.id} className={`bar bar${index}`}><span>{stat.points} pts</span></div>
                            )}
                        </div>
                        <h3>{totalPoints} pts</h3>
                    </div>
                    <Mutation 
                        mutation={deletePlayerMutation}
                        update={(cache, { data: { deletePlayer } }) => {
                            const query = playersQuery;
                            const { players } = cache.readQuery({ query: query });                           
                            let updatePlayers =players.filter(p => p.id !== deletePlayer.id);
                            // console.log('cache.readQuery', players);
                            // console.log('cache delete', deletePlayer);
                            // console.log('cache delete', updatePlayers);
                            cache.writeQuery({
                                query: query,
                                data: {players:updatePlayers}
                            });
                        }}    
                        >
                        {(deletePlayer, { loading, error }) =>  (
                            <DeleteBtn message={`Do you want to delete ${this.props.name}?`} onDelete={() => {
                                deletePlayer({variables: {playerId: this.props.id}});
                            }} 
                            />                
                        )}
                    </Mutation>
                </div>  
            </React.Fragment>
        )
    }
}

export default Player;
