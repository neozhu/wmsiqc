using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class POSTQTYRECEIVED
  {
    public int ID { get; set; }
    public decimal QTYRECEIVED { get; set; }
    public int RECEIPTID { get; set; }
  }
}