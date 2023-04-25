import { Link } from 'react-router-dom';
import React from 'react'

export function NotFound() {
    return (
        <section className="error">
        <div className="error-header">
            <h1>Not found</h1>
        </div>
        <div className="error-img">
            <img src="img/404.jpg" alt="astronaut" />
        </div>
        <div className="error-text">
            <p>The page you are searching was not found. Try the other one.</p>
            <p>Proceed to <Link to="/">main</Link></p>
            </div>
    </section>
    )
}