using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesMarket.Domain.Entities
{
    public class PickupPointEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Adress { get; set; }

        public List<OrderEntity> Orders { get; set; }

    }
}
