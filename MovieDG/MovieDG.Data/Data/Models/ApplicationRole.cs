namespace MovieDG.Data.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
            : this(null)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public bool isActive { get; set; } = true;
    }
}
