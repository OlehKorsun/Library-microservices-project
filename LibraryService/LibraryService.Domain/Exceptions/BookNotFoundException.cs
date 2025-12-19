namespace LibraryService.Domain.Exceptions;

public class BookNotFoundException : AppException
{
    public BookNotFoundException(int id) : base($"Book with id {id} was not found!") { }
}