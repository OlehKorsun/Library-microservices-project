namespace LibraryService.Application.DTOs;

public class PagedBooks<T>
{
    public IEnumerable<T> Books  { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages =>  (int)Math.Ceiling((double)TotalCount / PageSize);
}