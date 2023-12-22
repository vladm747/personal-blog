import axios from "axios";

const PERSONAL_BLOG_API_BASE_URL = "http://localhost:5189/";

class AuthService {
    login(loginDto) {
        return axios.post(PERSONAL_BLOG_API_BASE_URL + 'login', loginDto);
    }

    register(registerDto) {
        return axios.post(PERSONAL_BLOG_API_BASE_URL + 'register', registerDto);
    }

    signOut() {
        return axios.post(PERSONAL_BLOG_API_BASE_URL + 'signout');
    }
}
export default AuthService;