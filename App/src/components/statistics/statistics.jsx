import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class Statistics extends Component {

    render() {
        return (
            <div>
                <Link to='/'>Menu</Link>
                <h2>Here will be statistics</h2>
            </div>
        )
    }
}

export default Statistics;