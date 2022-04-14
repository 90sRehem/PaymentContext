using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(
            new Contract()
            .Requires()
            .IsEmail(
                email: Address,
                property: "Email.Address", "E-mail inválido"
                )
            );
        }
        public string Address { get; private set; }
    }
}