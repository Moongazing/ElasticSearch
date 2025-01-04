using Moongazing.ElasticSearch;
using Moongazing.ElasticSearch.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var elasticSearchConfig = builder.Configuration.GetSection("ElasticSearchConfig").Get<ElasticSearchConfig>();
if (elasticSearchConfig == null)
{
    throw new InvalidOperationException("ElasticSearchConfig section is missing in appsettings.json");
}

builder.Services.AddSingleton(elasticSearchConfig);
builder.Services.AddSingleton<IElasticSearchService, ElasticSearchService>(sp =>
{
    var config = sp.GetRequiredService<ElasticSearchConfig>();
    return new ElasticSearchService(config);
});

var app = builder.Build();

app.MapGet("/api/indices", (IElasticSearchService elasticSearchService) =>
{
    var indices = elasticSearchService.GetIndexList();
    return Results.Ok(indices);
});

app.MapPost("/api/index", async (IElasticSearchService elasticSearchService, IndexModel indexModel) =>
{
    if (indexModel == null)
        return Results.BadRequest(new { Message = "Index model cannot be null." });

    var result = await elasticSearchService.CreateNewIndexAsync(indexModel);
    return result.Success
        ? Results.Ok(result)
        : Results.StatusCode((int)HttpStatusCode.InternalServerError);
});

app.MapDelete("/api/index/{indexName}", async (IElasticSearchService elasticSearchService, string indexName, ElasticSearchModel model) =>
{
    if (string.IsNullOrEmpty(indexName))
        return Results.BadRequest(new { Message = "Index name cannot be null or empty." });

    var result = await elasticSearchService.DeleteByElasticIdAsync(model);
    return result.Success
        ? Results.Ok(result)
        : Results.StatusCode((int)HttpStatusCode.InternalServerError);
});

app.MapPost("/api/insert", async (IElasticSearchService elasticSearchService, ElasticSearchInsertUpdateModel model) =>
{
    if (model == null)
        return Results.BadRequest(new { Message = "Model cannot be null." });

    var result = await elasticSearchService.InsertAsync(model);
    return result.Success
        ? Results.Ok(result)
        : Results.StatusCode((int)HttpStatusCode.InternalServerError);
});

app.MapPut("/api/update", async (IElasticSearchService elasticSearchService, ElasticSearchInsertUpdateModel model) =>
{
    if (model == null)
        return Results.BadRequest(new { Message = "Model cannot be null." });

    var result = await elasticSearchService.UpdateByElasticIdAsync(model);
    return result.Success
        ? Results.Ok(result)
        : Results.StatusCode((int)HttpStatusCode.InternalServerError);
});

app.MapPost("/api/search/all", async (IElasticSearchService elasticSearchService, SearchParameters parameters) =>
{
    if (parameters == null)
        return Results.BadRequest(new { Message = "Search parameters cannot be null." });

    var results = await elasticSearchService.GetAllSearch<object>(parameters);
    return Results.Ok(results);
});

app.MapPost("/api/search/field", async (IElasticSearchService elasticSearchService, SearchByFieldParameters fieldParameters) =>
{
    if (fieldParameters == null)
        return Results.BadRequest(new { Message = "Field parameters cannot be null." });

    var results = await elasticSearchService.GetSearchByField<object>(fieldParameters);
    return Results.Ok(results);
});

app.MapPost("/api/search/query", async (IElasticSearchService elasticSearchService, SearchByQueryParameters queryParameters) =>
{
    if (queryParameters == null)
        return Results.BadRequest(new { Message = "Query parameters cannot be null." });

    var results = await elasticSearchService.GetSearchBySimpleQueryString<object>(queryParameters);
    return Results.Ok(results);
});

app.MapPost("/api/insert/many", async (IElasticSearchService elasticSearchService, string indexName, object[] items) =>
{
    if (string.IsNullOrEmpty(indexName))
        return Results.BadRequest(new { Message = "Index name cannot be null or empty." });
    if (items == null || items.Length == 0)
        return Results.BadRequest(new { Message = "Items cannot be null or empty." });

    var result = await elasticSearchService.InsertManyAsync(indexName, items);
    return result.Success
        ? Results.Ok(result)
        : Results.StatusCode((int)HttpStatusCode.InternalServerError);
});

app.Run();
