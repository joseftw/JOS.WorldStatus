/* globals __SITECONFIG__ */
import { applyMiddleware, createStore } from 'redux';

import { createLogger } from 'redux-logger';
import thunk from 'redux-thunk';
import promise from 'redux-promise-middleware';

import reducer from './reducers';

const middlewares = [promise(), thunk];

if (!__SITECONFIG__.production) {
  middlewares.push(createLogger());
}

const middleware = applyMiddleware(...middlewares);

export default createStore(reducer, middleware);
