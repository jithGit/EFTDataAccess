using EFDataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Model
{
    public class Customer : Entity
    {
        public string CustomerName { get; set; }
        public string Telephome { get; set; }
        public string Comment { get; set; }
        public ICollection<Site> Sites { get; set; }
    }
    
    public class Site : Entity
    {
        public string SiteNumber { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public Customer Customer { get; set; }
       
        [ForeignKey("Customer")]
        public int Customer_Id { get; set; }
  
    }


}
