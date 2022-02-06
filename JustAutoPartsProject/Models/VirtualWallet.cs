using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JustAutoPartsProject.Models
{
    public class VirtualWallet
    {
        public VirtualWallet() { }
        private int id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VirtualWalletID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private double balance = 3000;
        public double Balance
        {
            get { return this.balance; }
            set { this.balance = value; }
        }
        public void pay(double price)
        {
            if (price <= balance)
            {
                this.balance -= price;
            }
               
        }
    }
}