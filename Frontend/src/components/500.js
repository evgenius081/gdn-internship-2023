import { Link } from 'react-router-dom';
import React from 'react'

export function InternalError(props) {
    return (
        <section className="error">
        <div className="error-header">
            <h1>Error occurred</h1>
        </div>
        <div className="error-img">
            <img src="img/500.jpg" alt="burning Rome" />
        </div>
        <div className="error-text">
            <p>Internal error occurred. If this situation continues, please feel free to contact me via <Link to="mailto:yauheni.hulevich@gmail.com">yauheni.hulevich@gmail.com</Link>. This picture was generated by Midjourney for prompt "Emperor Nero watching as Rome burns". Isn't it exciting?</p>
            <p><br />Proceed to <Link to="/">main</Link></p>
            </div>
    </section>
    )
}