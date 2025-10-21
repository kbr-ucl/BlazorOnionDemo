namespace BlazorOnionDemo.Application.Contracts.Commands;

public interface IBookCommand
{
    Task Create(BookCreateCommandDto dto);
}

public class BookCreateCommandDto
{
    public string Title { get; set; }
    public string Description { get; set; }
}