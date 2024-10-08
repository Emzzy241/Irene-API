using IreneAPI.DTOs;
using IreneAPI.Services;
using IreneAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace IreneAPI.Services;

public interface IPaymentService
{
    Task<List<Payment>> GetAllPaymentsAsync();
    Task<Payment> GetPaymentByIdAsync(int id);
    Task CreatePaymentAsync(PaymentDto userPaymentDto);
    Task UpdatePaymentAsync(int id, PaymentDto editPaymentDto);
    Task DeletePaymentAsync(int id);
}