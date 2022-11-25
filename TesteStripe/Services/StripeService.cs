using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TesteStripe.Models;

namespace TesteStripe.Services
{
    public class StripeService
    {
        private readonly string _apiKey = "sk_test_51L98tbGRmqRb4WGtbStU2TUlOhf4JcCZRRiZis64ZPpmyp3iOWVhErayE9x7PPGfhOATkoaw6fhL54xC4vSkmXFk002tk2oW3V";
        private readonly string _defaultUrl = "https://api.stripe.com/v1/";

        public Dictionary<bool, string> PayWithCard(string cardNumber, string cvc, string expireMonth, string expireYear, double amount, string description)
        {
            var resultAction = new Dictionary<bool, string>();

            var urlAction = $"{_defaultUrl}charges";

            var client = new RestClient(urlAction);
            client.Authenticator = new HttpBasicAuthenticator(_apiKey, "");

            var source = "";

            if (string.IsNullOrEmpty(cardNumber) == false)
            {
                var resultCardToken = this.GenerateCardToken(cardNumber, cvc, expireMonth, expireYear);

                if (resultCardToken.First().Key)
                    source = resultCardToken.First().Value;
                else
                    resultAction.Add(false, "Erro ao gerar o token do cartão. Erro: " + resultCardToken.First().Value);
            }

            if (source != "")
            {
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("amount", amount.ToString("n2").Replace(".", "").Replace(",", ""));
                request.AddParameter("currency", "brl");
                request.AddParameter("description", description);
                request.AddParameter("source", source);

                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.Created)
                {
                    var objResultResponse = JsonConvert.DeserializeObject<StripePaymentCardResult>(response.Content);

                    resultAction.Add(true, objResultResponse.id);
                }
                else
                    resultAction.Add(false, response.Content);
            }

            return resultAction;
        }

        public Dictionary<bool, string> GenerateCardToken(string cardNumber, string cvc, string expireMonth, string expireYear)
        {
            var resultToken = new Dictionary<bool, string>();

            var urlAction = $"{_defaultUrl}tokens";

            var client = new RestClient(urlAction);
            client.Authenticator = new HttpBasicAuthenticator(_apiKey, "");

            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("card[number]", cardNumber);
            request.AddParameter("card[exp_month]", expireMonth);
            request.AddParameter("card[exp_year]", expireYear);
            request.AddParameter("card[cvc]", cvc);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.Created)
            {
                var objResultResponse = JsonConvert.DeserializeObject<StripeCardResult>(response.Content);

                resultToken.Add(true, objResultResponse.id);
            }
            else
                resultToken.Add(false, response.Content);

            return resultToken;
        }
    }
}
