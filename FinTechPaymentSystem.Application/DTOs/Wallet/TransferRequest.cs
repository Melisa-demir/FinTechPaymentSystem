using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechPaymentSystem.Application.DTOs.Wallet
{
    public class TransferRequest
    {
        public int ReceiverUserId { get; set; }
        public decimal Amount { get; set; }
    }
}
