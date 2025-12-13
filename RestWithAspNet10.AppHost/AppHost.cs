var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.RestWithAspNet10Scaffold>("restwithaspnet10scaffold");

builder.Build().Run();
