import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {FronteggProvider} from "@frontegg/react";

const contextOptions = {
    baseUrl: 'https://app-d9wi5bj3yi2x.frontegg.com',
    clientId: 'f98929ef-feea-42b5-b0a2-fe96fdbda6b9'
}

ReactDOM.render(
  <FronteggProvider contextOptions={contextOptions} hostedLoginBox={true}>
    <App />
  </FronteggProvider>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
