namespace BlazorOnionDemo.Application.Contracts.Commands;

public interface IBookCommand
{
    Task CreateAsync(BookCreateCommandDto dto);
}
