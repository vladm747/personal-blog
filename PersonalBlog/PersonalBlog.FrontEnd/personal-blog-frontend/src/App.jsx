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
    render () {
        console.log("app.js");
       return (
           <>
               <nav className="bg-gray-800">
                   <div className="mx-auto max-w-7xl px-2 sm:px-6 lg:px-8">
                       <div className="relative flex h-16 items-center justify-between">
                           <div className="flex flex-1 items-center justify-center sm:items-stretch sm:justify-start">
                               <div className="hidden sm:ml-6 sm:block">
                                   <div className="flex space-x-4">
                                       <Link className="text-gray-300 hover:bg-gray-700 hover:text-white rounded-md px-3 py-2 text-sm font-medium" to="/">List of Blogs</Link>
                                       <Link className="text-gray-300 hover:bg-gray-700 hover:text-white rounded-md px-3 py-2 text-sm font-medium" to="auth/login/">Login</Link>
                                       <Link className="text-gray-300 hover:bg-gray-700 hover:text-white rounded-md px-3 py-2 text-sm font-medium" to="auth/register/">Register</Link>
                                   </div>
                               </div>
                           </div>
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
