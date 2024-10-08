namespace IreneAPI.Errors;
public class PaymentNotFoundException : Exception
{
    // instead of throwing general exceptions like throw new Exception, we are creating custom exceptions to represent domain-specific errors
    // This makes our error process more precise and meaningful

    // Making our constructor also inherit the error messages from our base class(Exception)
    public PaymentNotFoundException(string message): base(message)
    {

    }
}