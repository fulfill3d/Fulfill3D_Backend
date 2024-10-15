using Fulfill3D.API.API;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(builder =>
    {
        var configuration = builder.Build();
        var token = new DefaultAzureCredential();
        var appConfigUrl = configuration["AppConfigUrl"] ?? string.Empty;
        
        builder.AddAzureAppConfiguration(config =>
        {
            config.Connect(new Uri(appConfigUrl), token);
            config.ConfigureKeyVault(kv => kv.SetCredential(token));
        });
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        
        services.RegisterServices(recaptchaOptions =>
        {
            recaptchaOptions.Secret = configuration["Fulfill3DRecaptchaSecret"] ?? string.Empty;
        },apiOptions =>
        {
            apiOptions.ReceiverName = configuration["Fulfill3D_FormRequestReceiverName"] ?? string.Empty;
            apiOptions.EmailTo = configuration["Fulfill3D_FormRequestEmailTo"] ?? string.Empty;
            apiOptions.EmailSubject = configuration["Fulfill3D_FormRequestEmailSubject"] ?? string.Empty;
            apiOptions.SenderName = configuration["Email_FromName"] ?? string.Empty;
            apiOptions.EmailFrom = configuration["Email_FromEmail"] ?? string.Empty;
        }, sendGridOptions =>
        {
            sendGridOptions.ApiKey = configuration["SendGridApiKey"] ?? string.Empty;
        }, cosmosOptions =>
        {
            cosmosOptions.EndpointUri = configuration["Fulfill3dCosmosEndpointUri"] ?? string.Empty;
            cosmosOptions.PrimaryKey = configuration["Fulfill3dCosmosPrimaryKey"] ?? string.Empty;
            cosmosOptions.DatabaseId = configuration["Fulfill3dCosmosDatabaseId"] ?? string.Empty;
            cosmosOptions.ContainerId = configuration["Fulfill3dCosmosContainerId"] ?? string.Empty;
        });
    })
    .Build();

host.Run();