import React from 'react';
import ReactDOM from 'react-dom';
import styles from './styles.css';

export default class App extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            question: "",
            id: "",
            answer: "",
            score: 0
        }
    }

    componentDidMount() {
        this.getNewQuestion();
    }

    render() {
        return (
            <div id="app">
                <div id="score">{this.state.score}</div>
                <div id="question">{this.state.question}</div>
                <div id="input">
                    <input className="input" value={this.state.answer} onChange={this.handleInput}/>
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
                        id: jr["id"]
                    });
                });                
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
                    if(jr) {
                        alert("Правильно пидрила");
                        this.setState({
                            score: this.state.score + 1
                        });
                        this.getNewQuestion();
                    } else {
                        alert("Неправильно");
                    }
                })               
            }
        });
    }
};