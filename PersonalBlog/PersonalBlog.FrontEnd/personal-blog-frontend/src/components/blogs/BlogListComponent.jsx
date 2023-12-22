import React, {Component, useState} from "react";
import BlogService from "../../services/BlogService.js";
// Bootstrap CSS
import "bootstrap/dist/css/bootstrap.min.css";
// Bootstrap Bundle JS
import "bootstrap/dist/js/bootstrap.bundle.min";
class BlogListComponent extends Component {
    constructor(props) {
        super(props);

        this.state = {
            blogs: []
        }

    }
    postsList(id){
        this.props.history.push(`/posts-list/${id}`)
    };

    async componentDidMount(){
        const response = await BlogService.getBlogs();
        if(response.data == null)
        {
            console.log("No blogs yet")
        }
        this.setState({blogs: response.data});
    }

    render() {
        debugger;
        return (
            <div>
                <h2 className="text-center">Blogs List</h2>
                {
                    this.state.blogs.map(
                        blog => (
                        <div key={blog.id} className="panel panel-default">
                            <div className="panel-heading">
                                <h3 className="panel-title">{blog.userNickName}</h3>
                            </div>
                            <div className="panel-body">
                                <button onClick={() => this.postsList(blog.id)} className="btn btn-info">
                                    View Posts
                                </button>
                            </div>
                        </div>
                    ))
                }
            </div>
        )
    }
}
export default BlogListComponent;