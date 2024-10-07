using System;
using IreneAPI.Repositories;
using IreneAPI.Services;
using IreneAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace IreneAPI.Repositories;

public interface IPaymentRepository
{
    Task<List<Payment>> GetAllPaymentsAsync();
    Task<Payment> GetPaymentByIdAsync(int id);
    Task CreatePaymentAsync(Payment userPayment);
    Task UpdatePaymentAsync(int id,Payment editPayment);
    Task DeletePaymentAsync(int id);
}