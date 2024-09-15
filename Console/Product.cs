using System.Text.Json;
using Bogus;

namespace Console;

public sealed class Product
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Categories { get; set; } = [];
    public double Price { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public sealed class ProductFaker : Faker<Product>
{
    public ProductFaker()
    {
        StrictMode(true);
        RuleFor(x => x.ProductId, f => Guid.NewGuid());
        RuleFor(x => x.Name, f => f.Commerce.ProductName());
        RuleFor(x => x.Description, f => f.Lorem.Sentence());
        RuleFor(x => x.Categories, f => f.Commerce.Categories(f.Random.Number(1, 5)).Distinct().ToList());
        RuleFor(x => x.Price, f => double.Parse(f.Commerce.Price()));
    }
}