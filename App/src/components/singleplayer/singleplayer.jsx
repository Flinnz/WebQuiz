import React from 'react';
import ReactDOM from 'react-dom';
import styles from './singleplayer.css';
import { Link } from 'react-router-dom';
import Timer from '../timer/timer.jsx';

class SinglePlayer extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            question: "",
            id: "",
            answer: "",
            score: 0,
            timer: 30,
            isNewQuestion: false
        };
    }

    componentDidMount() {
        this.getNewQuestion();
    }

    render() {
        return (
            <div id="app">
                <Link className="link" to='/'>Menu</Link>
                <Timer className="time" start={this.state.timer} isNewQuestion={this.state.isNewQuestion} onTimeout={this.getNewQuestion}/>
                <div id="score">{this.state.score}</div>
                <div id="question">{this.state.question}</div>
                <div id="input">
                    <input className="input" value={this.state.answer} onChange={this.handleInput} onKeyPress={this.handleEnterAnswer}/>
                </div>
                <div className="button" onClick={this.handleAnswer}>Отправить</div>
            </div>
        );
    }

    getNewQuestion = () => {
        fetch("/api/quiz/question")
            .then(r => r.json())
            .then(json => 
                this.setState({
                    question: json["text"],
                    id: json["id"],
                    isNewQuestion: false
                })
            )
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
        fetch("/api/quiz/answer", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                {
                    id: this.state.id,
                    answer: this.state.answer
                })
        })
        .then(r => r.json())
        .then(json => {
            this.setState({
                answer: "",
                timer: 2
            });
            if(json) {
                alert("Ответ верный");
                console.log('после алерта ' + this.state.isNewQuestion);
                this.getNewQuestion();
                this.setState({
                    score: this.state.score + 1,
                    isNewQuestion: true
                });
            } else {
                return Promise.reject("Ответ неверный");
            }
        })
        .catch(err => alert(err));
    }
};

export default SinglePlayer;