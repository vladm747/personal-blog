using System.Net.Http.Headers;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PersonalBlog.Common.DTO;
using PersonalBlog.Common.DTO.Auth;
using PersonalBlog.DAL.Entities;
using Tests.IntegrationTests.TestSetup;

namespace Tests.IntegrationTests;

public class BlogControllerTests : IClassFixture<ApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly AuthFixture _authFixture;

    public BlogControllerTests(ApplicationFactory fixture)
    {
        _client = fixture.CreateClient();
        _authFixture = new AuthFixture(fixture);
    }

    [Fact]
    public async void GetAllBlogsAsync_ShouldReturn1Blog_WhenBlogsExists()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Blog/blogs");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var blogs = JsonConvert.DeserializeObject<List<Blog>>(result);

        // Assert
        Assert.NotNull(blogs);
        Assert.Single(blogs);
    }

    [Fact]
    public async void GetBlogByIdAsync_ShouldReturnBlog_WhenBlogExists()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Blog/blog/1");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var blog = JsonConvert.DeserializeObject<Blog>(result);

        // Assert
        Assert.NotNull(blog);
        Assert.Equal(1, blog.Id);
    }

    [Fact]
    public async void GetBlogByIdAsync_ShouldReturnNull_WhenBlogDoesntExist()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Blog/blog/2");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var blogs = string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<Blog>(result) : null;

        // Assert
        Assert.Null(blogs);
    }

    [Fact]
    public async void CreateBlogAsync_ShouldReturnBlog_WhenBlogCreated()
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


        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Blog/blog/");
        var json = JsonConvert.SerializeObject(userId);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        
        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var blog = JsonConvert.DeserializeObject<BlogDTO>(result);

        // Assert
        Assert.NotNull(blog);
        Assert.Equal(3, blog.Id);
    }

    [Fact]
    public async void DeleteBlogAsync_ShouldReturnTrue_WhenBlogDeleted()
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
        
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/Blog/blog/2");

        // Act
        var response = await _client.SendAsync(request);
        var resultJson = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<int>(resultJson);

        // Assert
        Assert.Equal(1, result);
    }
}