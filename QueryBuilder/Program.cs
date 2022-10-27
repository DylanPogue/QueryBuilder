using QueryBuilder;
using QueryBuilder.Models;

var database = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + "\\Data\\Library.db";

Books a;
List<Author> authors;
using (var qb = new QueryBuilder.QueryBuilder(database))
{
    var ids = 2;
    var sk = new Author(77, "Lucky", "Duck");
    qb.Create<Author>(sk);
    authors = qb.ReadAll<Author>();

    var sk1 = new Author(77, "Unlucky", "Frog");
    qb.Update<Author>(sk1);
    authors = qb.ReadAll<Author>();

}