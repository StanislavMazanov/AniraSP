using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniraSP.DAL.Domain.IdentityUsers;

namespace AniraSP.DAL.Domain.IdentityRoles {
    public class IdentityRole {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required] public bool isEnadle { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public List<IdentityUser> Users { get; set; }

        public IdentityRole() {
            Users = new List<IdentityUser>();
        }
    }
}