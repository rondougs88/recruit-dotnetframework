﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace recruit_dotnetframework.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public IHttpActionResult Post([FromBody] CreditCardDetails creditCardDetails)
        {
            var errorMessages = CreditCardDetailsInputValidator.ValidateCreditCardDetails(creditCardDetails);
            if (errorMessages.Count > 0)
            {
                return BadRequest(string.Join(". ", errorMessages));
            }

            var visaResponse = Task.Run(() => CreditCardDetailsInputValidator.ValidateCreditCardUsingExternalApi(creditCardDetails)).Result;
            if (visaResponse == null)
            {
                return BadRequest("Visa credit card validation failed. Please check your submitted card details and try again.");
            }

            //Save successful request into database
            //SaveCreditCardDetailsInDb(creditCardDetails);

            return Ok("Successfull processing!!!");
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
