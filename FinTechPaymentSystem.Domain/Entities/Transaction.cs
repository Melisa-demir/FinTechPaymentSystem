using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechPaymentSystem.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int WalletId { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; } = null!;

        public int? RelatedWalletId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Wallet Wallet { get; set; } = null!;
    }
}

