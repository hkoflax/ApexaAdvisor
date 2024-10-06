// src/App.js
import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import AdvisorList from './components/AdvisorList';
import CreateAdvisor from './components/CreateAdvisor';
import EditAdvisor from './components/EditAdvisor';

function App() {
    return (
        <Router>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <div className="container-fluid">
                    <Link className="navbar-brand" to="/advisors">Advisor Management</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav">
                            <li className="nav-item">
                                <Link className="nav-link" to="/advisors">Advisors</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="/advisors/create">Create Advisor</Link>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

            <div className="container mt-5">
                <Routes>
                    <Route path="/advisors" element={<AdvisorList />} />
                    <Route path="/advisors/create" element={<CreateAdvisor />} />
                    <Route path="/advisors/edit/:id" element={<EditAdvisor />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
