using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesMarket.Domain.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey(nameof(RoleEntity))]
        public int RoleId { get; set; }
        
        public RoleEntity Role { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Surname{ get; set; }
        [Required]
        [MaxLength(100)]
        public string Patronymic { get; set; }

        [Required]
        [MaxLength(200)]
        public string Login { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

    }
}
