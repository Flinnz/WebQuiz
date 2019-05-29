import React, { Component } from 'react';

class Timer extends Component {
    constructor(props){
        super(props);
        this.state = {
            currentCount: this.props.start,
            isNewQuestion: this.props.isNewQuestion
        }
        this.onTimeout = props.onTimeout;
      }

    timer() {
        this.setState({
          currentCount: this.state.currentCount - 1
        })
        console.log(this.props.isNewQuestion);
        if (this.props.isNewQuestion) {
            this.setState({
                currentCount: this.props.start,
                isNewQuestion: false
            });
        }
        if(this.state.currentCount < 1) { 
            this.setState({
                currentCount: this.props.start,
            });
            this.onTimeout();
        }
    }

    componentDidMount() {
        this.intervalId = setInterval(() => {this.timer()}, 1000);
    }

    componentWillUnmount(){
        clearInterval(this.intervalId);
    }

    render() {
        return(
            <div>{this.state.currentCount}</div>
        )
    }
}

export default Timer;