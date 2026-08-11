using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechPaymentSystem.Application.DTOs.Wallet
{
    public class WalletResponse
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
    }
}
