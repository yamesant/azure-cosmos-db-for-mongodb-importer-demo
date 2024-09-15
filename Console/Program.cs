using Bogus;
using Console;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ShellProgressBar;

Randomizer.Seed = new Random(-1);
ProductFaker faker = new();

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

int numberOfProducts = 50_000;
int chunkSize = 1000;
int numberOfChunks = numberOfProducts / chunkSize;
int delayTimeInMilliseconds = 100;
using ProgressBar progressBar = new(numberOfChunks, "Uploading products");
for (int i = 0; i < numberOfChunks; i++)
{
    List<Product> products = faker.Generate(chunkSize);
    collection.InsertMany(products);
    progressBar.Tick();
    Thread.Sleep(delayTimeInMilliseconds);
}