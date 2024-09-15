using Bogus;
using Console;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

Randomizer.Seed = new Random(-1);
ProductFaker faker = new();
List<Product> products = faker.Generate(5);

IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();
string configSectionName = "ProductsCollectionConfig";
string connectionString = config[$"{configSectionName}:ConnectionString"]!;
string databaseName = config[$"{configSectionName}:DatabaseName"]!;
string collectionName = config[$"{configSectionName}:CollectionName"]!;
MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
MongoClient client = new(settings);
IMongoDatabase database = client.GetDatabase(databaseName);
IMongoCollection<Product> collection = database.GetCollection<Product>(collectionName);

System.Console.WriteLine(collection.EstimatedDocumentCount());