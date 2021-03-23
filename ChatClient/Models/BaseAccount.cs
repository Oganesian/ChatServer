using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Models
{
    [Serializable]
    public class BaseAccount : DomainObject
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int PublicId { get; set; }
        public string Username { get; set; }
    }
}
