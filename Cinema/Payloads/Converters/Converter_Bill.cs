using Cinema.Entities;
using Cinema.Payloads.DTO;

namespace Cinema.Payloads.Converters
{
    public class Converter_Bill
    {
        public DTO_Bill EntityToDTO(Bill bill)
            => new DTO_Bill
            {
                Id = bill.Id,
                TotalMoney = bill.TotalMoney,
                TradingCode = bill.TradingCode,
                CreateTime = bill.CreateTime,
                UserId = bill.UserId,
                Name = bill.Name,
                UpdateTime = bill.UpdateTime,
                PromotionID = bill.PromotionID,
                BillStatusId = bill.BillStatusId,
                IsActive = bill.IsActive
            };
    }
}
