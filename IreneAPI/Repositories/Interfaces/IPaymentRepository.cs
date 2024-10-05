using System;
using IreneAPI.Repositories;
using IreneAPI.Services;
using IreneAPI.Models;


namespace IreneAPI.Repositories;

public interface IPaymentRepository
{
    Task<List<Payment>> GetAllPaymentsAsync();
    Payment GetPaymentByIdAsync(int id);
    Task AddPaymentAsync(Payment userPayment);
    Task UpdatePaymentAsync(int id,Payment editPayment);
    Task DeletePaymentAsync(int id);
}