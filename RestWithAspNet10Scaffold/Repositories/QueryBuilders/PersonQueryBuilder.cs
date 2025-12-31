namespace RestWithAspNet10Scaffold.Repositories.QueryBuilders;

public class PersonQueryBuilder
{
    public (string query, string countQuery, string sort, int size, int offset) BuildQueries(string? name, string sortDirection, int pageSize, int page)
    {
        page = Math.Max(1, page);
        
        var offset = (page - 1) * pageSize;
        var size = pageSize < 1 ? 1 : pageSize;
        
        var sort = (!string.IsNullOrWhiteSpace(sortDirection) && sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase)) ? "desc" : "asc";
        
        var baseQuery = $@"FROM Person p WHERE 1=1";
        
        if (!string.IsNullOrWhiteSpace(name)) baseQuery += $" AND (p.first_name LIKE '%{name}%')";
        
        var countQuery = $"SELECT COUNT(*) {baseQuery}";
        var query = $@"SELECT * {baseQuery} ORDER BY p.first_name {sort} OFFSET {offset} ROWS FETCH NEXT {size} ROWS ONLY";
        
        return (query, countQuery, sort, size, offset);
    }
}