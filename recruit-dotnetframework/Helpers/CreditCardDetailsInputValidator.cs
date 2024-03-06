using System;
using System.Collections.Generic;

public static class CreditCardDetailsInputValidator
{
    public static List<string> ValidateCreditCardDetails(CreditCardDetails creditCardDetails)
    {
        var errorMessages = new List<string>();
        //Input validation Code Implementation
        //Credit card field is any number
        //CVC is any number
        //Expiry is any valid date
        //Handle appropriately when submitted fields are invalid

        if (creditCardDetails.CreditCardNumber <= 0)
        {
            errorMessages.Add("Credit card number is not valid");
        }

        if (creditCardDetails.Cvc <= 0)
        {
            errorMessages.Add("CVC is not valid");
        }

        if (creditCardDetails.ExpiryDate == DateTime.MinValue)
        {
            errorMessages.Add("Expiry date is not valid");
        }

        return errorMessages;
    }
}