using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  [Table("STOCKGROUPVIEW")]
  public partial class STOCKGROUPVIEW:Entity
  {
    [Key]
    public long RID { get; set; }

    [Display(Name = "供应商代码", Description = "供应商代码")]
    [MaxLength(20)]
    public string SUPPLIERCODE { get; set; }
    [Display(Name = "供应商名称", Description = "供应商名称")]
    [MaxLength(80)]
    public string SUPPLIERNAME { get; set; }
    [Display(Name = "物料编号", Description = "物料编号")]
    [MaxLength(30)]
    [Required]
    public string SKU { get; set; }
    [Display(Name = "物料名称", Description = "物料名称")]
    [MaxLength(80)]
    public string SKUNAME { get; set; }
    [Display(Name = "数量", Description = "数量")]
    public decimal TOTAL { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(10)]
    [Required]
    public string STATUS { get; set; }


  }
}