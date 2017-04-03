import React from 'react';
import { connect } from 'react-redux';

import { fetchMetroInformation } from '../../actions/metroActions';
import Loader from '../Loader';

import './_metroWidget.scss';

export class MetroWidget extends React.Component {
  componentWillMount() {
    this.props.dispatch(fetchMetroInformation());
  }

  render() {
    console.log(this.props);

    const showLoader = this.props.fetching;
    return (
      <div>
        { showLoader && <Loader message={'Fetching metro information...'} /> }
      </div>
    );
  }
}

const mapStateToProps = state => ({
  stopPointDeviations: state.metro.stopPointDeviations,
  metroInfo: state.metro.metroInfo,
  fetching: state.metro.fetching,
  fetched: state.metro.fetched
});

MetroWidget.propTypes = {
  dispatch: React.PropTypes.func.isRequired,
  metroInfo: React.PropTypes.array.isRequired,
  fetching: React.PropTypes.bool.isRequired,
  fetched: React.PropTypes.bool.isRequired,
  stopPointDeviations: React.PropTypes.array.isRequired
};

export default connect(mapStateToProps)(MetroWidget);
