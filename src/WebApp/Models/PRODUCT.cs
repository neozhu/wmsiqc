using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  public partial class PRODUCT:Entity
  {
    [Key]
    public int ID { get; set; }
    [Display(Name = "仓库号", Description = "仓库号")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("WHSE01")]
    public string WHSEID { get; set; }

    [Display(Name = "货主", Description = "货主")]
    [MaxLength(50)]
    [Required]
    [DefaultValue("方太")]
    public string STORERKEY { get; set; }
    [Display(Name = "物料编号", Description = "物料编号")]
    [MaxLength(30)]
    [Required]
    public string SKU { get; set; }
    [Display(Name = "物料名称", Description = "物料名称")]
    [MaxLength(80)]
    public string SKUNAME { get; set; }
    [Display(Name = "物料别名", Description = "物料别名")]
    [MaxLength(30)]
    public string ALTSKU { get; set; }
    [Display(Name = "分类", Description = "分类")]
    [MaxLength(10)]
    [Required]
    [DefaultValue("STD")]
    public string CLASS { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    [Required]
    [DefaultValue("EA")]
    public string UMO { get; set; }
    [Display(Name = "包装", Description = "包装")]
    [MaxLength(50)]
    [DefaultValue("1-1-1-1")]
    public string PACKKEY { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(150)]
    public string NOTES { get; set; }
    [Display(Name = "毛重", Description = "毛重")]
    [DefaultValue(0.00)]
    public decimal STDGROSSWGT { get; set; }
    [Display(Name = "净重", Description = "净重")]
    [DefaultValue(0.00)]
    public decimal STDNETWGT { get; set; }
    [Display(Name = "体积", Description = "体积")]
    [DefaultValue(0.00)]
    public decimal STDCUBE { get; set; }
    [Display(Name = "皮重", Description = "皮重")]
    [DefaultValue(0.00)]
    public decimal STDTARE { get; set; }
    [Display(Name = "单价", Description = "单价")]
    [DefaultValue(0.00)]
    public decimal PRICE { get; set; }
    
    [Display(Name = "是否可用", Description = "是否可用")]
    [DefaultValue(true)]
    public bool ACTIVE { get; set; }
    [Display(Name = "收货QC储位", Description = "收货QC储位")]
    [MaxLength(30)]
    [DefaultValue("QC")]
    public string RECEIPTINSPECTIONLOC { get; set; }
    [Display(Name = "LOTTABLE1", Description = "LOTTABLE1")]
    [MaxLength(50)]
    public string LOTTABLE1 { get; set; }
    [Display(Name = "LOTTABLE2", Description = "LOTTABLE2")]
    [MaxLength(50)]
    public string LOTTABLE2 { get; set; }
    [Display(Name = "LOTTABLE3", Description = "LOTTABLE3")]
    [MaxLength(50)]
    public string LOTTABLE3 { get; set; }

    [Display(Name = "LOTTABLE4", Description = "LOTTABLE4")]
    [MaxLength(50)]
    public string LOTTABLE4 { get; set; }
    [Display(Name = "LOTTABLE5", Description = "LOTTABLE5")]
    [MaxLength(50)]
    public string LOTTABLE5 { get; set; }
    [Display(Name = "LOTTABLE6", Description = "LOTTABLE6")]
    public decimal? LOTTABLE6 { get; set; }
    [Display(Name = "LOTTABLE7", Description = "LOTTABLE7")]
    public decimal? LOTTABLE7 { get; set; }
    [Display(Name = "LOTTABLE8", Description = "LOTTABLE7")]
    public DateTime? LOTTABLE8 { get; set; }
  }
}