using QueryBuilder;
using QueryBuilder.Models;

var database = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + "\\Data\\SQLiteDatabase.db";

Console.WriteLine(database);

Books a;
List<Author> authors;
using (var qb = new QueryBuilder.QueryBuilder(database))
{
    Console.WriteLine($"If you would like to create a second author hit enter...");
    var ids = 2;
    var sk = new Author(77, "Lucky", "Duck");
    qb.Create<Author>(sk);

    Console.WriteLine($"Here is a list of all the current authors!");
    authors = qb.ReadAll<Author>();

    Console.WriteLine($"If you would like me to updater one of the authors hit enter...");
    var sk1 = new Author(77, "Unlucky", "Frog");
    qb.Update<Author>(sk1);

    Console.WriteLine($"Here is an updated list of all the authors!");
    authors = qb.ReadAll<Author>();

    Console.WriteLine($"If you would like the second author to be removed hit enter...");
    qb.Delete<Author>(sk1);

    Console.WriteLine($"Here is an updated list of all the authors!");
    authors = qb.ReadAll<Author>();

}