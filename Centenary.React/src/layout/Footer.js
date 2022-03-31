import React from "react";

function Footer(props) {
    return (
        <footer className="footer">
            &copy; Copyright {props.year} Erisia Web Development
        </footer>
    );
}

export default Footer;