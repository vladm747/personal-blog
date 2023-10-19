using PersonalBlog.DAL.Context;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.DAL.Infrastructure.DI.Implementations.Base;

namespace PersonalBlog.DAL.Infrastructure.DI.Implementations;

public class CommentRepository: RepositoryBase<int, Comment>, ICommentRepository
{
    public CommentRepository(PersonalBlogContext context): base(context) { }
}