using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class POSTQTYSHIPPED
  {
    public int ID { get; set; }
    public int ORDERID { get; set; }
    public decimal QTYSHIPPED { get; set; }
  }
}