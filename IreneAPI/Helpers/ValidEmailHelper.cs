
namespace IreneAPI.Helpers;
public class ValidEmailHelper
{

    // A Method to check if an email entered is valid
    public static bool IsValidEmail(string input)
    {
        try
        {
            // passing in inputted email and checking if its actually a valid email, then we use the comparison operator(==) which returns true
            var email = new System.Net.Mail.MailAddress(input);
            return email.Address == input;
        }
        catch
        {
            return false;
        }
    }
    
}