using GoodBurger.Api.Data;
using GoodBurger.Api.Interfaces;
using GoodBurger.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodBurger.Api.Services;

public class MenuService : IMenuService
{
    private readonly AppDbContext _context;

    public MenuService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Menu>> GetMenu()
    {
        return await _context.Cardapios.ToListAsync();
    }

    public async Task<Menu?> GetMenuById(int id)
    {
        return await _context.Cardapios
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Menu?> UpdateMenu(int id, Menu item)
    {
        try
        {
            var existingItem = await _context.Cardapios
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingItem == null)
            {
                return null;
            }
            existingItem.Nome = item.Nome;
            existingItem.Preco = item.Preco;

            await _context.SaveChangesAsync();
            return existingItem;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteMenu(int id)
    {
        var item = await _context.Cardapios.FindAsync(id);
        if (item == null) return false;

        _context.Cardapios.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Menu> CreateMenu(Menu novoMenu)
    {
    if (string.IsNullOrEmpty(novoMenu.Nome) || novoMenu.Preco <= 0)
    {
        throw new Exception("Dados inválidos para o novo hambúrguer.");
    }

    _context.Cardapios.Add(novoMenu);
    await _context.SaveChangesAsync();
    return novoMenu;
    }


}