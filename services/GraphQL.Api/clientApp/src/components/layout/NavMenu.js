import React from 'react';
import Link from 'react-router-dom/Link';
import Glyphicon from 'react-bootstrap/lib/Glyphicon';
import Nav from 'react-bootstrap/lib/Nav';
import Navbar from 'react-bootstrap/lib/Navbar';
import NavItem from 'react-bootstrap/lib/NavItem';
import LinkContainer from 'react-router-bootstrap/lib/LinkContainer';
import './NavMenu.css';


import TestButton from '../notifications/notificationButton';

export default props => (
  <aside>
    <Navbar inverse fixedTop fluid collapseOnSelect className="navbar-inverse-alt">
      <Navbar.Header>
        <Navbar.Brand>
          <Link to={'/'}>GraphQL-Core</Link>

        </Navbar.Brand>
        <Navbar.Toggle />
      </Navbar.Header>
      <Navbar.Collapse>
        <Nav>
          <LinkContainer to={'/'} exact>
            <NavItem>
              <Glyphicon glyph='home' /> Home
            </NavItem>
          </LinkContainer>
          <LinkContainer to={'/leagues'}>
            <NavItem>
              <Glyphicon glyph='king' /> Leagues
            </NavItem>
          </LinkContainer>
          <LinkContainer to={'/teams'}>
            <NavItem>
              <Glyphicon glyph='list-alt' /> Teams
            </NavItem>
          </LinkContainer>
          <LinkContainer to={'/seasons'}>
            <NavItem>
              <Glyphicon glyph='calendar' /> Seasons
            </NavItem>
          </LinkContainer>
          <NavItem>
          <TestButton className="btn btn-info" />

          </NavItem>
        </Nav>
      </Navbar.Collapse>
    </Navbar>

  </aside>

);
