using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaikuRatingSystem.Models
{
    public class User
    {
        public int UserId { get; set; }

        public User()
        {
            this.Haikus = new HashSet<Haiku>();
            this.DateCreated = DateTime.Now;
            this.IsVip = false;
        }

        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string UserName { get; set; }


        [Required]
        public string PasswordHash { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsVip { get; set; }

        public virtual ICollection<Haiku> Haikus { get; set; }
    }
}
