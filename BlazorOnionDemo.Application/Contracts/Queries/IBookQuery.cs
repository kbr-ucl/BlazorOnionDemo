namespace BlazorOnionDemo.Application.Contracts.Queries;

public interface IBookQuery
{
    Task<BookDto> GetByIdAsync(int id);
    Task<List<BookDto>> GetAllAsync();
}

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public BookAuthorDto Author { get; set; }
}

public class BookAuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }    
}