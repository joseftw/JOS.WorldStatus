import React from 'react';

export default class DepartureRow extends React.Component {
  render() {
    return (
      <li>
        <p>{this.props.destination} {this.props.displayTime}</p>
      </li>
    );
  }
}

DepartureRow.propTypes = {
  displayTime: React.PropTypes.string.isRequired,
  destination: React.PropTypes.string.isRequired
};
