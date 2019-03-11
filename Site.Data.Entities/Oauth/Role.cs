using System;

namespace Site.Data.Entities.Oauth
{
    public class Role : Entity<Guid>
    {
        public string Name { get; set; }
    }
}