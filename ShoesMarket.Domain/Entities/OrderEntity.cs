using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesMarket.Domain.Entities
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }


        [Column(TypeName = "Date")]
        public DateOnly OrderDate {  get; set; }



        [Column(TypeName = "Date")]
        public DateOnly DeliveryDate { get; set; }

        [ForeignKey(nameof(UserEntity))]
        public int UserId { get; set; }

        public UserEntity User { get; set; }


        [ForeignKey(nameof(PickupPointEntity))]
        public int PickupPointId { get; set; }

        public PickupPointEntity PickupPoint { get; set; }

        public int ReceiptCode { get; set; }

        public string Status {  get; set; }

        public List<OrderDetailsEntity> Details { get; set; }
    }
}
