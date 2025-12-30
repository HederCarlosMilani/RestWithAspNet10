using RestWithAspNet10Scaffold.Hypermedia.Abstract;

namespace RestWithAspNet10Scaffold.Hypermedia.Helpers;

public class PagedSearchDto<T> where T : ISupportsHypermidea
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalResults { get; set; }
    public string SortField { get; set; }
    public string SortDirection { get; set; } = "asc";
    public Dictionary<string, object> Filters { get; set; } = [];
    
    public List<T> List { get; set; } = [];
    
    public PagedSearchDto() {}
    
    public PagedSearchDto(int currentPage, int pageSize, string sortField, string sortDirection, Dictionary<string, object>? filters, List<T> list)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        SortField = sortField;
        SortDirection = sortDirection;
        Filters = filters ?? [];
        List = list;
    }

    public PagedSearchDto(int currentPage, string sortField, string sortDirection)
        : this(currentPage, 10, sortField, sortDirection, null, [])
    {}
    
    public int GetCurrentPage() => CurrentPage <= 0 ? 1 : CurrentPage;
    
    public int GetPageSize() => PageSize <= 0 ? 10 : PageSize;
}