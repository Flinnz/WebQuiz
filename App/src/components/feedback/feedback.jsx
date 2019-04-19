import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class Feedback extends Component {

    render() {
        return (
            <div>
                <Link to='/'>Menu</Link>
                <h2>Here will be feedback</h2>
            </div>
        )
    }
}

export default Feedback;