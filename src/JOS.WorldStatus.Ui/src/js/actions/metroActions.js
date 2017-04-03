/* globals __SITECONFIG__ */
import axios from 'axios';

const apiUrl = __SITECONFIG__.apiUrl;

export function fetchMetroInformation() {
  return function(dispatch) {
    dispatch({
      type: 'FETCH_METRO'
    });
    axios.get(`${apiUrl}/api/metro/9288`) // TODO dont hardcode user station.
      .then((response) => {
        dispatch({
          type: 'FETCH_METRO_FULFILLED',
          payload: response.data
        });
      })
      .catch((error) => {
        dispatch({
          type: 'FETCH_METRO_REJECTED',
          payload: error
        });
      });
  };
}

export function josef() {
  return function() {
    console.log('hej');
  };
}
