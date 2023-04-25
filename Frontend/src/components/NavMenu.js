import React, {useState} from 'react';
import { Link } from 'react-router-dom'
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

export function NavMenu(){
    const [expanded, setExpanded] = useState(false);

    return (
        <Navbar expand="lg" expanded={expanded}>
            <Container>
                <Navbar.Toggle onClick={() => setExpanded(expanded ? false : "expanded")} aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <Link to="/" className="nav-item nav-link active" onClick={() => setExpanded(false)}>Average</Link>
                        <Link to="/minmax" className="nav-item nav-link active" onClick={() => setExpanded(false)}>MinMax</Link>
                        <Link to="/major-difference" className="nav-item nav-link active" onClick={() => setExpanded(false)}>Major difference</Link>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}
