import React from 'react';
import ReactDOM from 'react-dom';
import 'whatwg-fetch';
import App from './components/App/App.jsx';
import { AppContainer } from 'react-hot-loader';

ReactDOM.render(
    <AppContainer>
        <App />
    </AppContainer>,
    document.getElementById('content')
);

if (module.hot) {
    module.hot.accept(App, () => {
        renderApp();
    });
}