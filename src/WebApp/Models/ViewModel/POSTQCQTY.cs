using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class POSTQCQTY
  {
    public int ID { get; set; }
    public decimal PQCQTYINSPECTED { get; set; }
    public string STATUS { get; set; }
    public string SETSTATUS { get; set; }
  }
}