using IreneAPI.Services;
using IreneAPI.Models;
using IreneAPI.Data;
using IreneAPI.DTOs;
using IreneAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
// using System.ComponentModel.DataAnnotations;

namespace IreneAPI.Controllers;

[Authorize] // To protect payment processing endpoints with Authorization
[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase, IPaymentService
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    // The URI is GET: api/payments
    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<List<Payment>> GetAllPaymentsAsync()
    {
        return await _paymentService.GetAllPaymentsAsync();
    }

    // GET: api/payments/{id}
    // How to document an endpoint
    /// <summary>
    ///     Retrieves a specific payment by a unique id
    /// </summary>
    /// <remarks>API Endpoint</remarks>
    /// <response code="200">Payment found</response>
    /// <response code="400">Payment has invalid details</response>
    /// <response code="500">Oops! Payment can not be gotten right now</response>
    [Authorize(Roles = "User")]
    [HttpGet("{id}")]
    // I don't think the 3 lines below are Data annotations; Because we need the System.Computations.DataAnnotations namespace for that
    [ProducesResponseType(typeof(Payment), 200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(500)]
    public async Task<Payment> GetPaymentByIdAsync(int id)
    {
        return await _paymentService.GetPaymentByIdAsync(id);
        
    }
    // POST: api/payments
    [Authorize(Roles = "Merchant, Admin, Developer")]
    [HttpPost]
    public async Task CreatePaymentAsync(PaymentDto newPaymentDto)
    {
        await _paymentService.CreatePaymentAsync(newPaymentDto);
    }

    // PUT: api/payments/{id}
    [Authorize(Roles = "Admin, Developer, Merchant")]
    [HttpPut("{id}")]
    public async Task UpdatePaymentAsync(int id, [FromBody] PaymentDto editPaymentDto)
    {
        await _paymentService.UpdatePaymentAsync(id, editPaymentDto);
    }

    // DELETE: api/payments/{id}
    [Authorize(Roles = "Admin")]
    // The line below is a .NET attribute to create a URI
    [HttpDelete("{id}")]
    public async Task DeletePaymentAsync(int id)
    {
        await _paymentService.DeletePaymentAsync(id);
    }

    // POST /api/payments/process - For processing a payment: This now uses the URI POST /api/payments/process. This ensures there’s no conflict with PostPayment.
    // This is just a demo, the actual ProcessPayment ha snot yet been implemented
    [HttpPost("process")]
    public IActionResult ProcessPaymentAsync([FromBody] PaymentRequest payment)
    {
        // Payment Processing logic
        return Ok("Payment processed successfully");
    }
}

// Controllers should only handle HTTP requests, and calling services for the business logic
