using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JustAutoPartsProject.Models
{
    public class Order
    {
        public Order() { PartsName = new List<string>(); }
        private int orderId;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID
        {
            get { return this.orderId; }
            set { this.orderId = value; }
        }
        Bill bill = new Bill();
        private string items;
        public string Items
        {
            get { return this.items; }
            set { this.items = value; }
        }
        public virtual Bill Bill
        {
            get { return this.bill; }
            set { this.bill = value; }
        }
        List<Part> parts = new List<Part>();
        public List<Part> Parts { get => parts; set => parts = value; }
        internal List<string> partsName { get; set; }
        [NotMapped]
        public List<string> PartsName
        {
            get { return this.partsName; }
            set { this.partsName = value; }
        }
        public virtual Part part { get; set; }
        public void createOrder(ShoppingCart cart)
        {
            part = cart.part;
            /*
             * part.PartID = cart.part.PartID;
             * part.Category = cart.part.Category;
             * part.Name = cart.part.Name;
             * part.Description = cart.part.Description;
             * part.Price = cart.part.Price;
             */
        }

    }
}