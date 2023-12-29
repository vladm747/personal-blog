using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using PersonalBlog.Common.DTO;
using PersonalBlog.Common.DTO.Auth;
using PersonalBlog.DAL.Entities;
using Tests.IntegrationTests.TestSetup;

namespace Tests.IntegrationTests;

public class PostControllerTests : IClassFixture<ApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly AuthFixture _authFixture;

    public PostControllerTests(ApplicationFactory fixture)
    {
        _client = fixture.CreateClient();
        _authFixture = new AuthFixture(fixture);
    }

    [Fact]
    public async void GetAllPostsAsync_ShouldReturn2Posts_WhenPostsExists()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Post/posts/1");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var posts = JsonConvert.DeserializeObject<List<Post>>(result);

        // Assert
        Assert.NotNull(posts);
        Assert.Equal(2, posts.Count);
    }

    [Fact]
    public async void GetPostByIdAsync_ShouldReturnPost_WhenPostExists()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Post/post/1");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var post = JsonConvert.DeserializeObject<Post>(result);

        // Assert
        Assert.NotNull(post);
        Assert.Equal(1, post.Id);
    }

    [Fact]
    public async void GetPostByIdAsync_ShouldReturnNull_WhenPostDoesntExist()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Post/post/3");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var post = string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<Post>(result) : null;

        // Assert
        Assert.Null(post);
    }

    [Fact]
    public async void CreatePostAsync_ShouldReturnPost_WhenAuthorWhenPostCreated()
    {
        // Arrange
        var userId = await _authFixture.RegisterAsync(new RegisterDTO
        {
            NickName = "author 2",
            Email = "author@gmail.com",
            Password = "Password@1",
            Role = "author"
        });

        var tokens = await _authFixture.LoginAsync("author@gmail.com", "Password@1");

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        var user = await _authFixture.MeAsync(tokens.AccessToken);

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Post/post");

        var post = new PostDTO()
        {
            Id = 3,
            Content = "Test Content",
            UserNickName = user.NickName,
            UserId = userId,
            BlogId = 2
        };

        var json = JsonConvert.SerializeObject(post);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var createdPost = JsonConvert.DeserializeObject<PostDTO>(result);

        // Assert
        Assert.NotNull(createdPost);
        Assert.Equal(post.BlogId, createdPost.BlogId);
        Assert.Equal(post.Id, createdPost.Id);
    }

    [Fact]
    public async void UpdatePostAsync_ShouldReturnPost_WhenAuthorWhenPostUpdated()
    {
        // Arrange
        var userId = await _authFixture.RegisterAsync(new RegisterDTO
        {
            NickName = "author 2",
            Email = "author@gmail.com",
            Password = "Password@1",
            Role = "author"
        });

        var tokens = await _authFixture.LoginAsync("author@gmail.com", "Password@1");

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        var user = await _authFixture.MeAsync(tokens.AccessToken);

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Post/post");

        var post = new PostDTO()
        {
            Id = 3,
            Content = "Test Content",
            UserNickName = user.NickName,
            UserId = userId,
            BlogId = 2
        };

        var json = JsonConvert.SerializeObject(post);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var createdPost = JsonConvert.DeserializeObject<PostDTO>(result);

        // Act
        var updateRequest = new HttpRequestMessage(HttpMethod.Put, "/api/Post/post");
        var updatePost = new PostDTO()
        {
            Id = createdPost.Id,
            Content = "Updated Content",
            UserNickName = user.NickName,
            UserId = userId,
            BlogId = 2
        };

        var updateJson = JsonConvert.SerializeObject(updatePost);
        updateRequest.Content = new StringContent(updateJson, Encoding.UTF8, "application/json");

        var updateResponse = await _client.SendAsync(updateRequest);
        var updateResult = await updateResponse.Content.ReadAsStringAsync();
        var updatedPost = JsonConvert.DeserializeObject<PostDTO>(updateResult);

        // Assert
        Assert.NotNull(updatedPost);
        Assert.False(updatePost.Content == updatedPost.Content);
    }

    [Fact]
    public async void DeletePostAsync_ShouldReturnTrue_WhenPostDeleted()
    {
        // Arrange
        var userId = await _authFixture.RegisterAsync(new RegisterDTO
        {
            NickName = "author 2",
            Email = "author@gmail.com",
            Password = "Password@1",
            Role = "author"
        });

        var tokens = await _authFixture.LoginAsync("author@gmail.com", "Password@1");

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        var user = await _authFixture.MeAsync(tokens.AccessToken);

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Post/post");

        var post = new PostDTO()
        {
            Id = 3,
            Content = "Test Content",
            UserNickName = user.NickName,
            UserId = userId,
            BlogId = 2
        };

        var json = JsonConvert.SerializeObject(post);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var createdPost = JsonConvert.DeserializeObject<PostDTO>(result);
        
        // Act
        var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/Post/post/{createdPost.Id}");
        var deleteResponse = await _client.SendAsync(deleteRequest);
        var deleteResult = await deleteResponse.Content.ReadAsStringAsync();
        var deletedPost = JsonConvert.DeserializeObject<int>(deleteResult);
        
        // Assert
        Assert.Equal(1, deletedPost);
    }

}