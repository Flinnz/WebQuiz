import React from 'react';
import ReactDOM from 'react-dom';
import styles from './style.css';
import { Link } from 'react-router-dom';
import Timer from '../timer/timer.jsx';
import * as SignalR from '@aspnet/signalr';




class MultiPlayer extends React.Component {

    constructor(props) {
        super(props);
        
        this.state = {
            question: "",
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
        connection.serverTimeoutInMilliseconds = 100000;
        connection
            .start()
            .then(() => { 
                console.log('connected');
                this.getNewQuestion();
                connection.invoke('GetConnectedUsers');
            });
        
        connection.on('GetQuestion', data => {
            this.setState({
                id: data.id,
                question: data.text,
            });
        });
        connection.on('GetConnectedUsers', data => console.log(data));
        connection.on('Answer', data => {
            alert(data);
            if(data) {
                this.setState({
                    score: this.state.score + 1,
                });
            }
        });
        
    }

    render() {
        return (
            <div id="app">
                <Link to='/'>Menu</Link>
                <div id="score">{this.state.score}</div>
                <div id="question">{this.state.question}</div>
                <div id="input">
                    <input className="input" value={this.state.answer} onChange={this.handleInput} onKeyPress={this.handleEnterAnswer}/>
                </div>
                <div className="button" onClick={this.handleAnswer}>Кликай суда</div>
            </div>
        );
    }

    getNewQuestion = () => {
        this.connection.invoke('SendQuestion')
            .catch(err => this.setState({question: err}));
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
        this.connection.invoke('ReceiveAnswer', this.state.id, this.state.answer)
            .catch(err => alert(err));               
    }
}

export default MultiPlayer;