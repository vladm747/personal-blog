using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract.Base;

namespace PersonalBlog.DAL.Infrastructure.DI.Abstract;

public interface IPostRepository: IRepositoryBase<int, Post>{ }