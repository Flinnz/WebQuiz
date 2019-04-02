import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class Menu extends Component {
    render() {
        return (
            <div>
                <ul>
                    <li><Link to='/single'>Single Player</Link></li>
                    <li><Link to='/multi'>Multi Player</Link></li>
                    <li><Link to='/statistics'>Statistics</Link></li>
                    <li><Link to='/feedback'>Feedback</Link></li>
                </ul>
            </div>
        )
    }
}

export default Menu;