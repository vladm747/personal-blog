import axios from "axios";
import {getApiUrl} from "../helpers/API_URL.js";

const API_URL = getApiUrl();
class CommentService {

    getComments(postId) {
        return axios.get(API_URL + 'comments/' + postId);
    }

    createComment(comment) {
        return axios.post(API_URL + 'comment/', comment);
    }

    getCommentById(commentId) {
        return axios.get(API_URL + 'comment/' + commentId);
    }

    updateComment(comment, commentId) {
        return axios.put(API_URL + 'comment/' + commentId, comment);
    }

    deleteComment(commentId) {
        return axios.delete(API_URL + 'comment/' + commentId);
    }
}
export default CommentService;