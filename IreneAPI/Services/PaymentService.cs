using IreneAPI.Services;
using IreneAPI.Repositories;
using IreneAPI.Models;
using System.Threading.Tasks;

namespace IreneAPI.Services;
public class PaymentService : IPaymentService
{
    public readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public Task<List<Payment>> GetAllPaymentsAsync()
    {
        return _paymentRepository.GetAllPaymentsAsync();
    }

    public Payment GetPaymentByIdAsync(int id)
    {
        return _paymentRepository.GetPaymentByIdAsync(id);
    }

    public async Task AddPaymentAsync(Payment userPayment)
    {
        await _paymentRepository.AddPaymentAsync(userPayment);
    }

    public async Task UpdatePaymentAsync(int id, Payment editPayment)
    {
        await _paymentRepository.UpdatePaymentAsync(id, editPayment);
    }

    public async Task DeletePaymentAsync(int id)
    {
        await _paymentRepository.DeletePaymentAsync(id);
    }
}



// Service Layer: Move business logic into services that perform operations on data. Services are responsible for communicating with the repository layer.
// IPaymentService and PaymentService handle all payment-related business logic for IreneAPI

