using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Squares.Api.Repositories;
using Squares.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//** Mapper

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));//   *

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
});//   *


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISquaresRepository, InMemSquaresRepository>();    //  *DI
//builder.Services.AddSingleton<IPointsRepository, InMemPointsRepository>();    //  *DI
builder.Services.AddSingleton<IPointsRepository, MongoDbPointsRepository>();    //  *DI


builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});//  *For async





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
