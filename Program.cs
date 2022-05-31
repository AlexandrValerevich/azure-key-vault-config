using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// It value can store in App Configuration or appsetting.json
var vaultName = builder.Configuration.GetValue<string>("VaultName");
var vaultUri = new Uri($"https://{vaultName}.vault.azure.net/");
// Authentification does through service(user)-managmnet identity
// withe created on Azure App Service
// so it's creacte private key(secret unreacheble Envirament varible) 
// and auth-server to communicate with AAD 
builder.Configuration.AddAzureKeyVault(vaultUri, new DefaultAzureCredential());





var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
