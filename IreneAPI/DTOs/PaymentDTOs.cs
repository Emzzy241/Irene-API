namespace IreneAPI.DTOs;
public class PaymentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Amount { get; set; }
    public string AmountInWords { get; set; }    

    // Previously, I was passing the entire Payment object between the layers. It's best practice to use DTOs for separating the data that is passed between layers from the entity model used in the database.
    // This way, you decouple the internal workings of your database (entities) from what the API returns or accepts (DTOs). It gives you flexibility if the database structure changes but you want to keep the same API contract.
}