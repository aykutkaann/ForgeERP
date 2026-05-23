using ForgeERP.Catalog.Application.BomUseCases.Commands;
using ForgeERP.Catalog.Application.BomUseCases.Interfaces;
using ForgeERP.Catalog.Application.ItemUseCases.Commands;
using ForgeERP.Catalog.Application.ItemUseCases.Interfaces;
using ForgeERP.Catalog.Application.ItemUseCases.Queries;
using ForgeERP.Catalog.Infrastructure.Persistence;
using ForgeERP.Catalog.Infrastructure.Repositories;
using ForgeERP.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

});


builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IBomRepository, BomRepository>();
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CatalogDbContext>());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateItemCommand).Assembly));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Item endpoints
app.MapPost("/api/catalog/items", async(CreateItemCommand command, IMediator mediator)=>
{
    var result = await mediator.Send(command);
    return result.IsSuccess ? Results.Created($"/api/catalog/items/{result.Value}", result.Value) : Results.BadRequest(result.Error);

});

app.MapGet("/api/catalog/items/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var result = await mediator.Send(new GetItemByIdQuery(id));

    return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error); 
});


app.MapGet("/api/catalog/items", async (IMediator mediator) =>
{
    var result = await mediator.Send(new ListItemsQuery());

    return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
});

//BOM endpoints

app.MapPost("/api/catalog/boms", async (CreateBomCommand command, IMediator mediator) =>
{
    var result = await mediator.Send(command);

    return result.IsSuccess ? Results.Created($"/api/catalog/boms/{result.Value}", result.Value) : Results.BadRequest(result.Error);
});

app.MapPost("/api/catalog/boms/{id:guid}/lines", async (Guid id, AddBomLineCommand command, IMediator mediator) =>
{
    var commandWithId = command with { BomId = id };

    var result = await mediator.Send(commandWithId);
    return result.IsSuccess ? Results.Created($"/api/catalog/boms/{id}/lines/{result.Value}", result.Value) : Results.BadRequest(result.Error);
});


app.Run();
