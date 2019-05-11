import React from 'react';
import ReactDOM from 'react-dom';
import styles from './style.css';
import { Link } from 'react-router-dom';
import Timer from '../timer/timer.jsx';
import * as SignalR from '@aspnet/signalr';




class MultiPlayer extends React.Component {

    constructor(props) {
        super(props);
        const locationState = this.props.location.state || {id: "", playerId: "",};
        this.state = {
            question: "",
            gameId: locationState.id,
            playerId: locationState.playerId,
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
        connection.on('Answer', data => {
            alert(data);
            if(data) {
                this.setState({
                    score: this.state.score + 1,
                });
            }
        });
        connection.serverTimeoutInMilliseconds = 100000;
        connection
            .start()
            .then(() => { 
                console.log('connected');
                if (this.state.playerId == "" && this.state.gameId == "") {
                    fetch(`/api/quiz/game`, {
                        method: 'POST',
                        body: {},})
                        .then(c => c.json())
                        .then(c => this.setState({playerId: c.yourPlayerGuid, gameId: c.guid, }))
                        .then(() => this.getNewQuestion())
                        .catch(c => console.log(c));
                } else {
                    this.getNewQuestion();
                }
            });
    }

    render() {
        return (
            <div id="app">
                <Link to='/'>Menu</Link>
                <div id="score">{this.state.score}</div>
                <div id="score">{this.state.gameId}</div>
                <div id="question">{this.state.question}</div>
                <div id="input">
                    <input className="input" value={this.state.answer} onChange={this.handleInput} onKeyPress={this.handleEnterAnswer}/>
                </div>
                <div className="button" onClick={this.handleAnswer}>Кликай суда</div>
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
        this.connection.invoke('ReceiveAnswer', this.state.gameId, this.state.playerId, this.state.answer)
            .catch(err => alert(err));               
    }

    componentWillUnmount() {

    }
}

export default MultiPlayer;