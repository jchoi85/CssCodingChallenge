import 'bootstrap/dist/css/bootstrap.css';
import { Router } from 'react-router';
import { createBrowserHistory } from 'history';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import App from './App';


// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;
const history = createBrowserHistory({ basename: baseUrl });

ReactDOM.render(
    <Router history={history}>
        <App />
    </Router>,
    document.getElementById('root'));
