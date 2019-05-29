import React, { Component } from 'react';
import styles from './feedback.css';
import { Link } from 'react-router-dom';

class Feedback extends Component {
    constructor(props) {
        super(props);
        this.state = {
            text: ""
        };
    }

    handleInput = (evt) => {
        this.setState({
            text: evt.target.value
        })
    };

    handleAnswer = (evt) => {
        fetch("/api/quiz/feedback", {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                {
                    feedback: this.state.text
                })
        })
            .then(r => r.json())
            .then(json => {
                if(json) {
                    alert("Отзыв успешно отправлен");
                    this.setState({
                        text: ""
                    });
                } else {
                    return Promise.reject("Произошла ошибка, повторите отправку");
                }
            })
            .catch(err => alert(err));
    };

    render() {
        return (
            <div className="wrapper">
                <Link className="linkback" to='/'>Menu</Link>
                <div className="inputarea">
                    <div className="text">
                        <p>Здесь вы можете оставить свои пожелания или отзыв к нашему проекту. Также можете предложить свои вопросы по <a href="https://forms.gle/dGxzLWBwLSPz1ZceA">ссылке</a>.</p>
                    </div>
                    <div id="textinput">
                        <textarea
                            value={this.state.answer}
                            onChange={this.handleInput}
                            className="textinput"/>
                    </div>
                    <div className="send" onClick={this.handleAnswer}>Send</div>
                </div>
            </div>
        )
    }
}

export default Feedback;