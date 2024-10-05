using IreneAPI.Repositories;
using IreneAPI.Services;
using IreneAPI.Models;
using IreneAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;


namespace IreneAPI.Repositories;

public class PaymentRepository : IPaymentRepository
{
    public readonly IreneAPIContext _context;

    public PaymentRepository(IreneAPIContext context)
    {
        _context = context;
    }

    public async Task<List<Payment>> GetAllPaymentsAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public Payment GetPaymentByIdAsync(int id)
    {
        // var payment = await _context.Payments.FindAsync(id);

        // if(payment == null)
        // {
        //     return throw new Exception("Payment Details you want to get cannnot be found in the Database");
        // }
        // return await _context.Payments.FindAsync(id);
    
        return _context.Payments.Find(id);
    }

    public async Task AddPaymentAsync(Payment userPayment)
    {
        _context.Payments.Add(userPayment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePaymentAsync(int id, Payment editPayment)
    {
        var existingPayment = await _context.Payments.FindAsync(id);
        if(existingPayment == null)
        {
            throw new Exception("Payment Details you want to edit cannot be found in the database");
        }
        existingPayment.FirstName = editPayment.FirstName;
        existingPayment.LastName = editPayment.LastName;
        existingPayment.Amount = editPayment.Amount;

        await _context.SaveChangesAsync();
    }

    public async Task DeletePaymentAsync(int id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if(payment == null)
        {
            throw new Exception("Payment Details you want to delete cannot be found in the database");
        }

        _context.Remove(payment);
        await _context.SaveChangesAsync();
    }

    /* Data Access Layer is the same as Repository Layer in a 3-tier architecture
       Repository Layer: Implement the Repository pattern to handle database access, isolating data logic and making it reusable.
        IPaymentRepository and PaymentRepository to handle all database operations; It is here we made use of the database context that helps in managing our database access
    */
}