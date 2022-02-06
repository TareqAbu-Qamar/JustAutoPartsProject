using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JustAutoPartsProject.Models
{
    public class ShoppingCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShopCartID { get; set; }
        private List<Part> listOfParts = new List<Part>();
        public virtual List<Part> ListOfParts { get => listOfParts; set => listOfParts = value; }          

        public int PartID { get; set; }
        [ForeignKey("PartID")]
        public virtual Part part { get; set; }
        public ShoppingCart() { }
        public void addToShoppingCart(Part p)
        {
            part = p;
            /*
             * part.PartID = p.PartID;
             * part.Category = p.Category;
             * part.Name = p.Name;
             * part.Description = p.Description;
             * part.Price = p.Price;
            */
        }
        /*public void addToShoppingCart(List<Part> parts)
        {
            listOfParts = parts;
        }*/
    }
}
