using IreneAPI.Services;
using IreneAPI.Models;
using System.Collections.Generic;

namespace IreneAPI.Services;

public interface IPaymentService
{
    Task<List<Payment>> GetAllPaymentsAsync();
    Payment GetPaymentByIdAsync(int id);
    Task AddPaymentAsync(Payment userPayment);
    Task UpdatePaymentAsync(int id, Payment editPayment);
    Task DeletePaymentAsync(int id);
}