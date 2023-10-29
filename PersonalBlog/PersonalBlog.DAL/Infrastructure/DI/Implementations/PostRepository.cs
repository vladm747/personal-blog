using PersonalBlog.DAL.Context;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.DAL.Infrastructure.DI.Implementations.Base;

namespace PersonalBlog.DAL.Infrastructure.DI.Implementations;

public class PostRepository: RepositoryBase<int, Post>, IPostRepository
{
    public PostRepository(PersonalBlogContext context): base(context) { }
}