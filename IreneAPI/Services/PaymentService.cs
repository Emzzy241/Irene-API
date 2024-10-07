// Business Logic Layer (Service): Contains the business rules, computations, and transformations. It coordinates the logic of how things should work and communicates with the repository layer.


using IreneAPI.DTOs;
using IreneAPI.Errors;
using IreneAPI.Services;
using IreneAPI.Repositories;
using IreneAPI.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace IreneAPI.Services;
public class PaymentService : IPaymentService
{
    public readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<List<Payment>> GetAllPaymentsAsync()
    {
        return await _paymentRepository.GetAllPaymentsAsync();
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
            AmountInWords = ConvertAmountToWordsAsync((int)userPaymentDto.Amount)
        };

        await _paymentRepository.CreatePaymentAsync(payment);
    }

    // This way, you decouple the internal workings of your database (entities) from what the API returns or accepts (DTOs). Which gives you flexibility if the database structure changes but you want to keep the same API contract.
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
        existingPayment.AmountInWords = ConvertAmountToWordsAsync((int)editPaymentDto.Amount); // first, we explicitly convert the amount(in double) into an int because our ConvertAmountToWord() method takes in a parameter int
        await _paymentRepository.UpdatePaymentAsync(id, existingPayment);
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



    public string ConvertAmountToWordsAsync(int number)
    {
    if (number == 0)
        return "zero";
    if (number < 0)
        return "minus " + ConvertAmountToWordsAsync(Math.Abs(number)); // our method only works with int, hence the moment we take in a decimal number(e.g: 980262.28), we turn it into its absolute value which is: 980262
        Console.WriteLine("------------------------------------------------- " + ConvertAmountToWordsAsync(Math.Abs(number)));
    string words = "";
    if ((number / 1000000) > 0)
    {
    words += ConvertAmountToWordsAsync(number / 1000000) + " million ";
    number %= 1000000;
    }
    if ((number / 1000) > 0)
    {
    words += ConvertAmountToWordsAsync(number / 1000) + " thousand ";
    number %= 1000;
    }
    if ((number / 100) > 0)
    {
    words += ConvertAmountToWordsAsync(number / 100) + " hundred ";
    number %= 100;
    }
    if (number > 0)
    {
    if (words != "")
    words += "and ";
    var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
    var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
    if (number < 20)
    words += unitsMap[number];
    else
    {
    words += tensMap[number / 10];
    if ((number % 10) > 0)
    words += "-" + unitsMap[number % 10];
    }
    }
    return words;
    }
/**/
/*
    // Implementing the ConvertAmountToWordsAsync()
    public string ConvertAmountToWordsAsync(double amount)
    {
        if(amount == 0)
        {
            return "zero";
        }
        // else, create dictionaries for unit members(1-9), teens(10-19), tens(20,30, 40, 50, ...90), thousands(thousands, millions, billions, trillions)
        // Dictionary for unit members
        Dictionary<double, string> units = new Dictionary<double, string>()
        {
            {1, "one"},
            {2, "two"},
            {3, "three"},
            {4, "four"},
            {5, "five"},
            {6, "six"},
            {7, "seven"},
            {8, "eight"},
            {9, "nine"}
        };
        
        // Dictionary for teen members
        Dictionary<double, string> teenMembers = new Dictionary<double, string>()
        {
            {10, "ten"},
            {11, "eleven"},
            {12, "twelve"},
            {13, "thirteen"},
            {14, "fourteen"},
            {15, "fifteen"},
            {16, "sixteen"},
            {17, "sixteen"},
            {18, "eighteen"},
            {19, "nineteen"}
        };

        // Dictionary for tens
        Dictionary<double, string> tens = new Dictionary<double, string>()
        {
            {20, "twenty"},
            {30, "thirty"},
            {40, "forty"},
            {50, "fifty"},
            {60, "sixty"},
            {70, "seventy"},
            {80, "eighty"},
            {90, "ninety"}
        };
        
        // Dictionary for thousands
        Dictionary<double, string> thousands = new Dictionary<double, string>()
        {
            {0, ""},
            {1, "thousand"},
            {2, "million"},
            {3, "billion"},
            {4, "trillion"}
        };

        string result = "";

        // chunkCount is used to keep track of the current chunk's position (thousands, millions, billions, etc.).
        int chunkCount = 0;

        while (amount > 0)
        {
            double chunk = (amount % 1000);
            if(chunk > 0)
            {
                result = ConvertChunkToWords(chunk, units, teenMembers, tens) + " " + thousands[chunkCount] + " " + result;
            }
            amount /= 1000;
            chunkCount++;
        }
        return result.Trim();
        // The loop continues until the entire amount has been processed, and each digit chunk has been converted into words and added into the result string
        // The final result represents the entire amount in words
        
    }

    
    public string ConvertChunkToWords(double myChunk, Dictionary<double, string> myUnits, Dictionary<double, string> myTeenMembers, Dictionary<double, string> myTens)
    {
        string result = "";

        double hundreds = myChunk / 100;// This line calculates the hundreds digit in the chunk by performing integer division. For example, if chunk is 324, hundreds will be 3. We already divided by 1000 in our while loop before we called on this method, now we're dividing by 100
        double remainder = myChunk % 100; // This line calculates the remainder after removing the hundreds digit. Example, if chunk is 324, remainder will be 24.

        if(hundreds > 0)
        {
            // This condition checks if there are hundreds in the chunk. If there are, it appends the word representation of the hundreds to the result string. For example, if hundreds is 3, it appends "three hundred".... 
            // To achieve this, We used the key-value pairs defined in our units dictionary. We called on the units dictionary, passed in a key(e.g 3) and that key returns a string value: three 

            result += myUnits[hundreds] + " hundred ";
            if(remainder > 0)
            {
                result += " and ";
            }
        }

        if(remainder > 0)
        {
            // condition checks if there is a remainder in the chunk(i.e values less than 100)
            if (remainder < 10)
            {
                // If remainder is less than 10, its a unit, hence we use the units dictionary. e.g: myUnits[2] will return "two"
                result += myTeenMembers[remainder];
            }
            else if(remainder < 20)
            {
                result += myTeenMembers[remainder];
            }
            else
            {
                // If remainder is >= 20, it appends both the word representation of myTens and myUnits
                double tensDigit = remainder / 10;
                double unitsDigit = remainder % 10;

                result += myTens[tensDigit];

                if(unitsDigit > 0)
                {
                    result += " " + myUnits[unitsDigit];
                }
            }
        }

        return result;
        // the result string is gradually built up based on the hundreds, tens, and units in the chunk, and it is returned as the word representation of that particular three-digit chunk.
        // This method is called multiple times when processing the entire number, and each chunk contributes to the final representation of the number in words.

    }

    

*/
}



// Service Layer: Move business logic into services that perform operations on data. Services are responsible for communicating with the repository layer.
// IPaymentService and PaymentService handle all payment-related business logic for IreneAPI

