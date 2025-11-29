var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment(".env");

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.AspireCiCdToCoolify_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.AspireCiCdToCoolify_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
