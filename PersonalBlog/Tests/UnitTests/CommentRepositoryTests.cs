using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.DAL.Infrastructure.DI.Implementations;
using Tests.UnitTests.TestFixtures;

namespace Tests.UnitTests;

public class CommentRepositoryTests: IClassFixture<DatabaseSeedDataFixture>
{
    private readonly DatabaseSeedDataFixture _fixture;
    private readonly ICommentRepository _repository;

    public CommentRepositoryTests(DatabaseSeedDataFixture fixture)
    {
        _fixture = fixture;
        _repository = new CommentRepository(_fixture.Context);
    }
    
    [Fact]
    public async void GetAllCommentsAsync_ShouldReturn1Comment_WhenCommentsExist()
    {
        // Act
        var result = await _repository.GetAllAsync(1);
        var enumerable = result.ToList();
        
        // Assert
        Assert.NotNull(result);
        Assert.True(enumerable.Count() == 1);
    }
}