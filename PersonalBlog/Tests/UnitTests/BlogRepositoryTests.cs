using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Entities.Auth;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.DAL.Infrastructure.DI.Implementations;
using Tests.UnitTests.TestFixtures;

namespace Tests.UnitTests;

public class BlogRepositoryTests: IClassFixture<DatabaseSeedDataFixture>
{
    private readonly DatabaseSeedDataFixture _fixture;
    private readonly IBlogRepository _repository;

    public BlogRepositoryTests(DatabaseSeedDataFixture fixture)
    {
        _fixture = fixture;
        _repository = new BlogRepository(_fixture.Context);
    }
    
    [Fact]
    public async void GetAllBlogsAsync_ShouldReturn2Blogs_WhenBlogsExists()
    {
        // Act
        var result = await _repository.GetAllAsync();
        var enumerable = result.ToList();
        
        // Assert
        Assert.NotNull(result);
        Assert.True(enumerable.Count() == 2);
    }
    
    [Fact]
    public async void GetBlogByIdAsync_ShouldReturnBlog_WhenBlogExists()
    {
        // Arrange
        var blogId = 1;

        // Act
        var result = await _repository.GetByIdAsync(blogId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(blogId, result.Id);
    }
    
    [Fact]
    public async void GetBlogByIdASync_ShouldReturnNull_WhenBlogDoesntExist()
    {
        // Arrange
        var blogId = 5;
        
        // Act
        var result = await _repository.GetByIdAsync(blogId);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async void DeleteBlogAsync_ShouldReturn3_WhenBlogExists()
    {
        // Arrange
        var blogId = 1;
        var blogToDelete = await _repository.GetByIdAsync(blogId);
        
        // Act
        var result = await _repository.DeleteAsync(blogToDelete);

        // Assert
        Assert.Equal(3, result);
    }
    
    [Fact]
    public async void CreateBlogAsync_ShouldReturn_WhenBlogExists()
    {
        // Arrange
        var userId = "98ef8882-d443-4593-be9c-3570da9a8f67";
        var blog = new Blog
        {
            UserId = userId
        };
        
        // Act
        var result = await _repository.CreateAsync(blog);
        
        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async void DeleteBlogAsync_ShouldReturnNull_WhenBlogDoesntExist()
    {
        // Arrange
        Blog blogToDelete = new Blog
        {
            Id = 5,
            UserId = "98ef8882-d443-4593-be9c-3570da9a8f67"
        };
        
        // Act
        try{
            var result = await _repository.DeleteAsync(blogToDelete);
        }
        catch (Exception e)
        {
            // Assert
            Assert.IsType<DbUpdateConcurrencyException>(e);
        }    
    }
}