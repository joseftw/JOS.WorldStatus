import React from 'react';

export default class Widget extends React.Component {
  render() {
    return (
      <div className="widget">
        {this.props.children}
      </div>
    );
  }
}

Widget.propTypes = {
  children: React.PropTypes.element.isRequired
};
