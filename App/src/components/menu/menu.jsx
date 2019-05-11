import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import styles from './styles.css';
import Join from '../join/join.jsx';
import img from './UxIpJ6h5iu8.jpg';

export default function Menu() {
    const [openModal, setOpenModal] = React.useState(false);
    return (
        <div id="menu">
            {openModal && <Join closeModal={() => setOpenModal(false)}/>}
            <Link id="log" className="link" to='/log'>Log In</Link>
            <img id="header" src={img} alt="logo"/>
            <ul id="list">
                <li className="menu-button"><Link className="link" to='/single'>Singleplayer</Link></li>
                <li className="menu-button"><div className="link" onClick={() => setOpenModal(true)}>Multiplayer</div></li>
                <li className="menu-button"><Link className="link" to='/statistics'>Statistics</Link></li>
                <li className="menu-button"><Link className="link" to='/feedback'>Feedback</Link></li>
            </ul>
        </div>
    ); 
}