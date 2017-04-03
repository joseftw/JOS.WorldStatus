import React from 'react';

import './_loader.scss';

export default class Loader extends React.Component {
  render() {
    const message = this.props.message ? this.props.message : 'LOADING';
    return (
      <div className="loader-container">
        <div className="loader">
          <h3>{message}</h3>
          <div class="spinner"></div>
        </div>
      </div>
    );
  }
}

Loader.propTypes = {
  message: React.PropTypes.string
};
