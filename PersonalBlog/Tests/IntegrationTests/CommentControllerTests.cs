using Newtonsoft.Json;
using PersonalBlog.Common.DTO;
using Tests.IntegrationTests.TestSetup;

namespace Tests.IntegrationTests;

public class CommentControllerTests: IClassFixture<ApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly AuthFixture _authFixture;

    public CommentControllerTests(ApplicationFactory fixture)
    {
        _client = fixture.CreateClient();
        _authFixture = new AuthFixture(fixture);
    }
    
    [Fact]
    public async void GetAllCommentsAsync_ShouldReturn2Comments_WhenCommentsExists()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Comment/comments/1");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var comments = JsonConvert.DeserializeObject<List<CommentDTO>>(result);

        // Assert
        Assert.NotNull(comments);
        Assert.Equal(2, comments.Count);
    }
    
    [Fact]
    public async void GetCommentByIdAsync_ShouldReturnComment_WhenCommentExists()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Comment/comment/1");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var comment = JsonConvert.DeserializeObject<CommentDTO>(result);

        // Assert
        Assert.NotNull(comment);
        Assert.Equal(1, comment.Id);
    }
    
    [Fact]
    public async void GetCommentByIdAsync_ShouldReturnNull_WhenCommentDoesntExist()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Comment/comment/3");

        // Act
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var comment = JsonConvert.DeserializeObject<CommentDTO>(result);

        // Assert
        Assert.Null(comment);
    }
}