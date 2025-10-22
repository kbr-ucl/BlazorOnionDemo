namespace BlazorOnionDemo.Application.Contracts.Commands;

public interface IAuthorCommand
{
    Task CreateAsync(AuthorCreateCommandDto dto);
    Task AddBookToAuthorAsync(AddBookToAuthorCommandDto dto);
}

public class AddBookToAuthorCommandDto
{
    public int AuthorId { get; set; }
    public BookCreateCommandDto Book { get; set; }
}

public class AuthorCreateCommandDto
{
    public string Name { get; set; }
}

public class BookCreateCommandDto
{
    public string Title { get; set; }
    public string Description { get; set; }
}