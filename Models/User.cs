using System;

namespace NRGUserPortal.Models
{
    // simple user model - fresher style, a bit casual comments
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }    // may be null until set
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        // NOTE: this stores the hashed password (we name it Password to match DB)
        public string? Password { get; set; }
    }
}
