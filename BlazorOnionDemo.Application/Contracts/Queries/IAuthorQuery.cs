namespace BlazorOnionDemo.Application.Contracts.Queries;

public interface IAuthorQuery
{
    Task<AuthorDto> GetById(int id);
    Task<List<AuthorDto>> GetAll();
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