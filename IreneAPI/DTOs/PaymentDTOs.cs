namespace IreneAPI.DTOs;
public class PaymentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Amount { get; set; }
    public string AmountInWords { get; set; }    

    // A great use-case of DTO is when trying to turn Amount into words
    // I do not need this value from my user at all, I can do it with my algorithm,
    // If I tried achieving that with just Payment object which returns Id, AmountInWords, etc
    // I do not want users to have to set this values themselves in the JSON format, DTO really helped out here
    // With the AmountInWords, I also don't want my users togive me this value when they make a Create() or Update(), other code handles that
    

    // Previously, I was passing the entire Payment object between the layers. It's best practice to use DTOs for separating the data that is passed between layers from the entity model used in the database.
    // This way, you decouple the internal workings of your database (entities) from what the API returns or accepts (DTOs). It gives you flexibility if the database structure changes but you want to keep the same API contract.
}