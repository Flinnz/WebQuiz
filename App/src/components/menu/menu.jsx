import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import styles from './styles.css';
import Join from '../join/join.jsx';
import img from './1.png';

export default function Menu() {
    return (
        <div id="menu">
            <Link id="log" className="link" to='/log'>Log In</Link>
            <img id="header" src={img} alt="logo"/>
            <ul id="list">
                <li className="menu-button"><Link className="link" to='/single'>Singleplayer</Link></li>
                <li className="menu-button"><Link className="link" to='/multi'>Multiplayer</Link></li>
                <li className="menu-button"><Link className="link" to='/statistics'>Statistics</Link></li>
                <li className="menu-button"><Link className="link" to='/feedback'>Feedback</Link></li>
            </ul>
        </div>
    ); 
}