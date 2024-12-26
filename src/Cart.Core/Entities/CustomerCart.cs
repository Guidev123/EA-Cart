using Cart.Core.DomainObjects;
using Cart.Core.ValueObjects;

namespace Cart.Core.Entities
{
    public class CustomerCart : Entity, IAggregateRoot
    {
        public CustomerCart(Guid customerId)
        {
            CustomerId = customerId;
            TotalPrice = 0;
            VoucherIsUsed = false;
            Discount = 0;
        }
        protected CustomerCart() { }

        public Guid CustomerId { get; private set; }
        public decimal TotalPrice { get; private set; }
        public List<CartItem> Itens { get; private set; } = [];
        public bool VoucherIsUsed { get; private set; }
        public decimal Discount { get; private set; }
        public Voucher? Voucher { get; private set; }
        internal void CalculateTotalPrice()
        {
            TotalPrice = Itens.Sum(x => x.CalculateValue());
            CalculateTotalPriceDiscount();
        }
        internal CartItem GetProductById(Guid productId) => Itens.First(x => x.ProductId == productId);
        internal bool ItemCartAlreadyExists(CartItem item) => Itens.Any(x => x.ProductId == item.ProductId);
        public void AddItem(CartItem item)
        {
            item.AssociateCart(Id);

            if (ItemCartAlreadyExists(item))
            {
                var itemExists = GetProductById(item.ProductId);
                itemExists.AddUnity(item.Quantity);
                item = itemExists;
                Itens.Remove(itemExists);
            }

            Itens.Add(item);
            CalculateTotalPrice();
        }
        public void UpdateItem(CartItem item)
        {
            item.AssociateCart(Id);

            var existentItem = GetProductById(item.ProductId);

            Itens.Remove(existentItem);
            Itens.Add(item);

            CalculateTotalPrice();
        }

        public void RemoveItem(CartItem item)
        {
            Itens.Remove(GetProductById(item.ProductId));
            CalculateTotalPrice();
        }

        public void UpdateUnities(CartItem item, int unities)
        {
            item.UpdateUnities(unities);
            UpdateItem(item);
        }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherIsUsed = true;
            CalculateTotalPrice();
        }

        public void CalculateTotalPriceDiscount()
        {
            if (!VoucherIsUsed) return;

            decimal discount = 0;
            var value = TotalPrice;

            if (Voucher!.DiscountType == Enums.EDiscountType.Percentual)
            {
                if (Voucher.Percentual.HasValue)
                {
                    discount = (value * Voucher.Percentual.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher!.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }

            TotalPrice = value < 0 ? 0 : value;
            Discount = discount;
        }
    }
}
