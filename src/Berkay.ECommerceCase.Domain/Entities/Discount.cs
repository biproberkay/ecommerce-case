namespace Berkay.ECommerceCase.Domain.Entities
{
    public class DiscountByCart
    {
        public int Id { get; set; }
        public decimal MinTotalPrice { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class DiscountByCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int MinQuantity { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }


}
