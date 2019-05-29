import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import ReactDOM from 'react-dom';
import App from './components/App/App.jsx';

ReactDOM.render(
    <BrowserRouter>
        <App />
    </BrowserRouter>,
    document.getElementById('content')
);
/*
if (module.hot) {
    module.hot.accept(App, () => {
        renderApp();
    });
}
*/
