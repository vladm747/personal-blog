import React, {Component} from "react";
import BlogService from "../../services/BlogService.js"
import {Box, Button, Card, CardActions, CardContent, Container, Typography} from "@mui/material";
const bull = (
    <Box
        component="span"
        sx={{ display: 'inline-block', mx: '2px', transform: 'scale(0.8)' }}
    >
        •
    </Box>
);
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

    componentDidMount(){
        this.fetchBlogs();
    }

    fetchBlogs = async () => {
        const response = await BlogService.getBlogs();
        if(response.data == null)
        {
            console.log("No blogs yet")
        }
        this.setState({blogs: response.data});
    }

    render() {
        return (
            <Container maxWidth="sm">
                <Box sx={{ bgcolor: '#cfe8fc', height: '100vh' }} >
                {
                    this.state.blogs.map(
                        blog => (
                            <Card key={blog.id} variant="outlined" sx={{ minWidth: 275 }}>
                                <CardContent>
                                    <Typography variant="h5" component="div">
                                        {blog.userNickName}
                                    </Typography>
                                </CardContent>
                                <CardActions>
                                    <Button variant="outlined" size="small">View Posts</Button>
                                </CardActions>
                            </Card>
                        ))
                }
                </Box>
            </Container>

        )
    }

}

export default BlogListComponent;