using System;
using System.ComponentModel.DataAnnotations;

namespace IreneAPI.Models;
public class Payment
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(20)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(20)]
    public string LastName { get; set; }
    
    [Required]
    public double Amount { get; set; }
    
        
}