using GoodBurger.Api.Interfaces;
using GoodBurger.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodBurger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class OrderController : ControllerBase
{

    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;
    public OrderController(IOrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }
    //Gets
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAll()
    {
        var orders = await _orderService.GetAllOrder();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { error = "Código do pedido não pode ser vazio" });

        }
        var order = _orderService.GetOrderById(id);


        return Ok(order);
    }

    //Put
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Order order)
    {
        // 1. Validação básica de segurança
        if (id <= 0)
        {
            return BadRequest(new { error = "O ID fornecido é inválido." });
        }

        // 2. Verifica se o ID da URL coincide com o ID do objeto enviado (boa prática)
        // Se o seu modelo 'Order' usa 'Id' com 'I' maiúsculo, ajuste abaixo
        if (id != order.Id)
        {
            return BadRequest(new { error = "O ID da URL não corresponde ao ID do objeto enviado." });
        }

        try
        {
            // 3. Chama a Service que implementamos anteriormente
            var updatedOrder = await _orderService.UpdateOrder(id, order);

            // 4. Se a service retornar null, significa que o pedido não existe no banco
            if (updatedOrder == null)
            {
                return NotFound(new { message = $"Pedido com ID {id} não encontrado." });
            }

            // 5. Retorna 204 No Content (padrão para PUT) ou 200 OK com o objeto atualizado
            return Ok(updatedOrder);
        }
        catch (Exception ex)
        {
            // O log detalhado já é feito na Service, aqui retornamos o erro para o cliente
            return StatusCode(500, new { error = "Erro interno ao processar a atualização.", details = ex.Message });
        }
    }
    //Delete

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleteOrder = await _orderService.DeleteOrder(id);

            if (!deleteOrder)
                return NotFound(new { error = $"Pedido com código {id} não encontrado para exclusão." });

            _logger.LogInformation("Pedido deletado com sucesso: {Code}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar pedidoi: {Id}", id);
            return StatusCode(500, new { error = "Erro interno do servidor ao deletar pedido." });
        }


    }

    //Post
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Order request)
    {
        try
        {
            var order = await _orderService.CreateOrder(new Order
            {
                SanduicheNome = request.SanduicheNome,
                TemBatata = request.TemBatata,
                TemRefrigerante = request.TemRefrigerante
            });

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

   
}