using GoodBurger.Api.Data;
using GoodBurger.Api.Interfaces;
using GoodBurger.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodBurger.Api.Services;

public class OrderServices : IOrderService
{
        private readonly AppDbContext _context;
        private readonly ILogger<OrderServices> _logger;

    public OrderServices(AppDbContext context,ILogger<OrderServices> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Order> CreateOrder(Order novoOrder)
    {
   // Preços
    var precos = new Dictionary<string, decimal>
    {
        { "X Burger", 5.00m },
        { "X Egg", 4.50m },
        { "X Bacon", 7.00m }
    };

    if (!precos.ContainsKey(novoOrder.SanduicheNome!))
        throw new ArgumentException("Sanduíche inválido.");

    // Calcula subtotal
    decimal subtotal = precos[novoOrder.SanduicheNome!];
    if (novoOrder.TemBatata)      subtotal += 2.00m;
    if (novoOrder.TemRefrigerante) subtotal += 2.50m;

    // Regras de desconto
    decimal percentual = 0;

    if (novoOrder.TemBatata && novoOrder.TemRefrigerante)
        percentual = 0.20m;
    else if (novoOrder.TemRefrigerante)
        percentual = 0.15m;
    else if (novoOrder.TemBatata)
        percentual = 0.10m;

    novoOrder.Subtotal  = subtotal;
    novoOrder.Desconto  = subtotal * percentual;
    novoOrder.TotalFinal = subtotal - novoOrder.Desconto;

    _context.Pedidos.Add(novoOrder);
    await _context.SaveChangesAsync();

    return novoOrder;
    }
    

    public async Task<bool> DeleteOrder(int id)
    {
 if (id <= 0)
    {
        _logger.LogWarning("ID inválido fornecido para exclusão: {Id}", id);
        return false;
    }

    try
    {
        // 1. Busca o pedido no banco antes de deletar
        // Verifique se o DbSet no seu AppDbContext se chama 'pedidos' ou 'Pedidos'
        var order = await _context.Pedidos
            .FirstOrDefaultAsync(p => p.Id == id);

        // 2. Verifica se o pedido existe
        if (order == null)
        {
            _logger.LogWarning("Pedido com ID {Id} não encontrado para exclusão", id);
            return false;
        }

        // 3. Remove do contexto e salva as alterações no banco
        _context.Pedidos.Remove(order);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Pedido deletado com sucesso: {Name} (ID: {Id})", 
            order.SanduicheNome, id);

        return true;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Erro ao deletar pedido com ID: {Id}", id);
        // O 'throw' repassa o erro para o Controller tratar (geralmente gerando um 500)
        throw;
    }
    }

    public async Task<List<Order>> GetAllOrder()
    {
        return await _context.Pedidos.ToListAsync();
    }

    public async Task<Order> GetOrderById(int id)
    {
    if (id <= 0)
        {
            _logger.LogWarning("ID inválido fornecido: {Id}", id); // 4. Use _logger
            return null;
        }

        try
        {
            _logger.LogInformation("Buscando pedido com Id: {Id}", id);

            // Agora o await vai funcionar corretamente
            var order = await _context.Pedidos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (order == null)
            {
                _logger.LogWarning("Pedido com Id {Id} não encontrado", id);
                return null;
            }

            _logger.LogInformation("Pedido encontrado para o sanduíche: {Name}", order.SanduicheNome);
            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar pedido por ID: {Id}", id);
            throw;
        }
    }


    public async Task<Order?> UpdateOrder(int id, Order updatedOrder)
    {
      if (id <= 0)
    {
        _logger.LogWarning("ID inválido fornecido para atualização: {Id}", id);
        return null;
    }

    try
    {
        // 1. Busca o pedido existente no banco
        var existingOrder = await _context.Pedidos
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existingOrder == null)
        {
            _logger.LogWarning("Pedido com ID {Id} não encontrado para atualização", id);
            return null;
        }

        // 2. Atualiza os campos baseados na estrutura da sua tabela
        existingOrder.SanduicheNome = updatedOrder.SanduicheNome;
        existingOrder.TemBatata = updatedOrder.TemBatata;
        existingOrder.TemRefrigerante = updatedOrder.TemRefrigerante;
        existingOrder.Subtotal = updatedOrder.Subtotal;
        existingOrder.Desconto = updatedOrder.Desconto;
        existingOrder.TotalFinal = updatedOrder.TotalFinal;

        // 3. Persiste as mudanças
        await _context.SaveChangesAsync();

        _logger.LogInformation("Pedido ID {Id} atualizado com sucesso. Novo Total: {Total}", 
            id, existingOrder.TotalFinal);

        return existingOrder;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Erro ao atualizar pedido ID: {Id}", id);
        throw;
    }
    }
}