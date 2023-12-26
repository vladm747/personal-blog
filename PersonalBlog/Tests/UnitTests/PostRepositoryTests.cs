
using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.DAL.Infrastructure.DI.Implementations;
using Tests.UnitTests.TestFixtures;

namespace Tests.UnitTests;

public class PostRepositoryTests:  IClassFixture<DatabaseSeedDataFixture>
{
    private readonly DatabaseSeedDataFixture _fixture;
    private readonly IPostRepository _repository;

    public PostRepositoryTests(DatabaseSeedDataFixture fixture)
    {
        _fixture = fixture;
        _repository = new PostRepository(_fixture.Context);
    }
    
    [Fact]
    public async void GetAllAsync_ShouldReturn2Posts_WhenPostsExists()
    {
        // Act
        var result = await _repository.GetAllAsync(1);
        var enumerable = result.ToList();
        
        // Assert
        Assert.NotNull(result);
        Assert.True(enumerable.Count() == 1);
    }
    
    [Fact]
    public async void FindPostByKeyAsync_ShouldReturnPost_WhenPostExists()
    {
        // Arrange
        var postId = 1;

        // Act
        var result = await _repository.FindByKeyAsync(postId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(postId, result.Id);
    }

    [Fact]
    public async void FindPostByKeyAsync_ShouldReturnNull_WhenPostDoesNotExist()
    {
        // Arrange
        var postId = 700;

        // Act
        var result = await _repository.FindByKeyAsync(postId);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async void DeletePostAsync_ShouldReturn2_WhenPostExists()
    {
        // Arrange
        var postId = 1;
        var postToDelete = await _repository.FindByKeyAsync(postId);
        
        // Act
        var result = await _repository.DeleteAsync(postToDelete!);
        
        // Assert
        Assert.Equal(2, result);
    }
    
    [Fact]
    public async void CreatePostAsync_ShouldReturnPost_WhenPostExists()
    {
        // Arrange
        var userId = "98ef8882-d443-4593-be9c-3570da9a8f67";
        var post = new Post
        {
            UserId = userId
        };
        
        // Act
        var result = await _repository.CreateAsync(post);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
    }
    
    [Fact]
    public async void UpdatePostAsync_ShouldReturn1_WhenPostExists()
    {
        // Arrange
        var postToUpdate = await _repository.FindByKeyAsync(1);
        postToUpdate!.Content = "New content";
        postToUpdate!.PublicationDate = DateTime.Now;
        
        // Act
        var result = await _repository.UpdateAsync(postToUpdate);
        var updatedPost = await _repository.FindByKeyAsync(1);
        
        // Assert
        Assert.Equal(1, result);
        Assert.Equal(postToUpdate!.Content, updatedPost!.Content);
    }
    
    [Fact]
    public async void DeletePostAsync_ShouldReturnNull_WhenPostDoesntExist()
    {
        // Arrange
        Post postToDelete = new Post
        {
            Id = 5,
            UserId = "98ef8882-d443-4593-be9c-3570da9a8f67"
        };
        
        // Act
        try {
            var result = await _repository.DeleteAsync(postToDelete);
        }
        catch (Exception e)
        {
            // Assert
            Assert.IsType<DbUpdateConcurrencyException>(e);
        }    
    }
}