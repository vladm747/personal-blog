﻿using PersonalBlog.Common.DTO;

namespace PersonalBlog.BLL.Interfaces;

public interface IBlogService
{
    Task<IEnumerable<BlogDTO>> GetAllAsync();
    Task<BlogDTO> GetByIdAsync(int blogId);
    Task<BlogDTO> CreateAsync(string userId);
    Task<int> DeleteAsync(int blogId);
}