using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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

    public async static Task<VisaResponse> ValidateCreditCardUsingExternalApi(CreditCardDetails creditCardDetails)
    {
        var jsonData = JsonConvert.SerializeObject(creditCardDetails);

        using (var httpClient = new HttpClient())
        {
            var token = Guid.NewGuid().ToString();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync("https://www.visacreditcardvalidator.com/api/v1.2", new StringContent(jsonData, Encoding.UTF8, "application/json"));

            VisaResponse visaResponse;
            using (var content = response.Content)
            {
                var result = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<VisaResponse>(result);
            }
        }
    }
}