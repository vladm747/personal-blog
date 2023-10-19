using PersonalBlog.DAL.Context;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.DAL.Infrastructure.DI.Implementations.Base;

namespace PersonalBlog.DAL.Infrastructure.DI.Implementations;

public class BlogRepository: RepositoryBase<int, Blog>, IBlogRepository
{
    public BlogRepository(PersonalBlogContext context):base(context) { }
}