using ASPNET_tutorial.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDTO> games = [
    new (
        1,
        "Call of Duty 1",
        "Shooter",
        19.99M,
        new DateOnly(1995, 7, 15)),
    new (
        2,
        "Final Fantasy XIV",
        "RPG",
        59.99M,
        new DateOnly(2010, 9, 30)),
    new (
        3,
        "League of Legends",
        "MOBA",
        0.00M,
        new DateOnly(2009, 2, 10)),
    ];

// GET /games 
app.MapGet("games", () => games);

// GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDTO newGame) =>
{
    GameDTO game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
        );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
});

// PUT /games/1
app.MapPut("games/{id}", (int id, UpdateGameDTO updatedGame) =>
{
    var index = games.FindIndex(game => game.Id == id);

    games[index] = new GameDTO(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
     );

    return Results.NoContent();
});

// DELETE /games/1
app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
