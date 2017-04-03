import React from 'react';
import Widget from './widgets/Widget';
import MetroWidget from './widgets/MetroWidget';

export default class Dashboard extends React.Component {
  render() {
    return (
      <div className="dashboard container">
        <h1>IM LE DASHBOARD</h1>
        <Widget>
          <MetroWidget />
        </Widget>
      </div>
    );
  }
}
