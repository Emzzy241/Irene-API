using IreneAPI.Services;
using IreneAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace IreneAPI.Services;

public interface IPaymentService
{
    Task<List<Payment>> GetAllPaymentsAsync();
    Task<Payment> GetPaymentByIdAsync(int id);
    Task CreatePaymentAsync(Payment userPayment);
    Task UpdatePaymentAsync(int id, Payment editPayment);
    Task DeletePaymentAsync(int id);
}