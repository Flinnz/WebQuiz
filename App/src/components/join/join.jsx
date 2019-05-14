import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import style from './styles.css'

export default function Join(props) {
    const [id, setId] = React.useState("");
    const [playerId, setPlayerId] = React.useState("");
    const [redirect, setRedirect] = React.useState(false);
    const {closeModal} = props;
    return redirect 
        ? (
            <Redirect to={{
                pathname: '/multi',
                state: {id: id, playerId: playerId, },
        }}/>)
        : (
            <div id="joinwindow">
                <div id="joinwindowform">
                    <div id="back" onClick={() => closeModal()}></div><br/>
                    <label>Введите ID созданной игры, чтобы присоединиться к ней:
                        <input value={id} onChange={(evt) => setId(evt.target.value)}/>
                    </label>
                    <div id="joinwindowbuttons">
                        <div className="link" onClick={
                            () => fetch(`/api/quiz/game?id=${id}`, {method: 'GET',})
                                .then(c => c.json())
                                .then(c => c.yourPlayerGuid ? Promise.resolve(c.yourPlayerGuid) : Promise.reject())
                                .then(c => setPlayerId(c))
                                .then(() => setRedirect(true))
                                .catch(c => console.log(c))}
                                >Join to game</div>
                        <Link className="link" to='/multi'>Create Game</Link>
                    </div>
                </div>
            </div>
        )
}
