import React, {useContext, useEffect, useState} from 'react';
import AuthService from '../../services/AuthService.js';
import {
    Box,
    Button,
    Container,
    createTheme,
    CssBaseline,
    Grid,
    TextField,
    ThemeProvider,
    Typography
} from "@mui/material";

import {Link, useNavigate} from "react-router-dom";
import UserService from "../../services/UserService.js";
import {NavbarContext} from "../../App.jsx";
const defaultTheme = createTheme();

const SignIn = () => {
    const { user, setUser } = useContext(NavbarContext);
    debugger;
    const [tokens, setTokens] =
        React.useState({accessToken: '', refreshToken: ''});
    const userService = new UserService();
    //const { user, setUser } = context;

    const navigate = useNavigate();
    const service = new AuthService();
    const handleSubmit = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        const loginDto = {
            email: data.get('email'),
            password: data.get('password')
        };
        const loginResult = await service.login(loginDto);
        setTokens({
            accessToken: loginResult.data.accessToken,
            refreshToken: loginResult.data.refreshToken
        });
    };

    useEffect(() => {
        debugger;
        if(tokens.accessToken){
            setUser(() => {
                const response =  userService.me()
                return response.data;
            })
            navigate('/');
        }
    }, [tokens])

    return (
        <ThemeProvider theme={defaultTheme}>
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <Box
                    sx={{
                        marginTop: 8,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Typography component="h1" variant="h5">
                        Sign in
                    </Typography>
                    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="email"
                            label="Email Address"
                            name="email"
                            autoComplete="email"
                            autoFocus
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Sign In
                        </Button>
                        <Grid container>
                            <Grid item>
                                <Link to="/register" variant="body2">
                                    {"Don't have an account? Sign Up"}
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
            </Container>
        </ThemeProvider>
    );
}

export default SignIn;