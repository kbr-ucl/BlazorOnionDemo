namespace BlazorOnionDemo.Application.Contracts.Queries;

public interface IAuthorQuery
{
    Task<AuthorDto> GetByIdAsync(int id);
    Task<List<AuthorDto>> GetAllAsync();
}

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<AuthorBookDto> Books { get; set; }
}

public class AuthorBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}