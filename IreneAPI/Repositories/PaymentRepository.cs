// Data Access Layer (Repository): Handles interaction with the database or other persistence mechanisms. This layer should be responsible for reading from and writing to the database.

using IreneAPI.DTOs;
using IreneAPI.Repositories;
using IreneAPI.Services;
using IreneAPI.Models;
using IreneAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


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

    //Keeping the async pattern consistent to avoid blocking threads unnecessarily, especially with database access
    public async Task<Payment> GetPaymentByIdAsync(int id)
    {    
        return await _context.Payments.FindAsync(id); // return null if not found
        // It's good practice to handle exceptions in the Service Layer rather than the Repository. The repository should focus on database operations and not on throwing domain-specific exceptions.
    }

    public async Task CreatePaymentAsync(Payment userPayment)
    {
        _context.Payments.Add(userPayment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePaymentAsync(int id, Payment editPayment)
    {
        
        await _context.SaveChangesAsync();
    }

    public async Task DeletePaymentAsync(int id)
    {
        var payment = _context.Payments.FindAsync(id);

        _context.Remove(payment);
        await _context.SaveChangesAsync();
    }

    // Implementing the ProcessPaymentAsync() Later on
    // public async Task<IActionResult> ProcessPaymentAsync(Payment payment)
    // {
    //     return 
    // }

    /* Data Access Layer is the same as Repository Layer in a 3-tier architecture
       Repository Layer: Implement the Repository pattern to handle database access, isolating data logic and making it reusable.
        IPaymentRepository and PaymentRepository to handle all database operations; It is here we made use of the database context that helps in managing our database access
    */
}