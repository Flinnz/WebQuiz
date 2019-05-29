import React from 'react';
import ReactDOM from 'react-dom';
import styles from './style.css';
import { Link } from 'react-router-dom';
import Join from '../join/join.jsx';
import Timer from '../timer/timer.jsx';
import * as SignalR from '@aspnet/signalr';




class MultiPlayer extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            question: "",
            gameId: "",
            id: "",
            answer: "",
            score: 0,
            timer: 30,
            isNewQuestion: false
        }
    }
    
    componentDidMount() {
        const connection = new SignalR.HubConnectionBuilder()
            .withUrl('/multiplayer')
            .configureLogging(SignalR.LogLevel.Information)
            .build();
        this.connection = connection;
        connection.on('GetQuestion', data => {
            this.setState({
                id: data.id,
                question: data.text,
            });
        });
        connection.on('CreateGame', data => {
            this.setState({
                gameId: data,
            });
        });
        connection.on('JoinGame', data => {
            if(data != null) {
                this.setState({
                    gameId: data,
                });
            }
        });
        connection.on('FindGame', data => {
            console.log(data);
            if (data != null) {
                this.setState({
                    gameId: data,
                });
            }
        })
        connection.on('Answer', data => {
            alert(data);
            if(data) {
                this.setState({
                    score: this.state.score + 1,
                });
            }
        });
        connection.on('Tick', data => console.log(1));
        connection.serverTimeoutInMilliseconds = 10000;
        connection.start().then(() => setInterval(() => connection.invoke('Tick').catch(err => console.log(err)), 1000));
    }

    render() {
        return this.state.gameId == "" ? (<Join onJoin={this.handleJoin} onCreate={this.handleCreate} onRandom={this.handleRandom}/>) : (
            <div id="app">
                <Link className="link"to='/'>Menu</Link>
                <div id="score">{this.state.score}</div>
                <div id="gameid">{this.state.gameId}</div>
                <div id="question">{this.state.question}</div>
                <div id="input">
                    <input className="input" value={this.state.answer} onChange={this.handleInput} onKeyPress={this.handleEnterAnswer}/>
                </div>
                <div className="button" onClick={this.handleAnswer}>Send</div>
            </div>
        );
    }

    getNewQuestion = () => {
        if (this.state.gameId != "") {
            this.connection
                .invoke('SendQuestion', this.state.gameId)
                .catch(err => this.setState({question: err}));
        }
    }

    handleJoin = (id) => {
        this.connection
            .invoke('JoinGame', id)
            .catch(err => console.log(err));
    }

    handleCreate = () => {
        this.connection
            .invoke('CreateGame')
            .catch(err => console.log(err));
    }

    handleRandom = () => {
        this.connection
            .invoke('FindGame')
            .catch(err => console.log(err));
    }

    handleInput = (evt) => {
        this.setState({
            answer: evt.target.value
        })
    }

    handleEnterAnswer = (event) => {
        if (event.key === "Enter") {
            this.handleAnswer();
        }
    }
    

    handleAnswer = (evt) => {     
        this.connection.invoke('ReceiveAnswer', this.state.gameId, this.state.answer)
            .catch(err => alert(err));               
    }

    componentWillUnmount() {
        this.connection
            .invoke('LeaveGame', this.state.gameId)
            .then(() => this.connection.stop())
            .catch(err => console.log(err));
    }
}

export default MultiPlayer;