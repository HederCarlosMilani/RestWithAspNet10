using FluentAssertions;
using RestWithAspNet10Scaffold.Repositories.QueryBuilders;

namespace RestWithAspNet10Scafold.Tests;

public class PersonQueryBuilderTests
{
    private readonly PersonQueryBuilder _personQueryBuilder;
    
    public PersonQueryBuilderTests()
    {
        _personQueryBuilder = new PersonQueryBuilder();
    }

    [Fact]
    public void BuildQueries_ShouldReturnCorrectQueryString_WhenAllParametersAreProvided()
    {
        // Arrange
        var name = "John";
        var sortDirection = "asc";
        var pageSize = 10;
        var pageNumber = 1;
        
        // Act
        var (query, countQuery, sort, size, offset) = _personQueryBuilder.BuildQueries(name, sortDirection, pageSize, pageNumber);
        
        // Assert
        query.Should().NotBeNull();
        query.Should().Be(@$"SELECT * FROM Person p WHERE 1=1 AND (p.first_name LIKE '%{name}%') ORDER BY p.first_name {sortDirection} OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY");
        
        countQuery.Should().NotBeNull();
        countQuery.Should().Be(@$"SELECT COUNT(*) FROM Person p WHERE 1=1 AND (p.first_name LIKE '%{name}%')");
        
        sort.Should().Be(sortDirection);
        
        size.Should().Be(pageSize);
        offset.Should().Be((pageNumber - 1) * pageSize);
    }

    [Fact]
    public void BuildQueries_ShouldHandleMissingNameParameter()
    {
        // Arrange
        string? name = null;
        var sortDirection = "desc";
        var pageSize = 5;
        var pageNumber = 2;

        // Act
        var (query, countQuery, sort, size, offset) =
            _personQueryBuilder.BuildQueries(name, sortDirection, pageSize, pageNumber);

        // Assert
        query.Should().NotBeNull();
        query.Should()
            .Be(
                @$"SELECT * FROM Person p WHERE 1=1 ORDER BY p.first_name {sortDirection} OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY");

        countQuery.Should().NotBeNull();
        countQuery.Should().Be(@$"SELECT COUNT(*) FROM Person p WHERE 1=1");

        sort.Should().Be(sortDirection);
        size.Should().Be(pageSize);
        offset.Should().Be((pageNumber - 1) * pageSize);
    }

    [Fact]
    public void BuildQueries_ShouldDefaultToAscSort_WhenInvalidSortDirectionIsProvided()
    {
        // Arrange
        var name = "Jane";
        var sortDirection = "invalid";
        var pageSize = 15;
        var pageNumber = 1;

        // Act
        var (query, countQuery, sort, size, offset) =
            _personQueryBuilder.BuildQueries(name, sortDirection, pageSize, pageNumber);

        // Assert
        query.Should().NotBeNull();
        query.Should()
            .Be(
                @$"SELECT * FROM Person p WHERE 1=1 AND (p.first_name LIKE '%{name}%') ORDER BY p.first_name asc OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY");

    }
}