﻿namespace Cinema.Payloads.Requests.Request_Cinema
{
    public class Request_UpdateCinema
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string NameOfCinema { get; set; }
    }
}
