import axios from "axios";
import {getApiUrl} from "../helpers/API_URL.js";

const API_URL = getApiUrl();
class PostService {

    static getPosts(blogId) {
        return axios.get(API_URL + 'posts'+'/' + blogId);
    }

    createPost(post) {
        return axios.post(API_URL + 'post', post);
    }

    getPostById(postId) {
        return axios.get(API_URL + 'post/' + postId);
    }

    updatePost(post, postId) {
        return axios.put(API_URL + 'post/' + postId, post);
    }

    deletePost(postId) {
        return axios.delete(API_URL + 'post/' + postId);
    }
}
export default PostService;