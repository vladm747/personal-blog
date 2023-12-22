import axios from "axios";

const PERSONAL_BLOG_API_BASE_URL = "http://localhost:5189/api/Blog/";

class BlogService {

    getBlogs = async () => axios.get(PERSONAL_BLOG_API_BASE_URL + 'blogs');

    getBlogById(blogId){
        return axios.get(PERSONAL_BLOG_API_BASE_URL + 'blog/' + blogId);
    }

    deleteBlog(blogId){
        return axios.delete(PERSONAL_BLOG_API_BASE_URL + 'blog/' + blogId);
    }
}

export default new BlogService();



