using GamesApi.Models;
using GamesApi.Data;
using Microsoft.AspNetCore.Mvc; 
namespace GamesApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase {
    [HttpGet]
    public ActionResult<List<Game>> GetAll() {
        return Ok(GameStore.Games);
    }
    [HttpGet("{id}")]
    public ActionResult<Game> GetById(int id) {
        var game = GameStore.Games.FirstOrDefault(g => g.Id == id);
        if (game is null) {
            return NotFound(new { message = $"игра с id = {id} не найден " });

        }
        return Ok(game);
    }
    [HttpPost]
    public ActionResult<Game> Creatr([FromBody] Game game) {
        game.Id = GameStore.NextId();
        GameStore.Games.Add(game);
        return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);

    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id) {
        var game = GameStore.Games.FirstOrDefault(g => g.Id == id);
        if (game is null) {
            return NotFound(new { message = $"Игра с id = {id} не найдена " });

        }
        GameStore.Games.Remove(game);
        return NoContent();
    }
    [HttpPut("{id}")]
    public ActionResult<Game> Update(int id , [FromBody] Game updared) {
        var game = GameStore.Games.FirstOrDefault(g => g.Id == id);
        if (game is null) {
            return NotFound(new { message = $"игра с id = {id} не найдена " });
        }
        game.Title = updared.Title ;
        game.Genre = updared.Genre;
        game.ReleaseYear = updared.ReleaseYear;
        return Ok(game); 
    }
}