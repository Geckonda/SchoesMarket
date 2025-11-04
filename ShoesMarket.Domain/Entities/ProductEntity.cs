using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesMarket.Domain.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Article {  get; set; }


        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        [Required]
        [MaxLength(50)]
        public string UnitOfMeasurement { get; set; }


        [Required]
        public int Price{ get; set; }


        [Required]
        [MaxLength(100)]
        public string Supplier { get; set; }


        [Required]
        [MaxLength(100)]
        public string Manufacturer { get; set; }


        [Required]
        [MaxLength(100)]
        public string Category { get; set; }


        [Required]
        public int Amount { get; set; }


        [Required]
        public int Discount { get; set; }

        [Required]
        public string Description { get; set; }

        public string Photo { get; set; }


        public List<OrderDetailsEntity> Details { get; set; }
    }
}
