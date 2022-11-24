﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieDG.Data.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            this.UsersMovies = new HashSet<UserMovie>();
        }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        public ICollection<UserMovie> UsersMovies { get; set; }
    }
}
