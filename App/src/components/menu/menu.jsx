import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import styles from './styles.css';
import img from './UxIpJ6h5iu8.jpg'

class Menu extends Component {
    render() {
        return (
            <div id="menu">
                <img id="header" src={img}/>
                <ul id="list">
                    <li className="menu-button"><Link className="link" to='/single'>Single Player</Link></li>
                    <li className="menu-button"><Link className="link" to='/multi'>Multi Player</Link></li>
                    <li className="menu-button"><Link className="link" to='/statistics'>Statistics</Link></li>
                    <li className="menu-button"><Link className="link" to='/feedback'>Feedback</Link></li>
                </ul>
            </div>
        )
    }
}

export default Menu;