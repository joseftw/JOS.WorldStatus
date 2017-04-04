export default function reducer(state = {
  stopPointInformation: {
    deviations: []
  },
  metroInfo: [],
  fetching: false,
  fetched: false,
  errors: []
}, action) {
  switch (action.type) {
    case 'FETCH_METRO':
      return {
        ...state,
        fetching: true
      };
    case 'FETCH_METRO_REJECTED':
      return {
        ...state,
        fetched: true,
        fetching: false,
        errors: action.payload.errors
      };
    case 'FETCH_METRO_FULFILLED':
      return {
        ...state,
        fetched: true,
        fetching: false,
        stopPointInformation: action.payload.stopPointInformation,
        metroInfo: action.payload.metroInfo
      };
  }

  return state;
}
