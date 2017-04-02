import React from 'react';
import { render } from 'react-dom';
import { createBrowserHistory } from 'history';
import { Router, Route } from 'react-router';

import Layout from './components/Layout';
import Dashboard from './components/Dashboard';

const app = document.getElementById('app');
const history = createBrowserHistory();

render(
    <Router history={history}>
        <Layout>
          <Route path="/" component={Dashboard} />
        </Layout>
    </Router>,
    app
);
