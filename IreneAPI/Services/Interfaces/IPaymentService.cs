using IreneAPI.Services;
using IreneAPI.Models;
using System.Collections.Generic;

namespace IreneAPI.Services;

public interface IPaymentService
{
    Task<List<Payment>> GetPayments();
    Payment GetPayment(int id);
    Task PostPayment(Payment userPayment);
    Task PutPayment(int id, Payment editPayment);
    Task DeletePayment(int id);
}