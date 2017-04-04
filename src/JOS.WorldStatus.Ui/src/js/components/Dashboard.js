import React from 'react';
import Widget from './widgets/Widget';
import MetroWidget from './widgets/metro/MetroWidget';

import './_dashboard.scss';

export default class Dashboard extends React.Component {
  render() {
    return (
      <div className="dashboard-container">
        <Widget cssClass={'metro'}>
          <MetroWidget/>
        </Widget>
        <Widget>
        </Widget>
      </div>
    );
  }
}
