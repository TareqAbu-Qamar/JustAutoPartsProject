using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JustAutoPartsProject.Models
{
    public class Bill
    {
        public Bill() { }
        private int billID;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillID
        {
            get { return this.billID; }
            set { this.billID = value; }
        }
        private double value;
        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private string status;
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        private DateTime billDate;

        [Display (Name = "Bill Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BillDate
        {
            get { return this.billDate; }
            set { this.billDate = value; }
        }
        public void generateBill(Order order)
        {
            double total = 0;
            total += (order.part.Price * order.part.Quantity);
            this.value = total;
            this.status = "processing";
            this.billDate = DateTime.Now;
        }
    }
}