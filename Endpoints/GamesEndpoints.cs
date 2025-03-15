using ASPNET_tutorial.DTOs;

namespace ASPNET_tutorial.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName = "GetGame";

        private static List<GameDTO> games = [
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


        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("games");

            // GET /games 
            group.MapGet("/", () => games);

            // GET /games/1
            group.MapGet("/{id}", (int id) =>
            {
                var game = games.Find(game => game.Id == id);

                return game is null ? Results.NotFound() : Results.Ok(game);

            }).WithName(GetGameEndpointName);

            // POST /games
            group.MapPost("/", (CreateGameDTO newGame) =>
            {
                GameDTO game = new(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price,
                    newGame.ReleaseDate
                    );
                games.Add(game);

                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
            });

            // PUT /games/1p
            group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame) =>
            {
                var index = games.FindIndex(game => game.Id == id);

                if (index == -1)
                {
                    return Results.NotFound();
                }

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
            group.MapDelete("/{id}", (int id) =>
            {
                games.RemoveAll(game => game.Id == id);

                return Results.NoContent();
            });

            return group;
        }
    }
}
