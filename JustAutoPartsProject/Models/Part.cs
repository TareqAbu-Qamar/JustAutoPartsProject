using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JustAutoPartsProject.Models
{
    public class Part
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than {1}")]
        public int Quantity { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value greater than {1}")]
        public double Price { get; set; }
        public double? DefualtPrice { get; set; }
        public bool? SelectedItem { get; set; }
        public string Image { get; set; }
        public Part() { }
    }
}