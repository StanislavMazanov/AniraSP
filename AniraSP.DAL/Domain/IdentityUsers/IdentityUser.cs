using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniraSP.DAL.Domain.IdentityRoles;

namespace AniraSP.DAL.Domain.IdentityUsers {
    public class IdentityUser {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Login { get; set; }

        [Required] public bool isEnadle { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Guid? RoleId { get; set; }
        public IdentityRole Role { get; set; }
    }
}