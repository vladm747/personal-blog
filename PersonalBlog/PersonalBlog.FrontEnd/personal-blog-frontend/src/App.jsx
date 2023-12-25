import BlogListComponent from "./components/blogs/BlogListComponent.jsx";
import Register from "./components/auth/Register.jsx";
import PostListComponent from "./components/posts/PostListComponent.jsx";
import React, {Component, createContext, useContext, useState} from "react";
import {Routes, Route} from 'react-router-dom';
import Login from "./components/auth/Login.jsx";
import Navbar from "./components/navbar/Navbar.jsx";
export const NavbarContext = createContext({});
const NavbarProvider = ({ children }) => {
    const [user, setUser] = React.useState({});

    return (
        <NavbarContext.Provider value={{ user, setUser }}>
            {children}
        </NavbarContext.Provider>
    );
};
const App = () => {
    const [ user, setUser ] = React.useState({});
    return (
        <>
            <NavbarProvider>
                <Navbar/>
               <Routes>
                   <Route path="/" element={<BlogListComponent/>}/>
                   <Route path="/login" element={<Login/>}/>
                   <Route path="/register" element={<Register/>}/>
                   <Route path="/posts-list" element={<PostListComponent/>}/>
               </Routes>
            </NavbarProvider>
        </>
    );
}

export default App;
