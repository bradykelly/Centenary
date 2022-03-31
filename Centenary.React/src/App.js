import React from "react";
import Header from './layout/Header.js';
import Main from './layout/Main.js';
import Footer from './layout/Footer.js';
import './App.css';

function App() {
    let parent = "";
    return (
        <div className="App">
            <Header/>
            <Main parentFolder={parent}/>
            <Footer year={new Date().getFullYear()}/>
        </div>
    );
}

export default App;
