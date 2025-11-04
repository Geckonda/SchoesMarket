using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesMarket.Domain.Entities
{
    public class OrderDetailsEntity
    {
        public int Id { get; set; }

        [ForeignKey(nameof(OrderEntity))]
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }

        [ForeignKey(nameof(ProductEntity))]
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }

        public int Quantitty { get; set; }
    }
}
