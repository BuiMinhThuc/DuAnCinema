﻿namespace Cinema.Entities
{
    public class BillStatus : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Bill> Bills { get; set; }
    }
}
