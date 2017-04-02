import axios from 'axios';

export function fetchMetroInformation() {
  return function(dispatch) {
    axios.get('http://localhost:5521/api/metro/9288') // TODO dont hardcode user station.
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
