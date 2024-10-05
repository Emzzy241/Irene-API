// This would be similar to the ApplicationUser class which inherit from IdentityUser
// This class allows us to extend user properties if needed i.e we can add additional properties for our User class

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace IreneAPI.Models;
public class ApplicationUser : IdentityUser
{
    // Adding additional User properties
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    // You’re extending the user model(IdentityUser) to include more fields. This makes it flexible—you can add any number of custom fields based on your project’s requirements (e.g., profile pictures, date of birth, etc.).
}


