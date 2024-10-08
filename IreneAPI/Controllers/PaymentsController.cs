using IreneAPI.Services;
using IreneAPI.Models;
using IreneAPI.Data;
using IreneAPI.DTOs;
using IreneAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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

    // The URI is GET: api/payments/{id}
    [Authorize(Roles = "User")]
    [HttpGet("{id}")]
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
