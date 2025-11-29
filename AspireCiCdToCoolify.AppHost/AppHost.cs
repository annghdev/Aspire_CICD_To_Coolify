var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("artifacts");

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.AspireCiCdToCoolify_ApiService>("myapi")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.AspireCiCdToCoolify_Web>("myweb")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
