import React from 'react';
import { connect } from 'react-redux';

import { fetchMetroInformation } from '../../../actions/metroActions';
import Loader from '../../Loader';
import StopPointInfo from './StopPointInfo';
import MetroInfo from './MetroInfo';

import './_metroWidget.scss';

export class MetroWidget extends React.Component {
  componentWillMount() {
    this.props.dispatch(fetchMetroInformation());
    this.startPoll();
  }

  componentWillUnmount() {
    clearTimeout(this.timeout);
  }

  startPoll() {
    this.timeout = setTimeout(() => this.props.dispatch(fetchMetroInformation()), 30000);
  }

  render() {
    const showLoader = this.props.fetching;
    if (showLoader) {
      return (
        <div>
          <Loader message={'Fetching metro information...'} />
        </div>
      );
    }

    return (
      <div>
        <StopPointInfo stopPointInformation={this.props.stopPointInformation} />
        <MetroInfo metroInfo={this.props.metroInfo} />
      </div>
    );
  }
}

const mapStateToProps = state => ({
  stopPointInformation: state.metro.stopPointInformation,
  metroInfo: state.metro.metroInfo,
  fetching: state.metro.fetching,
  fetched: state.metro.fetched
});

MetroWidget.propTypes = {
  dispatch: React.PropTypes.func.isRequired,
  metroInfo: React.PropTypes.array.isRequired,
  fetching: React.PropTypes.bool.isRequired,
  fetched: React.PropTypes.bool.isRequired,
  stopPointInformation: React.PropTypes.object.isRequired
};

export default connect(mapStateToProps)(MetroWidget);
