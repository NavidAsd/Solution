using System;

namespace Domain.Entities
{
    public class Users : BaseEntities.Basic
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
    }
}
