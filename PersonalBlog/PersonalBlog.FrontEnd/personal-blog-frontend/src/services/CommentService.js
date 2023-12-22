import axios from "axios";

const PERSONAL_BLOG_API_BASE_URL = "http://localhost:5189/";


class CommentService {

    getComments(postId) {
        return axios.get(PERSONAL_BLOG_API_BASE_URL + 'comments/' + postId);
    }

    createComment(comment) {
        return axios.post(PERSONAL_BLOG_API_BASE_URL + 'comment/', comment);
    }

    getCommentById(commentId) {
        return axios.get(PERSONAL_BLOG_API_BASE_URL + 'comment/' + commentId);
    }

    updateComment(comment, commentId) {
        return axios.put(PERSONAL_BLOG_API_BASE_URL + 'comment/' + commentId, comment);
    }

    deleteComment(commentId) {
        return axios.delete(PERSONAL_BLOG_API_BASE_URL + 'comment/' + commentId);
    }
}
export default CommentService;