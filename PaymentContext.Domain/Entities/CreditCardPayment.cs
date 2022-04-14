using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(
             string cardHolder,
             string cardNumber,
             string lastTransactionNumber,
             DateTime paidDate,
             DateTime expireDate,
             decimal total,
             decimal totalPaid,
             Address address,
             Document document,
             string payer,
             Email email) : base(
                paidDate,
                expireDate,
                total,
                totalPaid,
                address,
                document,
                payer,
                email
            )
        {
            CardHolder = cardHolder;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolder { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}