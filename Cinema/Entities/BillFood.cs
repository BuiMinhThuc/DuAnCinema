﻿namespace Cinema.Entities
{
    public class BillFood : BaseEntity
    {
        public int Quantity { get; set; }
        public int BillId { get; set; }
        public int FoodId { get; set; }
        public Bill? Bill { get; set; }
        public Food? Food { get; set; }
    }
}
