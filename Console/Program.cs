using Bogus;
using Console;

Randomizer.Seed = new Random(-1);
ProductFaker faker = new();
List<Product> products = faker.Generate(5);
foreach(Product product in products)
{
    System.Console.WriteLine(product);
}