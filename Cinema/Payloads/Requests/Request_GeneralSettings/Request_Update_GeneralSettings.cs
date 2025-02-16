﻿namespace Cinema.Payloads.Requests.Request_GeneralSettings
{
    public class Request_Update_GeneralSettings
    {
        public int Id {  get; set; }
        public DateTime BreakTime { get; set; }
        public int BusinessHours { get; set; }
        public DateTime CloseTime { get; set; }
        public double FixedTiketPrice { get; set; }
        public int PercentDay { get; set; }
        public int PercenWeekend { get; set; }
        public DateTime TimeBeginToChage { get; set; }
    }
}
