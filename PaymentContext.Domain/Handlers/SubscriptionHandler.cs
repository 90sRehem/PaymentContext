using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBilletSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBilletSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(success: false, message: "Não foi possível realizar sua assinatura.");
            }
            // Verificar se Documento está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification(property: "Document", message: "Esse CPF já está em uso.");
            // Verificar se E-mail está cadastrado
            if (_repository.DocumentExists(command.Email))
                AddNotification(property: "Email", message: "Esse E-mail já está em uso.");
            // Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                street: command.Street,
                number: command.Number,
                neighborhood: command.Neighborhood,
                city: command.City,
                state: command.State,
                country: command.Country,
                zipCode: command.ZipCode
                        );
            // Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(expireDate: DateTime.Now.AddMonths(1));
            var payment = new BilletPayment(
                barCode: command.BarCode,
                billetNumber: command.BilletNumber,
                paidDate: command.PaidDate,
                expireDate: command.ExpireDate,
                total: command.Total,
                totalPaid: command.TotalPaid,
                address: address,
                document: new Document(command.PayerDocument, command.PayerDocumentType),
                payer: command.Payer,
                email: email);
            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);
            // Checar validações
            if(Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");
            // Agrupar validações
            AddNotifications(document, name, address, student, subscription, payment);
            // Salvar informações
            _repository.CreateSubscription(student);
            // Enviar E-mail de boas vindas
            _emailService.Send(
                to: student.Name.ToString(),
                email: student.Email.Address,
                subject: "Bem vindo ao nosso sitema.",
                body: "Sua assinatura foi criada."
            );
            // Retornar informações
            return new CommandResult(success: true, message: "Assinatura realizada com sucesso.");
        }
    }
}