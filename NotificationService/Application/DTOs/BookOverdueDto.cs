namespace Application.DTOs;

public record BookOverdueDto
{
    public int Id { get; init; }
    public string Email  { get; init; }
    public string BookTitle { get; init; }
    public DateOnly DueDate { get; init; }
}