using QueryBuilder;
using QueryBuilder.Models;

var database = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + "\\Data\\SQLiteDatabase.db";

Console.WriteLine(database);

List<Author> authors;
using (var qb = new QueryBuilder.QueryBuilder(database))
{
    Console.WriteLine($"If you would like to create a second author hit enter...");
    Console.ReadLine();
    var sk = new Author(77, "Lucky", "Duck");
    qb.Create<Author>(sk);

    Console.WriteLine($"Here is a list of all the current authors!");
    authors = qb.ReadAll<Author>();
    foreach(var a in authors)
    {
        Console.WriteLine(a);
    }

    var sk1 = new Author(77, "Unlucky", "Frog");

    Console.WriteLine($"If you would like me to update one of the authors hit enter...");
    Console.ReadLine();
    qb.Update<Author>(sk1);

    Console.WriteLine($"Here is an updated list of all the authors!");
    authors = qb.ReadAll<Author>();
    foreach (var a in authors)
    {
        Console.WriteLine(a);
    }

    Console.WriteLine($"If you would like the second author to be removed hit enter...");
    Console.ReadLine();
    qb.Delete<Author>(sk1);

    Console.WriteLine($"Here is an updated list of all the authors!");
    authors = qb.ReadAll<Author>();
    foreach (var a in authors)
    {
        Console.WriteLine(a);
    }

}