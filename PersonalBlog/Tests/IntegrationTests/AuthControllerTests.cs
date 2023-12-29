using System.Net.Http.Headers;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PersonalBlog.API.Models;
using PersonalBlog.Common.DTO.Auth;
using Tests.IntegrationTests.TestSetup;

namespace Tests.IntegrationTests;

public class AuthControllerTests:  IClassFixture<ApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly AuthFixture _authFixture;
    
    public AuthControllerTests(ApplicationFactory fixture)
    {
        _client = fixture.CreateClient();
        
        _authFixture = new AuthFixture(fixture);
    }

    [Fact]
    public async void RegisterAsync_ShouldReturn200_WhenUserIsCreated()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Auth/register");
        var user = new RegisterDTO
        {
            NickName = "author 2",
            Email = "author@gmail.com",
            Password = "Password@1",
            Role = "author"
        };
        
        var json = JsonConvert.SerializeObject(user);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        
        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(!result.IsNullOrEmpty());
    }

    [Fact]
    public async void LoginAsync_ShouldReturn200_WhenUserIsLoggedIn()
    {
        // Arrange
        var userId = await _authFixture.RegisterAsync(new RegisterDTO
        {
            NickName = "author 2",
            Email = "author@gmail.com",
            Password = "Password@1",
            Role = "author"
        });
        
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Auth/login");
        var user = new LoginDTO
        {
            Email = "author@gmail.com",
            Password = "Password@1"
        };
        var json = JsonConvert.SerializeObject(user);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        
        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<Tokens>(result);
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(!token.AccessToken.IsNullOrEmpty());
        Assert.True(!token.RefreshToken.IsNullOrEmpty());
    }

    [Fact]
    public async void RefreshTokenAsync_ShouldReturn200_WhenTokenIsRefreshed()
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
        
        var request = new HttpRequestMessage(HttpMethod.Post, 
            "/api/Auth/test-refresh-token");
        
        var json = JsonConvert.SerializeObject(tokens.RefreshToken);
        
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.SendAsync(request);   
        var newAccessToken = await response.Content.ReadAsStringAsync();
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(!newAccessToken.IsNullOrEmpty());
        Assert.True(newAccessToken != tokens.AccessToken);
    }
}