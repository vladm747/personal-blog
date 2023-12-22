import axios from "axios";

const PERSONAL_BLOG_API_BASE_URL = "http://localhost:5189/";

class PostService {

    static getPosts(blogId) {
        return axios.get(PERSONAL_BLOG_API_BASE_URL + 'posts'+'/' + blogId);
    }

    createPost(post) {
        return axios.post(PERSONAL_BLOG_API_BASE_URL + 'post', post);
    }

    getPostById(postId) {
        return axios.get(PERSONAL_BLOG_API_BASE_URL + 'post/' + postId);
    }

    updatePost(post, postId) {
        return axios.put(PERSONAL_BLOG_API_BASE_URL + 'post/' + postId, post);
    }

    deletePost(postId) {
        return axios.delete(PERSONAL_BLOG_API_BASE_URL + 'post/' + postId);
    }
}
export default PostService;