import {Link} from "react-router-dom";
import React, {createContext, Fragment, useContext, useEffect, useRef} from "react";
import TokenValidation from "../../helpers/TokenValidation.js";
import UserService from "../../services/UserService.js";
import {AppBar, Box, Button, Container, Toolbar} from "@mui/material";
import AuthService from "../../services/AuthService.js";
import {useCookies} from "react-cookie";
import {NavbarContext} from "../../App.jsx";

const Navbar = () => {
    const { user, setUser } = useContext(NavbarContext);

    const userService = new UserService();
    const authService = new AuthService();
    const [anchorElNav, setAnchorElNav] = React.useState(null);
    const [anchorElUser, setAnchorElUser] = React.useState(null);
    const [accessToken, setAccessToken] = useCookies('accessToken');

    useEffect(() => {
        setUser(() => {
            if (accessToken.accessToken)
                return userService.me();
            else
                return { id: '', nickName: '', email: '' };
        })
    }, []);


    const handleOpenNavMenu = (event) => {
        setAnchorElNav(event.currentTarget);
    };

    const handleOpenUserMenu = (event) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const onSignOut = async () => {
        await authService.signOut();
    }

    TokenValidation();

    return (
        <>
            <AppBar position="static">
                <Container maxWidth="xl">
                    <Toolbar disableGutters>
                        <Link key="listOfBlogs" sx={{color: 'white', textDecoration: 'none'}}
                              to="/">
                            <Button
                                onClick={handleCloseNavMenu}
                                sx={{my: 2, color: 'white', display: 'block'}}
                            > List of Blogs </Button>
                        </Link>
                        {
                            user ? (
                            <Button
                                key="signOut"
                                onClick={onSignOut}
                                sx={{my: 2, color: 'white', display: 'block'}}
                            > Sign Out </Button>
                        ) : (
                            <>
                                <Link key="register" sx={{color: 'white', textDecoration: 'none'}}
                                      to="register/">
                                    <Button
                                        onClick={handleCloseNavMenu}
                                        sx={{my: 2, color: 'white', display: 'block'}}
                                    > Register </Button>
                                </Link>
                                    <Link
                                        key="login"
                                        sx={{
                                            color: 'white',
                                            textDecoration: 'none'
                                        }}
                                        to="login/" >
                                        <Button
                                            onClick={handleCloseNavMenu}
                                            sx={{my: 2, color: 'white', display: 'block'}}
                                        > Login </Button>
                                    </Link>
                            </>
                        )}
                    </Toolbar>
                </Container>
            </AppBar>
        </>
    )
}
export default Navbar;