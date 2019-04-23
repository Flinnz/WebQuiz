import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import styles from './styles.css';
import img from './UxIpJ6h5iu8.jpg'

class Menu extends Component {
    constructor(props) {
        super(props);
        this.state = {
            isOpen: false
        }
    }

    multiClick = (evt) => {
        this.setState({
            isOpen: !this.state.isOpen
        })

    };

    render() {
        let hidden1;
        let hidden2;
        if (this.state.isOpen) {
            hidden1 = <li className="menu-button"><Link className="link" to='/multi'>Create game</Link></li>;
            hidden2 = <li className="menu-button"><Link className="link" to='/join'>Join to game</Link></li>;
        } else {
            hidden1 = hidden2 = null;
        }

        return (
            <div id="menu">
                <Link id="log" className="link" to='/log'>Log In</Link>
                <img id="header" src={img} alt="logo"/>
                <ul id="list">
                    <li className="menu-button"><Link className="link" to='/single'>Single Player</Link></li>
                    <li className="menu-button"><div className="link" onClick={this.multiClick}>Multi Player</div></li>
                    {hidden1}
                    {hidden2}
                    <li className="menu-button"><Link className="link" to='/statistics'>Statistics</Link></li>
                    <li className="menu-button"><Link className="link" to='/feedback'>Feedback</Link></li>
                </ul>
            </div>
        )
    }
}

export default Menu;