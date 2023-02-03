using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Authors;

public class AuthorAppService_Tests : BookStoreApplicationTestBase
{
    private readonly IAuthorAppService _authorAppService;

    public AuthorAppService_Tests()
    {
        _authorAppService = GetRequiredService<IAuthorAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Authors_Without_Any_Filter()
    {
        var result = await _authorAppService.GetListAsync(new GetAuthorListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(author => author.Name == "Tomislav Wittenberg");
        result.Items.ShouldContain(author => author.Name == "Branko Živković");
    }

    [Fact]
    public async Task Should_Get_Filtered_Authors()
    {
        var result = await _authorAppService.GetListAsync(new GetAuthorListDto { Filter = "Wittenberg" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(author => author.Name == "Tomislav Wittenberg");
        result.Items.ShouldNotContain(author => author.Name == "Branko Živković");
    }

    [Fact]
    public async Task Should_Create_A_New_Author()
    {
        var authorDto = await _authorAppService.CreateAsync(
            new CreateAuthorDto
            {
                Name = "Vladimir Hip",
                BirthDate = new DateTime(1850, 05, 22),
                ShortBio = "Vladimir Hip je rođen..."
            }
        );

        authorDto.Id.ShouldNotBe(Guid.Empty);
        authorDto.Name.ShouldBe("Vladimir Hip");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Author()
    {
        await Assert.ThrowsAsync<AuthorAlreadyExistsException>(async () =>
        {
            await _authorAppService.CreateAsync(
                new CreateAuthorDto
                {
                    Name = "Tomislav Wittenberg",
                    BirthDate = DateTime.Now,
                    ShortBio = "..."
                }
            );
        });
    }
}