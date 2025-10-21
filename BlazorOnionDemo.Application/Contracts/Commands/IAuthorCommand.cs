namespace BlazorOnionDemo.Application.Contracts.Commands;

public interface IAuthorCommand
{
    Task Create(AuthorCreateCommandDto dto);
    Task AddBookToAuthor(AddBookToAuthorCommandDto dto);
}

public class AddBookToAuthorCommandDto
{
    public int AuthorId { get; set; }
    public int BookId { get; set; }
}

public class AuthorCreateCommandDto
{
    public string Name { get; set; }
}