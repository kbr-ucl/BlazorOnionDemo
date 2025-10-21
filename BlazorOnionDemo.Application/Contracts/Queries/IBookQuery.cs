namespace BlazorOnionDemo.Application.Contracts.Queries;

public interface IBookQuery
{
    Task<BookDto> GetById(int id);
    Task<List<BookDto>> GetAll();
}

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AuthorDto Author { get; set; }
}