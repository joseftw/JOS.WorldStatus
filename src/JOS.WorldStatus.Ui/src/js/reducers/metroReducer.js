export default function reducer(state = {
  stopPointDeviations: [],
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
        errors: action.payload.errors
      };
    case 'FETCH_METRO_FULFILLED':
      return {
        ...state,
        fetched: true,
        stopPointDeviations: action.payload.stopPointInformation.deviations,
        metroInfo: action.payload.metroInfo
      };
  }

  return state;
}
