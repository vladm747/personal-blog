using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PersonalBlog.Common.DTO.Auth;
using PersonalBlog.DAL.Entities.Auth;
using Tests.IntegrationTests.TestSetup;

namespace Tests.IntegrationTests;

public class UserControllerTests : IClassFixture<ApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly AuthFixture _authFixture;

    public UserControllerTests(ApplicationFactory fixture)
    {
        _client = fixture.CreateClient();
        _authFixture = new AuthFixture(fixture);
    }
    
    [Fact]
    public async void GetAllUsersAsync_ShouldReturn1Users_WhenUsersExists()
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
        
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/User/users");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var users = JsonConvert.DeserializeObject<List<User>>(result);

        // Assert
        Assert.NotNull(users);
        Assert.Equal(3, users.Count);
    }
    
    [Fact]
    public async void DeleteUserAsync_ShouldReturnNull_WhenUserExists()
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
        
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/User/user?email=author@gmail.com");

        // Act
        var response = await _client.SendAsync(request);
        var getUsersRequest = new HttpRequestMessage(HttpMethod.Get, "/api/User/users");
        var getUsersResponse = await _client.SendAsync(getUsersRequest);
        var result = await getUsersResponse.Content.ReadAsStringAsync();
        var users = JsonConvert.DeserializeObject<List<User>>(result);
  
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(2, users.Count);
    }
    
    [Fact]
    public async void GetCurrentUser_ShouldReturnUser_WhenUserExists()
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
        
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/User/me");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<User>(result);
  
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("author@gmail.com", user.Email);
    }
}