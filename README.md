ElasticSearch API Project

This project provides a Minimal API implementation for interacting with ElasticSearch. It includes functionalities for creating, managing, and querying indices and documents using a structured, dependency-injected service approach. The configuration is managed via appsettings.json, and the project adheres to clean architecture principles.

Features

Create, delete, and manage ElasticSearch indices.

Insert single or multiple documents into an index.

Update documents by their ElasticSearch ID.

Query documents with:

Full-text search

Field-based search

Simple query string search

List all indices available in the ElasticSearch instance.

Technologies Used

ASP.NET Core Minimal API

ElasticSearch (via Nest and Elasticsearch.Net libraries)

Dependency Injection

Configuration Management

C# 10 and .NET 6/7

Prerequisites

ElasticSearch Instance: Ensure ElasticSearch is running locally or on a server accessible by the application.

.NET SDK: Install .NET SDK 6 or later.

appsettings.json Configuration:

{
  "ElasticSearchConfig": {
    "ConnectionString": "http://localhost:9200"
  }
}

Setup Instructions

1. Clone the Repository

git clone <repository-url>
cd <repository-directory>

2. Restore Dependencies

Run the following command to restore required NuGet packages:

dotnet restore

3. Configure ElasticSearch

Edit the appsettings.json file to provide your ElasticSearch instance details:

{
  "ElasticSearchConfig": {
    "ConnectionString": "http://your-elasticsearch-instance:9200"
  }
}

4. Build and Run the Application

dotnet run

The API will be hosted on http://localhost:5000 (or as configured).

API Endpoints

Indices Management

Get All Indices: GET /api/indices

Create Index: POST /api/index

Request Body:

{
  "IndexName": "string",
  "AliasName": "string",
  "NumberOfShards": int,
  "NumberOfReplicas": int
}

Document Management

Insert Document: POST /api/insert

Request Body:

{
  "IndexName": "string",
  "ElasticId": "string",
  "Item": { "key": "value" }
}

Insert Multiple Documents: POST /api/insert/many

Request Body:

{
  "IndexName": "string",
  "Items": [ { "key": "value" }, { "key": "value" } ]
}

Update Document: PUT /api/update

Request Body:

{
  "IndexName": "string",
  "ElasticId": "string",
  "Item": { "key": "value" }
}

Delete Document by ID: DELETE /api/index/{indexName}

Path Parameter: indexName (string)

Request Body:

{
  "ElasticId": "string"
}

Search Operations

Search All Documents: POST /api/search/all

Request Body:

{
  "IndexName": "string",
  "From": int,
  "Size": int
}

Search by Field: POST /api/search/field

Request Body:

{
  "IndexName": "string",
  "Field": "string",
  "Value": "string",
  "From": int,
  "Size": int
}

Search with Query: POST /api/search/query

Request Body:

{
  "IndexName": "string",
  "Query": "string",
  "Fields": ["string"],
  "From": int,
  "Size": int
}

Error Handling

All endpoints return appropriate HTTP status codes:

200 OK: Successful operation.

400 Bad Request: Invalid input.

500 Internal Server Error: ElasticSearch errors or application exceptions.

Contribution Guidelines

Fork the repository.

Create a feature branch.

Commit your changes.

Submit a pull request.

License

This project is licensed under the MIT License. See the LICENSE file for details.

Notes

Ensure that the ElasticSearch instance has sufficient resources to handle your workload.

You can customize ElasticSearchService to include additional features as required.
