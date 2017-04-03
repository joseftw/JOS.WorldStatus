import React from 'react';

import './_widget.scss';


export default class Widget extends React.Component {
  render() {
    console.log(this.props);
    return (
      <div className={`widget ${this.props.cssClass}`}>
        {this.props.children}
      </div>
    );
  }
}

Widget.propTypes = {
  cssClass: React.PropTypes.string,
  heading: React.PropTypes.string,
  children: React.PropTypes.element.isRequired
};
