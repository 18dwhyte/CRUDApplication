using GameStore.Api.Entities;

var  policyName = "_myAllowSpecificOrigins";


List<Game> games = new(){
    new Game(){
        Id = 1,
        Name = "Grand Theft Auto 5",
        Genre = "Fighting",
        Price = 59.99M,
        ReleaseDate = new DateTime(2015, 7, 23),
        
    },
    new Game(){
        Id = 2,
        Name = "Super Mario Bros",
        Genre = "Adventure",
        Price = 39.99M,
        ReleaseDate = new DateTime(2010, 2, 1),
        
    },
    new Game(){
        Id = 3,
        Name = "Undertale",
        Genre = "Adventure",
        Price = 19.99M,
        ReleaseDate = DateTime.Now,
       
    }
};

const string GetGameEndpointName = "GetGame";
var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                      });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) => {
    
    Game? game = games.Find(game => game.Id ==id);
    if(game == null){
        return Results.NotFound();
    }
    return Results.Ok(game);

})
.WithName(GetGameEndpointName);

app.MapPost("/games", (Game game) => {
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
});

app.MapPut("/games/{id}", (int id, Game updatedGame) => {
    Game? existingGame = games.Find(game => game.Id ==id);
    if(existingGame == null){
        return Results.NotFound();
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
    

});

app.MapDelete("/games/{id}", (int id) => {
    Game? existingGame = games.Find(game => game.Id ==id);
    if(existingGame == null){
        return Results.NotFound();
    }
    games.Remove(existingGame);
    return Results.NoContent();
});
app.Run();
