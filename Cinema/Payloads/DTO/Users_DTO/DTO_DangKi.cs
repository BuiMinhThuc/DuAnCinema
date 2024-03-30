namespace Cinema.Payloads.DTO.Users_DTO
{
    public class DTO_DangKi
    {
        public int Id { get; set; }
        public int? Point { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string RankCustomerName { get; set; }
        public string UserStatusName { get; set; }
        public bool? IsActive { get; set; }
        public string RoleName { get; set; }
    }
}
