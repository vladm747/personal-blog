import axios from "axios";
import {getApiUrl} from "../helpers/API_URL.js";

const API_URL = getApiUrl();
axios.defaults.withCredentials = true;
class AuthService {
    login = async (loginDto) => axios.post(API_URL + 'Auth/login', loginDto);

    register = async (registerDto) => axios.post(API_URL + 'Auth/register', registerDto);

    signOut = async () => axios.post(API_URL + 'Auth/sign-out');

    refreshToken = async () => axios.post(API_URL + 'Auth/refresh-token');
}
export default AuthService;