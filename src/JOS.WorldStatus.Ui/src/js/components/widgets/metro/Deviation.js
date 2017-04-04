import React from 'react';

export default class Deviation extends React.Component {
  render() {
    const deviation = this.props.deviation;

    return (
      <div>
        <h1>{deviation.description}</h1>
      </div>
    );
  }
}

Deviation.propTypes = {
  deviation: React.PropTypes.object.isRequired
};
