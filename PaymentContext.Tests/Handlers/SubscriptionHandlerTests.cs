using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Fakes;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(
                repository: new FakeStudantRepository(),
                emailService: new FakeEmailService());
            var command = new CreateBilletSubscriptionCommand();
            command.FirstName = "Jonathan";
            command.LastName = "Rehem";
            command.Document = "99999999999";
            command.Email = "hello@email.com";
            command.BarCode = "123456789";
            command.BilletNumber = "";
            command.PaymentNumber = "";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Jonathan Rehem";
            command.PayerDocument = "12345678901";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "payer@email.com";
            command.Street = "Rua teste";
            command.Number = "teste";
            command.Neighborhood = "teste";
            command.City = "teste";
            command.State = "teste";
            command.Country = "teste";
            command.ZipCode = "teste";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}