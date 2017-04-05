import React from 'react';
import { render } from 'react-dom';
import { createBrowserHistory } from 'history';
import { Router, Route } from 'react-router';
import { Provider } from 'react-redux';

import Dashboard from './components/Dashboard';
import Layout from './components/Layout';
import store from './store';

import '../styles/main.scss';

const app = document.getElementById('app');
const history = createBrowserHistory();

function getLocation() {
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition((location) => {
      console.log('hejhej', location);
    });
  } else {
    const hej = 'tja';
    console.log(hej);
  }
}

getLocation();

render(
  <Provider store={store}>
    <Router history={history}>
      <Layout>
        <Route path="/" component={Dashboard} />
      </Layout>
    </Router>
  </Provider>,
  app
);
