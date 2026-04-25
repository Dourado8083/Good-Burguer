using GoodBurger.Api.Interfaces;
using GoodBurger.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodBurger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService) => _menuService = menuService;
  //Gets
    [HttpGet]
    public async Task<ActionResult<List<Menu>>> Get() => Ok(await _menuService.GetMenu());

    [HttpGet("{id}")]
    public async Task<ActionResult<Menu>> GetById(int id)
    {
        var item = await _menuService.GetMenuById(id);
        return item == null ? NotFound() : Ok(item);
    }
  //Put
    [HttpPut("{id}")]
public async Task<ActionResult<Menu>> Put(int id, [FromBody] Menu item)
{
    if (id != item.Id)
    {
        return BadRequest("O ID informado na URL não coincide com o ID do objeto.");
    }

    try
    {
        var itemAtualizado = await _menuService.UpdateMenu(id, item);

        if (itemAtualizado == null)
        {
            return NotFound($"Item do cardápio com ID {id} não encontrado.");
        }
        return Ok(itemAtualizado);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro interno ao atualizar o cardápio: {ex.Message}");
    }

}
 [HttpPost]
    public async Task<ActionResult<Menu>> PostMenu(Menu novoMenu)
    {
        var orderItem = await _menuService.CreateMenu(novoMenu);
        return Ok(orderItem);
    }
    //Delete Lanche
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sucesso = await _menuService.DeleteMenu(id);
        
        if (!sucesso)
            return NotFound("Lanche não encontrado no banco.");

        return NoContent(); // Retorno padrão para deleção com sucesso (204)
    }
}