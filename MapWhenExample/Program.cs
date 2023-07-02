var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapWhen(context => context.Request.Query.ContainsKey("fileType") , HandleRequest);
app.Run(async context => await context.Response.WriteAsync("Your request is not correct"));

app.MapControllers();

app.Run();

void HandleRequest(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        var message = string.Empty;
        var fileType = context.Request.Query["fileType"].ToString().ToLower();

        if (fileType == "json")
            message = "Choosen file type is JSON and will be sent to your mail as soon as possible";
        else if (fileType == "xml")
            message = "Choosen file type is XML and will be sent to your mail as soon as possible";
        else message = "Unknown file type please make sure to write it correct";

        await context.Response.WriteAsync(message);
    });
}