using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract.Base;

namespace PersonalBlog.DAL.Infrastructure.DI.Abstract;

public interface ICommentRepository : IRepositoryBase<int, Comment>
{
    Task<IEnumerable<Comment>> GetAllAsync(int postId);
}