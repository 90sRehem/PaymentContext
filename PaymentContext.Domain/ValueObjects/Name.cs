using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;


            AddNotifications(new Contract()
            .Requires()
            .HasMinLen(
                val: FirstName,
                min: 3,
                property: "Name.FirstName",
                message: "Nome de conter pelo menos de três caracteres.")
            .HasMinLen(
                val: LastName,
                min: 3,
                property: "Name.LastName",
                message: "Sobrenome de conter pelo menos de três caracteres.")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}