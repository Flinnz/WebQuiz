import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import styles from './styles.css';
import Log from '../log/log.jsx';
import img from './1.png';

export default function Menu() {
    const [openModal, setOpenModal] = React.useState(false);
    return (
        <div id="menu">
            {openModal && <Log closeModal={() => setOpenModal(false)} onSave={() => {}/*отправка формы логин*/}/>}
            <div className="logArea">
                <FontAwesomeIcon icon="user" size="3x"/>
                <div id="log" className="link" onClick={() => setOpenModal(true)}>Log In</div>
            </div>
            <img id="header" src={img} alt="logo" />
            <ul id="list">
                <li className="menu-button"><Link className="link" to='/single'>Singleplayer</Link></li>
                <li className="menu-button"><Link className="link" to='/multi'>Multiplayer</Link></li>
                <li className="menu-button"><Link className="link" to='/statistics'>Statistics</Link></li>
                <li className="menu-button"><Link className="link" to='/feedback'>Feedback</Link></li>
            </ul>
        </div>
    ); 
}