import axios from "axios";
import {getApiUrl} from "../helpers/API_URL.js";

const API_URL = getApiUrl();
class UserService {

    me = () => { return axios.get(API_URL + "User/me") };

    getAllUsers() {
        return axios.get(API_URL + 'users');
    }

    deleteAccount(email) {
        return axios.delete(API_URL + 'user', email);
    }
}
export default UserService;