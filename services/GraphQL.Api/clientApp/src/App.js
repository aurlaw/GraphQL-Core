import React, { Component } from 'react';
import { Switch, Route } from 'react-router-dom'


import MasterLayout from './components/layout/MasterLayout'

import {PlayersFeed} from "./components/playersFeed"
import {TeamFeed} from "./components/teamFeed"
import {LeagueFeed} from "./components/leagueFeed"
import {SeasonFeed} from "./components/seasonFeed"
import {AddPlayerContainer} from "./components/addPlayerContainer"



class App extends Component {
  render() {
    return (
        <MasterLayout>
          <Switch>
            <Route exact path="/" component={PlayersFeed} />
            <Route exact path="/add" component={AddPlayerContainer} />
            <Route path="/teams" component={TeamFeed} />
            <Route path="/leagues" component={LeagueFeed} />
            <Route path="/seasons" component={SeasonFeed} />
          </Switch>
        </MasterLayout>
    );
  }
}


export default App;
