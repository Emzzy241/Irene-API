using  Microsoft.AspNetCore.Mvc;
using IreneAPI.Services;
using IreneAPI.Models;
using IreneAPI.Data;
using IreneAPI.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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

    
    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<List<Payment>> GetAllPaymentsAsync()
    {
        return await _paymentService.GetAllPaymentsAsync();
    }

    [Authorize(Roles = "User")]
    [HttpGet("{id}")]
    public Payment GetPaymentByIdAsync(int id)
    {
        return _paymentService.GetPaymentByIdAsync(id);
        
    }

    [Authorize(Roles = "Merchant, Admin, Developer")]
    [HttpPost]
    public async Task AddPaymentAsync(Payment newPayment)
    {
        await _paymentService.AddPaymentAsync(newPayment);
    }

    [Authorize(Roles = "Admin, Developer, Merchant")]
    [HttpPut("{id}")]
    public async Task UpdatePaymentAsync(int id, [FromBody] Payment newPayment)
    {
        await _paymentService.UpdatePaymentAsync(id, newPayment);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task DeletePaymentAsync(int id)
    {
        await _paymentService.DeletePaymentAsync(id);
    }

    // POST /api/payments/process - For processing a payment: This now uses the route POST /api/payments/process. This ensures there’s no conflict with PostPayment.
    
    // The line below is a .NET attribute to create a URI
    [HttpPost("process")]
    public IActionResult ProcessPayment([FromBody] PaymentRequest payment)
    {
        // Payment Processing logic
        return Ok("Payment processed successfully");
    }
}

// Controllers should only handle HTTP requests, calling services for the business logic


public class PaymentRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}