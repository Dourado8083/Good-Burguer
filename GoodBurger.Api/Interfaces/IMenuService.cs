
using GoodBurger.Api.Models;

namespace GoodBurger.Api.Interfaces;

public interface IMenuService
{
    Task<List<Menu>> GetMenu();

    Task<Menu?> GetMenuById(int id);

    Task<Menu?> UpdateMenu(int id, Menu item);
  
    Task<bool> DeleteMenu(int id);

  //Usuario Administrador criar novos pedidos
  Task<Menu> CreateMenu(Menu novoMenu);
}