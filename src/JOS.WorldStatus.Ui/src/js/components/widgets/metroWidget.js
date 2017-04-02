import React from 'react';
import { connect } from 'react-redux';

import { fetchMetroInformation } from '../../actions/metroActions';

export class MetroWidget extends React.Component {
  componentWillMount() {
    this.props.dispatch(fetchMetroInformation());
  }

  render() {
    return (
      <div>
        <h2>IM A WIDGET</h2>
      </div>
    );
  }
}

const mapStateToProps = state => ({
  stopPointDeviations: state.metro.stopPointDeviations
});
export default connect(mapStateToProps)(MetroWidget);
