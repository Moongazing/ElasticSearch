namespace Moongazing.ElasticSearch.Models;

public class ElasticSearchConfig
{
    public string ConnectionString { get; set; } = default!;
    public string? UserName { get; set; }
    public string? Password { get; set; }
}