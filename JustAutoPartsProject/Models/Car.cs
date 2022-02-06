using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JustAutoPartsProject.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarID { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        private List<Part> parts = new List<Part>();
        public virtual List<Part> Parts
        {
            get { return this.parts; }
            set { this.parts = value; }
        }
        public Car() { }

        public Car(int carID, string modelName, int year, string color)
        {
            CarID = carID;
            ModelName = modelName;
            Year = year;
            Color = color;
        }
    }
}