using System;
using System.Linq;
using TesteStripe.Services;

namespace TesteStripe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite um valor:");
            var amountResponse = Console.ReadLine();

            var amount = 0.0;
            double.TryParse(amountResponse, out amount);

            Console.WriteLine("Digite uma descrição:");
            var description = Console.ReadLine();

            Console.WriteLine("Digite o número do cartão:");
            var cardNumber = Console.ReadLine();

            Console.WriteLine("Digite um CVC:");
            var cvc = Console.ReadLine();

            Console.WriteLine("Digite o mês de expiração do cartão:");
            var month = Console.ReadLine();

            Console.WriteLine("Digite o ano de expiração do cartão:");
            var year = Console.ReadLine();

            var stripeService = new StripeService();

            var resultPaymentCard = stripeService.PayWithCard(cardNumber, cvc, month, year, amount, description);

            if (resultPaymentCard.First().Key)
                Console.WriteLine("Pagamento efetuado com sucesso. Id gerado: " + resultPaymentCard.First().Value);
            else
                Console.WriteLine("Erro ao efetuar o pagamento. Erro: " + resultPaymentCard.First().Value);

            Console.ReadKey();
        }
    }
}
