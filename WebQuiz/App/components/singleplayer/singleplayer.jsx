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
        }
    }

    componentDidMount() {
        this.getNewQuestion();
    }

    render() {
        return (
            <div id="app">
                <Link to='/'>Menu</Link>
                <Timer start={this.state.timer} isNewQuestion={this.state.isNewQuestion}/>
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
        fetch("/api/quiz/question").then(r => {
            if (r.ok){
                r.json().then(jr => {
                    this.setState({
                        question: jr["text"],
                        id: jr["id"],
                        isNewQuestion: false
                    });
                });
                console.log('после ответа ' + this.state.isNewQuestion);                
            } else {
                this.setState({
                    question: "error"
                });
            }
        });
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
        }).then(r => {
            if(r.ok) {
                r.json().then(jr => {
                    this.setState({
                        answer: "",
                        timer: 30
                    });
                    if(jr) {
                        alert("Правильно пидрила");
                        this.setState({
                            score: this.state.score + 1,
                            isNewQuestion: true
                        });
                        console.log('после алерта ' + this.state.isNewQuestion);
                        this.getNewQuestion();
                    } else {
                        alert("Неправильно");
                    }
                })               
            }
        });
    }
};

export default SinglePlayer;