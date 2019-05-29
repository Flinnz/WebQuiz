import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import style from './styles.css'

export default function Join(props) {
    const [id, setId] = React.useState("");
    const {onJoin, onCreate, onRandom} = props;

    return (
            <div id="joinwindow">
                <div id="joinwindowform">
                    <div className="backLink">
                        <Link className="back" to='/'></Link>
                    </div><br/>
                    <label>Введите ID созданной игры, чтобы присоединиться к ней:
                        <input value={id} onChange={(evt) => setId(evt.target.value)}/>
                    </label>
                    <div id="joinwindowbuttons">
                        <div className="link" onClick={(evt) => onJoin(id)}>Join to game</div>
                        <div className="link" onClick={(evt) => onRandom()}>Find Game</div>
                        <div className="link" onClick={() => onCreate()}>Create Game</div>
                    </div>
                </div>
            </div>
        )
}
