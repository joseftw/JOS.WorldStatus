import React from 'react';
import MetroWidget from './widgets/metroWidget';

export default class Dashboard extends React.Component {
  render() {
    console.log(this.props);
    return (
      <div>
        <base href="/" />
        <h1>IM LE DASHBOARD</h1>
        <MetroWidget />
      </div>
    );
  }
}
