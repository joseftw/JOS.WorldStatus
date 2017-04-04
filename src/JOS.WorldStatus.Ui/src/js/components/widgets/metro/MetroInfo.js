import React from 'react';

import DepartureRow from './DepartureRow';

export default class MetroInfo extends React.Component {
  render() {
    const departures = this.props.metroInfo;

    if (departures.length === 0) {
      return (
        <div>
          <h2>Inga tåg :(</h2>
        </div>
      );
    }

    const rows = [];
    for (let i = 0; i < departures.length; i += 1) {
      const departure = departures[i];
      rows.push(
        <DepartureRow key={i}
          destination={departure.destination}
          displayTime={departure.displayTime} />
      );
    }
    console.log(rows);
    return (
        <ul>
          {rows}
        </ul>
    );
  }
}

MetroInfo.propTypes = {
  metroInfo: React.PropTypes.array.isRequired
};
