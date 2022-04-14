using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreateBilletSubscriptionCommandTest
    {
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBilletSubscriptionCommand();
            command.Validate();
            Assert.AreEqual(false, command.Valid);
        }
    }
}