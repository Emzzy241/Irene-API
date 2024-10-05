using System;
using IreneAPI.Repositories;
using IreneAPI.Services;
using IreneAPI.Models;


namespace IreneAPI.Repositories;

public interface IPaymentRepository
{
    Task<List<Payment>> GetAllPayments();
    Payment GetPayment(int id);
    Task PostPayment(Payment userPayment);
    Task PutPayment(int id,Payment editPayment);
    Task DeletePayment(int id);
}