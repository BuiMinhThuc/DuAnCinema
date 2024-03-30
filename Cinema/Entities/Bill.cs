namespace Cinema.Entities
{
    public class Bill : BaseEntity
    {
        public double TotalMoney { get; set; }
        public string TradingCode { get; set; }
        public DateTime? CreateTime { get; set; }= DateTime.Now;
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime? UpdateTime { get; set; }=DateTime.Now;
        public int PromotionID { get; set; }
        public int? BillStatusId { get; set; } = 1;
        public bool? IsActive { get; set; } = true;
        public IEnumerable<BillFood>? BillFoods { get; set; }
        public IEnumerable<BillTicket>? BillTickets { get; set; }
         
    }
}
