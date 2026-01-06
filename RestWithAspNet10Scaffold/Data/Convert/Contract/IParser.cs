namespace RestWithAspNet10Scaffold.Data.Convert.Contract;

public interface IParser<TO, TD>
{
    TD Parser(TO origin);
    List<TO> ParserList(List<TD> origins);
}