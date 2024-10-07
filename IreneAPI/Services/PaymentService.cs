// Business Logic Layer (Service): Contains the business rules, computations, and transformations. It coordinates the logic of how things should work and communicates with the repository layer.


using IreneAPI.DTOs;
using IreneAPI.Errors;
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
            throw new PaymentNotFoundException("Payment Details not found.");
        }

        return payment;
    }

     public async Task CreatePaymentAsync(PaymentDto userPaymentDto)
    {
        var payment = new Payment
        {
            FirstName = userPaymentDto.FirstName,
            LastName = userPaymentDto.LastName,
            Amount = userPaymentDto.Amount,
            // AmountInWords = NumberToWords((int)userPaymentDto.Amount)
        };

        await _paymentRepository.CreatePaymentAsync(payment);
    }

    public async Task UpdatePaymentAsync(int id, PaymentDto editPaymentDto)
    {
        // Adding the logic for updating a payment
        var existingPayment = await _paymentRepository.GetPaymentByIdAsync(id);
        if(existingPayment == null)
        {
            throw new PaymentNotFoundException("Payment details not found.");
        }
        existingPayment.FirstName = editPaymentDto.FirstName;
        existingPayment.LastName = editPaymentDto.LastName;
        existingPayment.Amount = editPaymentDto.Amount;
        Payment editPayment = (Payment)editPaymentDto;
        await _paymentRepository.UpdatePaymentAsync(id, editPayment);
    }

    public async Task DeletePaymentAsync(int id)
    {
        // Adding the logic for deleting a payment
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        if(payment == null)
        {
            throw new PaymentNotFoundException("Payment details not found.");
        }

        // After adding the logic on how a Payment is deleted, then we call on the Repository top actually delete such Payment
        await _paymentRepository.DeletePaymentAsync(id);
    }
}



// Service Layer: Move business logic into services that perform operations on data. Services are responsible for communicating with the repository layer.
// IPaymentService and PaymentService handle all payment-related business logic for IreneAPI

