using _0_Framework.Domain;
using System;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount:EntityBase
    {
        public long ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }

        public CustomerDiscount(long productId, int discountRate,
            DateTime startDate, DateTime endDate, string description)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            IsActive = true;
        }

        public void Edit(long productId, int discountRate,
            DateTime startDate, DateTime endDate, string description)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }

        public void Active()
        {
            IsActive = true;
        }

        public void InActive()
        {
            IsActive = false;
        }
    }
}
