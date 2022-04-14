using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Subscription _subscription;

    public StudentTests()
    {
        _name = new Name("Jonathan", "Rehem");
        _document = new Document("12385567724", EDocumentType.CPF);
        _email = new Email(address: "jonathan.de.oliveira@live.com");
        _address = new Address(
                        street: "Rua leila guimaraes",
                        number: "20",
                        neighborhood: "sepetiba",
                        city: "rio de janeiro",
                        state: "RJ",
                        country: "Brazil",
                        zipCode: "23530131"
                    );
        _student = new Student(_name, _document, _email);

        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment(
                    paidDate: DateTime.Now,
                    expireDate: DateTime.Now.AddDays(5),
                    total: 10,
                    totalPaid: 10,
                    address: _address,
                    document: _document,
                    payer: "jonathan",
                    email: _email,
                    transactionCode: "123");

        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.Invalid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    {
        var subscription = new Subscription(null);
        _student.AddSubscription(subscription);
        Assert.IsTrue(_student.Invalid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenAddSubscription()
    {
        var subscription = new Subscription(null);
        var payment = new PayPalPayment(
                     paidDate: DateTime.Now,
                     expireDate: DateTime.Now.AddDays(5),
                     total: 10,
                     totalPaid: 10,
                     address: _address,
                     document: _document,
                     payer: "jonathan",
                     email: _email,
                     transactionCode: "123");

        _subscription.AddPayment(payment);
        _student.AddSubscription(subscription);
        Assert.IsTrue(_student.Valid);
    }
}