namespace CompanyAPI.Dtos {
    public class EmployeeDto {
        public Guid id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string photoUrl { get; set; }
        public string document { get; set; }
        public DateTime birthDay { get; set; }
        public string role { get; set; }
        public string gender { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime bloquedAt { get; set; }
    }

    public class EmployeeViewModel {
        public string fullName { get; set; }
        public string email { get; set; }
        public string photoUrl { get; set; }
        public string document { get; set; }
        public DateTime birthDay { get; set; }
        public string role { get; set; }
        public string gender { get; set; }
    }
}
