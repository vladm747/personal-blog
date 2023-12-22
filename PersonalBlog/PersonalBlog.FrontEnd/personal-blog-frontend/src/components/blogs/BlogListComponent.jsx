import React, {Component, useState} from "react";
import BlogService from "../../services/BlogService.js"

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
            <div className="container-xl">
                <div className="bg-white py-24 sm:py-32">
                    <div className="mx-auto max-w-7xl px-6 lg:px-8">
                        <div className="mx-auto max-w-2xl lg:mx-0">
                            <h2 className="text-3xl font-bold tracking-tight text-gray-900 sm:text-4xl">List of
                                Blogs</h2>
                        </div>
                        <div
                            className="mx-auto mt-10 grid max-w-2xl grid-cols-1 gap-x-8 gap-y-16 border-t border-gray-200 pt-10 sm:mt-16 sm:pt-16 lg:mx-0 lg:max-w-none lg:grid-cols-3">
                            {
                                this.state.blogs.map(
                                    blog => (
                                        <div key={blog.id}
                                              className="bg-white dark:bg-slate-800 rounded-lg px-6 py-8 ring-1 ring-slate-900/5 shadow-xl">
                                            <div>
                                                <span className="inline-flex items-center justify-center p-2 bg-indigo-500 rounded-md shadow-lg">
                                                  <svg className="h-6 w-6 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                                                       stroke="currentColor" aria-hidden="true"></svg>
                                                </span>
                                            </div>
                                            <h3 className="text-slate-900 dark:text-white mt-5 text-base font-medium tracking-tight">{blog.userNickName}</h3>
                                            <p className="text-slate-500 dark:text-slate-400 mt-2 text-sm">
                                                    <button onClick={() => this.postsList(blog.id)}
                                                            className="bg-sky-600 rounded-lg border-solid border-2 ext-center w-2/6 h-8 text-white border-black">
                                                        View Posts
                                                    </button>
                                            </p>
                                        </div>
                                ))
                            }
                        </div>
                    </div>
                </div>
            </div>

        )
    }
}

export default BlogListComponent;