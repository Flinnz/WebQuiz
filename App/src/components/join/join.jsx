import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class Join extends Component {
    constructor(props) {
        super(props);
        this.state ={
            id: ""
        }
    }

    handleInput = (evt) => {
        this.setState({
            id: evt.target.value
        })
    };

    sendId = (evt) => {
        console.log(this.state.id);
    };

    render() {
        return (
            <div>
                <Link to='/'>Menu</Link><br/>
                <label>Введите ID созданной игры, чтобы присоединиться к ней:
                    <input value={this.state.id} onChange={this.handleInput}/>
                </label>
                <div className="send" onClick={this.sendId}>Join to game</div>
            </div>
        )
    }
}

export default Join;