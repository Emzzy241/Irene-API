// Business Logic Layer (Service): Contains the business rules, computations, and transformations. It coordinates the logic of how things should work and communicates with the repository layer.


using IreneAPI.Services;
using IreneAPI.Repositories;
using IreneAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<Payment> GetPaymentByIdAsync(int id)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);

        if(payment == null)
        {
            throw new Exception("Payment Details not found");
        }

        return payment;
    }

    public async Task CreatePaymentAsync(Payment userPayment)
    {
        await _paymentRepository.CreatePaymentAsync(userPayment);
    }

    public async Task UpdatePaymentAsync(int id, Payment editPayment)
    {
        // Adding the logic for updating a payment
        var existingPayment = await _paymentRepository.GetPaymentByIdAsync(id);
        if(existingPayment == null)
        {
            throw new Exception("Payment details not found");
        }
        existingPayment.FirstName = editPayment.FirstName;
        existingPayment.LastName = editPayment.LastName;
        existingPayment.Amount = editPayment.Amount;

        await _paymentRepository.UpdatePaymentAsync(id, editPayment);
    }

    public async Task DeletePaymentAsync(int id)
    {
        // Adding the logic for deleting a payment
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        if(payment == null)
        {
            throw new Exception("Payment Details you want to delete cannot be found in the database");
        }

        // After adding the logic on how a Payment is deleted, then we call on the Repository top actually delete such Payment
        await _paymentRepository.DeletePaymentAsync(id);
    }
}



// Service Layer: Move business logic into services that perform operations on data. Services are responsible for communicating with the repository layer.
// IPaymentService and PaymentService handle all payment-related business logic for IreneAPI

