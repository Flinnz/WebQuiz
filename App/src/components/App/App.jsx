import React from 'react';
import ReactDOM from 'react-dom';
import styles from './styles.css';
import { Route } from 'react-router-dom';
import Menu from '../menu/menu.jsx';
import SinglePlayer from '../singleplayer/singleplayer.jsx';
import MultiPlayer from '../multiplayer/multiplayer.jsx';
import Statistics from '../statistics/statistics.jsx';
import Feedback from '../feedback/feedback.jsx';
import Join from '../join/join.jsx';

class App extends React.Component {
    render() {
        return (
            <div>
                <Route path='/' exact component={Menu} />
                <Route path='/single' exact component={SinglePlayer} />
                <Route path='/multi' component={MultiPlayer} />
                <Route path='/statistics' exact component={Statistics} />
                <Route path='/feedback' exact component={Feedback} />
                <Route path='/join' exact component={Join}/>
            </div>
        );
    }
}

export default App;