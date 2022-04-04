import React from "react";
import { Link, useLocation, Outlet } from "react-router-dom";

export function Home() {
    return (
        <div>
            <h1>[Company website]</h1>
            <nav>
                <Link to="about">About</Link>
                <Link to="events">Events</Link>
                <Link to="contact">Contact</Link>
            </nav>
        </div>
    );
}

export function About() {
    return (
        <div>
            <h1>[About text]</h1>
            <Outlet />
        </div>
    );
}

export function Services() {
    return (
        <div>
            <h2>Our Services</h2>
        </div>
    );
}

export function CompanyHistory() {
    return (
        <div>
            <h2>Our History</h2>
        </div>
    );
}

export function Location() {
    return (
        <div>
            <h2>Our Location - Here</h2>
        </div>
    );
}

export function Events() {
    return (
        <div>
            <h1>[Events list]</h1>
        </div>
    );
}

export function Contact() {
    return (
        <div>
            <h1>[Contact info]</h1>
        </div>
    );
}

export function NotFound() {
    let location = useLocation();
    console.log(location);
    return (
        <div>
            <h1>{location.pathname}: 404 - Not found</h1>
        </div>
    );
}
