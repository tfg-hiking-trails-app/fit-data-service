using System.Text;
using MongoDB.Driver;

namespace FitDataService.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext()
    {
        string connectionString = GetDefaultConnectionToDatabase();
        
        MongoClient client = new MongoClient(connectionString);
        _database = client.GetDatabase(GetDatabaseName());
    }

    public IMongoCollection<T> GetCollection<T>()
    {
        return _database.GetCollection<T>(ToSnakeCase(typeof(T).Name)); 
    }
    
    private string GetDefaultConnectionToDatabase()
    {
        string server = Environment.GetEnvironmentVariable("MONGO_SERVER") ?? "";
        string user = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_USERNAME") ?? "";
        string password = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_PASSWORD") ?? "";
        
        return $"mongodb://{user}:{password}@{server}:27017";
    }
    
    private string GetDatabaseName()
    {
        return Environment.GetEnvironmentVariable("MONGO_INITDB_DATABASE") ?? "";
    }

    private string ToSnakeCase(string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;
        
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < value.Length; i++)
        {
            if (char.IsUpper(value[i]))
            {
                if (i > 0)
                    result.Append("_");
                
                result.Append(char.ToLower(value[i]));
            }
            else
                result.Append(value[i]);
        }

        return result.ToString();
    }
    
}