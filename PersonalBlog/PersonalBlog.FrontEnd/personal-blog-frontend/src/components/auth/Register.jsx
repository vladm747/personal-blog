import React, {useEffect} from 'react';
import AuthService from '../../services/AuthService.js';
import {
    Box,
    Button,Container, createTheme,
    CssBaseline,
    Grid, InputLabel, MenuItem, Select,
    TextField,
    ThemeProvider,
    Typography
} from "@mui/material";
import {Link, useNavigate} from "react-router-dom";


const Register = () => {
    const navigate = useNavigate();
    const defaultTheme = createTheme();
    const service = new AuthService();
    const [role, setRole] = React.useState('author');
    const [userId, setUserId] = React.useState();

    useEffect(() => {
        if (userId)
            navigate('/login');
    }, userId)

    const handleChange = (event) => {
        setRole(`${event.target.value}`);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        const registerDto=  {
            email: data.get("email"),
            nickName: data.get("nickName"),
            password: data.get("password"),
            role: role
        };

        const userId = await service.register(registerDto);
        setUserId(userId);
    };

    return (
        <ThemeProvider theme={defaultTheme}>
            <Container component="main" maxWidth="xs">
                <CssBaseline/>
                <Box
                    sx={{
                        marginTop: 8,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Typography component="h1" variant="h5">
                        Sign up
                    </Typography>
                    <Box component="form" noValidate onSubmit={handleSubmit} sx={{mt: 3}}>
                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <TextField
                                    required
                                    fullWidth
                                    id="nickName"
                                    label="Nick Name"
                                    name="nickName"
                                    autoComplete="nickName"
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    required
                                    fullWidth
                                    id="email"
                                    label="Email Address"
                                    name="email"
                                    autoComplete="email"
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    required
                                    fullWidth
                                    name="password"
                                    label="Password"
                                    type="password"
                                    id="password"
                                    autoComplete="new-password"
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <InputLabel id="role-ddl">Role</InputLabel>
                                <Select
                                    labelId="eole"
                                    id="role"
                                    value={role}
                                    label="role"
                                    onChange={handleChange}
                                >
                                    <MenuItem value={"user"}>User</MenuItem>
                                    <MenuItem value={"author"}>Author</MenuItem>
                                </Select>
                            </Grid>
                        </Grid>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{mt: 3, mb: 2}}
                        >
                            Sign Up
                        </Button>
                        <Grid container justifyContent="flex-end">
                            <Grid item>
                                <Link to="/login" variant="body2">
                                    Already have an account? Sign in
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
            </Container>
        </ThemeProvider>
    );
}
export default Register;