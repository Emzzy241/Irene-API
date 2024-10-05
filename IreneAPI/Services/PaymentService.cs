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

    public Task<List<Payment>> GetPayments()
    {
        return _paymentRepository.GetAllPayments();
    }

    public Payment GetPayment(int id)
    {
        return _paymentRepository.GetPayment(id);
    }

    public async Task PostPayment(Payment userPayment)
    {
        await _paymentRepository.PostPayment(userPayment);
    }

    public async Task PutPayment(int id, Payment editPayment)
    {
        await _paymentRepository.PutPayment(id, editPayment);
    }

    public async Task DeletePayment(int id)
    {
        await _paymentRepository.DeletePayment(id);
    }
}



// Service Layer: Move business logic into services that perform operations on data. Services are responsible for communicating with the repository layer.
// IPaymentService and PaymentService handle all payment-related business logic for IreneAPI

