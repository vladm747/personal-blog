import {Component} from "react";
import PostService from "../../services/PostService.js";


class PostListComponent extends Component{
    constructor(props) {
        super(props);

        this.state = ({ posts: [] })
    }

    componentDidMount() {
        PostService.getPosts().then((res) => {
                if(res.data == null)
                    console.log("no posts yet :(((")

                this.setState({posts: res.data});
            }
        )
    }

    render() {
        return (
            <div className="container">
                <h2 className="text-center">Posts</h2>

                {
                    this.state.posts.map(post =>
                        <div className="panel panel-primary">
                            <div className="panel-heading">
                                <p className="text-left">{post.UserNickName}</p>
                                <p className="text-right">{post.PublicationDate}</p>
                            </div>
                            <div className="panel-body">
                                <p>{post.Content}</p>
                            </div>
                        </div>
                    )
                }

            </div>
        )
    }
}

export default PostListComponent;