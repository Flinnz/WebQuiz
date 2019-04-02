import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class MultiPlayer extends Component {

    render() {
        return (
            <div>
                <Link to='/'>Menu</Link>
                <h2>Here will be multiplayer</h2>
            </div>
        )
    }
}

export default MultiPlayer;