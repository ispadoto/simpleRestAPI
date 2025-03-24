using Aspire.Hosting;
using Google.Protobuf.WellKnownTypes;

var builder = DistributedApplication.CreateBuilder(args);

// banco de dados SQL Server... 

var password = builder.AddParameter("password", "simpleRest@2025", secret: true); 
var sql = builder.AddSqlServer("MSSQL",password)
                 .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("SimpleRestDB");

// Backend para que o frontend possa consumir, atrelado ao SQL Server
var restAPI = builder.AddProject<Projects.Presentation_API>("presentation-api")
              .WithReference(db)
              .WaitFor(db);

// Depois o frontend Angular
builder.AddNpmApp("Frontend-Angular", "../Presentantion.AngularFront")
    .WithReference(restAPI)
    .WaitFor(restAPI)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

// Depois o frontend React
builder.AddNpmApp("Frontend-React", "../Presentation.ReactFront")
    .WithReference(restAPI)
    .WaitFor(restAPI)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
