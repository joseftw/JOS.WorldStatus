import React from 'react';

export default class StopPointInfo extends React.Component {
  render() {
    const deviations = this.props.stopPointInformation.deviations;

    if (deviations.length === 0) {
      return (
        <div>
          <h2>Inga förseningar</h2>
        </div>
      );
    }

    return (
      <div>
        <h2>Förseningar</h2>
      </div>
    );
  }
}

StopPointInfo.propTypes = {
  stopPointInformation: React.PropTypes.object.isRequired
};
