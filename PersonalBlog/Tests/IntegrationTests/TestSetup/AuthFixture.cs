using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using PersonalBlog.API.Models;
using PersonalBlog.Common.DTO.Auth;

namespace Tests.IntegrationTests.TestSetup;

public class AuthFixture: IDisposable
{
    public HttpClient Client { get; }

    public AuthFixture(ApplicationFactory factory)
    {
        Client = factory.CreateClient();
    }

    public async Task<string> RegisterAsync(RegisterDTO registerDto)
    {
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
        var response = await Client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
    
    public async Task<Tokens> LoginAsync(string email, string password)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Auth/login");
        var user = new LoginDTO
        {
            Email = email,
            Password = password
        };
        var json = JsonConvert.SerializeObject(user);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await Client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<Tokens>(result);
        return token;
    }
    
    public async Task<UserDTO?> MeAsync(string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/User/me");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        
        var response = await Client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<UserDTO>(result);
        return user;
    }  
    
    public void Dispose()
    {
        Client.Dispose();
    }
}