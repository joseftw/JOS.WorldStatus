import React from 'react';

export default class DepartureRow extends React.Component {
  render() {
    const now = new Date(this.props.timeTabled);
    return (
      <li>
        <p>{this.props.destination} {this.props.displayTime} {`${now.getHours()}:${now.getMinutes()}`}</p>
      </li>
    );
  }
}

DepartureRow.propTypes = {
  displayTime: React.PropTypes.string.isRequired,
  destination: React.PropTypes.string.isRequired,
  timeTabled: React.PropTypes.string.isRequired
};
