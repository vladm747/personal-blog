import axios from "axios";

const PERSONAL_BLOG_API_BASE_URL = "http://localhost:5189/";

class UserService {
    getAllUsers() {
        return axios.get(PERSONAL_BLOG_API_BASE_URL + 'users');
    }

    deleteAccount(email) {
        return axios.delete(PERSONAL_BLOG_API_BASE_URL + 'user', email);
    }
}
export default UserService;