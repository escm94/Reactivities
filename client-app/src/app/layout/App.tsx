import React from 'react';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import { observer } from 'mobx-react-lite';
import { Outlet, useLocation } from 'react-router-dom';
import HomePage from '../../features/home/HomePage';

function App() {
  const location = useLocation();

  return (
    <React.Fragment>
      {location.pathname === '/' ? (
        <HomePage />
      ) : (
        <>
          <NavBar />
          <Container style={{ marginTop: '7em' }}>
            {/* will get swapped out with HomePage, ActivitiesForm, etc. */}
            <Outlet />
          </Container>
        </>
      )}
    </React.Fragment>
  );
}

export default observer(App);
