namespace Cinema.Payloads.DTO
{
    public class DTO_Bill
    {
        public int Id {  get; set; }
        public double TotalMoney { get; set; }
        public string TradingCode { get; set; }
        public DateTime? CreateTime { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int PromotionID { get; set; }
        public int? BillStatusId { get; set; }
        public bool? IsActive { get; set; }
    }
}
