namespace Moongazing.ElasticSearch.Models;

public class ElasticSearchInsertManyModel : ElasticSearchModel
{
    public object[] Items { get; set; } = default!;
}