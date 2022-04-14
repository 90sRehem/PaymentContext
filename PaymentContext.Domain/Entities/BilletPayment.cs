using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class BilletPayment : Payment
    {
        public BilletPayment(
         string barCode,
         string billetNumber,
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
           email)
        {
            BarCode = barCode;
            BilletNumber = billetNumber;
        }

        public string BarCode { get; private set; }
        public string BilletNumber { get; private set; }
    }
}