import React from 'react';
import Col  from 'react-bootstrap/lib/Col';
import Grid from 'react-bootstrap/lib/Grid';
import Row  from 'react-bootstrap/lib/Row';
import Helmet from "react-helmet/lib/Helmet";

import './MasterLayout.css';


import NavMenu from './NavMenu';
import NotificationView from '../notifications/notificationView';
import Footer from './Footer'



export default props => (
<main className="background-container">
  <Grid fluid>
    <Row>
      <Col sm={3}>
      <NavMenu />
      </Col>
      <Col sm={9}>
        <Helmet titleTemplate="%s | GraphQL-Core">
          <meta charSet="utf-8" />
        </Helmet>
        <article className="content">
          <section>
            <NotificationView />
          </section>
          {props.children}
        </article>
      </Col>
    </Row>
  </Grid>
  <Footer />
</main>
);
