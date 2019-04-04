import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class Feedback extends Component {
    constructor(props) {
        super(props);
        this.state = {
            message: "",
            err: "",
            name: ""
        }
    }
    
    changeName = (event) => {
        this.setState({
            name: event.target.value
        })
    }

    changeMessage = (event) => {
        this.setState({
            message: event.target.value
        })
    }

    handleClick = () => {
        fetch("/api/feedback", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                {
                    message: this.state.message,
                    name: this.state.name
                })
        })
        .then(r => r.json())
        .catch(err => alert(err));  
    }

    render() {
        return (
            <div>
                <Link to='/'>Menu</Link>
                <h2>Here will be feedback</h2>
                <p>Оставьте свой отзыв о нашем проекте или предложите что-то интересное:</p>
                <label>Ваше имя:
                    <input type="text" onChange={this.changeName} />
                </label>
                <br />
                <textarea rows="10" cols="90" onChange={this.changeMessage}></textarea>
                <br />
                <input type="button" value="Отправить" onClick={this.handleClick} />
            </div>
        )
    }
}

export default Feedback;