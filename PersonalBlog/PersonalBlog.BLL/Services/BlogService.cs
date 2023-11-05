﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.BLL.DTO;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Entities.Auth;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;

namespace PersonalBlog.BLL.Services;

public class BlogService: IBlogService
{
    private readonly IBlogRepository _repo;

    private readonly IMapper _mapper;
    
    public BlogService(IBlogRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BlogDTO>> GetAllAsync()
    {
        var blogs = await _repo.GetAllAsync();

        return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
    }
    
    public async Task<BlogDTO> GetByIdAsync(int blogId)
    {
        var blog = await _repo.FindByKeyAsync(blogId);

        if (blog == null)
            throw new KeyNotFoundException($"The blog with id {blogId} does not exist in database.");
        
        return _mapper.Map<BlogDTO>(blog);
    }

    public async Task CreateAsync(string userId)
    {
        var entity = new Blog()
        {
            UserId = userId
        };

        await _repo.CreateAsync(entity);
    }

    public async Task DeleteAsync(int blogId)
    {
        var blog = await _repo.FindByKeyAsync(blogId);

        if (blog == null)
            throw new KeyNotFoundException($"There is no blog with ID {blogId} in database.");

        await _repo.DeleteAsync(blog);
    }
}