import axios from "axios";
import {getApiUrl} from "../helpers/API_URL.js";

const API_URL = getApiUrl();
class BlogService {

    getBlogs = async () => axios.get(API_URL + 'Blog/blogs');

    getBlogById(blogId){
        return axios.get(API_URL + 'Blog/blog/' + blogId);
    }

    deleteBlog(blogId){
        return axios.delete(API_URL + 'Blog/blog/' + blogId);
    }
}

export default new BlogService();



