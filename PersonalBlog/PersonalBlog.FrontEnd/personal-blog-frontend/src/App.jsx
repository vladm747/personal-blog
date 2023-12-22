import BlogListComponent from "./components/blogs/BlogListComponent.jsx";
import AuthComponent from "./components/auth/AuthComponent.jsx";
import PostListComponent from "./components/posts/PostListComponent.jsx";
import React, {Component} from "react";
import {BrowserRouter, Routes, Route, Link} from 'react-router-dom';

const linkStyle = {
    margin: "1rem",
    textDecoration: "none",
    color: 'white'
};
class App extends Component {
    constructor(props) {
        super(props);

        this.state = { }

    }
    render (){
        console.log("app.js");
       return (
           <>
               <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
                   <div className="navbar-nav">
                       <div className="nav-item nav-link">
                           <Link style={linkStyle} to="/">List of Blogs</Link>
                       </div>
                       <div className="nav-item nav-link">
                           <Link style={linkStyle} to="auth/login/">Login</Link>
                       </div>
                       <div className="nav-item nav-link">
                           <Link style={linkStyle} to="auth/register/">Register</Link>
                       </div>
                   </div>
               </nav>

               <Routes>
                   <Route path="/" element={<BlogListComponent/>}/>
                   <Route path="/auth" element={<AuthComponent/>}/>
                   <Route path="/posts-list" element={<PostListComponent/>}/>
               </Routes>
           </>
       );
    }
}

export default App;
