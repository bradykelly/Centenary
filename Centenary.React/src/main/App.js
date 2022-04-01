import React from "react";
import Header from '../layout/Header.js';
import Footer from '../layout/Footer.js';
import sphinxSmile from "../img/sphinx-smile.png";
import './App.css';
import Main from "./Main";

function App() {
    let parent = "";
    return (
        <div className="App">
            <Header/>
            <img src={sphinxSmile} alt="sphinx with a smile" height={100}/>
            <Main parentFolder={parent}/>
            <Footer year={new Date().getFullYear()}/>
        </div>
    );
}

export default App;
