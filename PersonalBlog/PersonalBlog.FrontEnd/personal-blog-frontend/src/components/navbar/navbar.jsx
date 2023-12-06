import {BrowserRouter, Routes, Route, Link} from 'react-router-dom';
import BlogList from '../blogs/BlogList';
import Login from '../auth/Login';
import Register from '../auth/Register';
import './App.css';

function Navbar(){

    return(
        <div className={Navbar}>
            <BrowserRouter>
                <nav>
                    <ul>
                        <li>
                            <Link to="/">List of Blogs</Link>
                        </li>
                        <li>
                            <Link to="auth/login">Login</Link>
                        </li>
                        <li >
                            <Link to="auth/register">Register</Link>
                        </li>
                    </ul>
                </nav>
                <Routes>
                    <Route index element={<ListUser />} />
                    <Route path="user/create" element={<CreateUser />} />
                    <Route path="user/:id/edit" element={<EditUser />} />
                </Routes>
            </BrowserRouter>
        </div>
    )
}