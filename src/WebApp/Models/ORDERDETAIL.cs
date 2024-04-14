using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  public partial class ORDERDETAIL:Entity
  {
    [Key]
    public int ID { get; set; }
    [Display(Name = "出货单号", Description = "出货单号")]
    [MaxLength(20)]
    [Index("ORDERDETAIL_IX", IsUnique = true,Order =1)]
    [Required]
    [DefaultValue("00000001")]
    public string ORDERKEY { get; set; }
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
    [Display(Name = "批次号", Description = "批次号")]
    [MaxLength(50)]
    public string EXTERNORDERKEY { get; set; }
    [Display(Name = "行号", Description = "行号")]
    [MaxLength(6)]
    [Index("ORDERDETAIL_IX", IsUnique = true, Order = 2)]
    [Required]
    [DefaultValue("001")]
    public string ORDERLINENUMBER { get; set; }

    [Display(Name = "状态", Description = "状态")]
    [MaxLength(3)]
    [DefaultValue("105")]
    public string STATUS { get; set; }
    [Display(Name = "配送厂区", Description = "配送厂区")]
    [DefaultValue("-")]
    [MaxLength(50)]
    public string LOTTABLE2 { get; set; }
    [Display(Name = "供应商代码", Description = "供应商代码")]
    [MaxLength(20)]
    public string SUPPLIERCODE { get; set; }
    [Display(Name = "供应商名称", Description = "供应商名称")]
    [MaxLength(80)]
    public string SUPPLIERNAME { get; set; }
    [Display(Name = "供应商物料编号", Description = "供应商物料编号")]
    [MaxLength(30)]
    [Required]
    public string SKU { get; set; }
    [Display(Name = "供应商物料名称", Description = "供应商物料名称")]
    [MaxLength(80)]
    public string SKUNAME { get; set; }
    [Display(Name = "第三方物料编号", Description = "第三方物料编号")]
    [MaxLength(30)]
    public string ALTSKU { get; set; }
   
    [Display(Name = "配送数量", Description = "配送数量")]
    [DefaultValue(0)]
    public decimal ORIGINALQTY { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    [Required]
    [DefaultValue("EA")]
    public string UMO { get; set; }
    [Display(Name = "配送件数", Description = "配送件数")]
    [DefaultValue(0)]
    public decimal CASECNT { get; set; }
    [Display(Name = "未出货数量", Description = "未出货数量")]
    [DefaultValue(0)]
    public decimal OPENQTY { get; set; }
    [Display(Name = "已发运数量", Description = "已发运数量")]
    [DefaultValue(0)]
    public decimal SHIPPEDQTY { get; set; }
    [Display(Name = "已捡货数量", Description = "已捡货数量")]
    [DefaultValue(0)]
    public decimal QTYPICKED { get; set; }
    
    [Display(Name = "栈板数", Description = "栈板数")]
    [DefaultValue(0)]
    public decimal PALLET { get; set; }
    [Display(Name = "包装", Description = "包装")]
    [MaxLength(10)]
    [DefaultValue("-")]
    public string PACKKEY { get; set; }
    [Display(Name = "毛重(KG)", Description = "毛重(KG)")]
    [DefaultValue(0)]
    public decimal GROSSWGT { get; set; }
    [Display(Name = "净重(KG)", Description = "净重(KG)")]
    [DefaultValue(0)]
    public decimal NETWGT { get; set; }
    [Display(Name = "体积(M)", Description = "体积(M)")]
    [DefaultValue(0)]
    public decimal CUBE { get; set; }

    [Display(Name = "备注", Description = "备注")]
    [MaxLength(100)]
    public string NOTES { get; set; }

    [Display(Name = "QC数量", Description = "QC数量")]
    [DefaultValue(0)]
    public decimal PQCQTYINSPECTED { get; set; }
    [Display(Name = "QC不良数", Description = "QC不良数")]
    [DefaultValue(0)]
    public decimal PQCQTYREJECTED { get; set; }
    [Display(Name = "QC状态", Description = "QC状态")]
    [MaxLength(10)]
    public string PQCSTATUS { get; set; }
    [Display(Name = "QC人员", Description = "QC人员")]
    [MaxLength(10)]
    public string PQCWHO { get; set; }
    [Display(Name = "QC日期", Description = "QC日期")]
    [DefaultValue(null)]
    public DateTime? PQCDATE { get; set; }
    [Display(Name = "外箱标签", Description = "外箱标签")]
    [DefaultValue("-")]
    [MaxLength(50)]
    public string LOTTABLE3 { get; set; }
    [Display(Name = "上传用户", Description = "上传用户")]
    [DefaultValue("-")]
    [MaxLength(50)]
    public string LOTTABLE1 { get; set; }
    [Display(Name = "配送日期", Description = "配送日期")]
    [DefaultValue("now")]
    public DateTime? REQUESTEDSHIPDATE { get; set; }
    [Display(Name = "要求到货时间", Description = "要求到货时间")]
    [DefaultValue("now")]
    public DateTime? DELIVERYDATE { get; set; }
    [Display(Name = "实际配送日期", Description = "实际配送日期")]
    [DefaultValue(null)]
    public DateTime? ACTUALSHIPDATE { get; set; }
    [Display(Name = "实际到货时间", Description = "实际到货时间")]
    [DefaultValue(null)]
    public DateTime? ACTUALDELIVERYDATE { get; set; }
    [Display(Name = "外部行号", Description = "外部行号")]
    [MaxLength(20)]

    public string EXTERNLINENO { get; set; }
    [Display(Name = "出货方式", Description = "出货方式")]
    [MaxLength(10)]
    [DefaultValue("1")]
    public string TYPE { get; set; }

    [Display(Name = "LOTTABLE4", Description = "LOTTABLE4")]
    [MaxLength(50)]
    public string LOTTABLE4 { get; set; }
    [Display(Name = "方太标签", Description = "方太标签")]
    [MaxLength(50)]
    public string LOTTABLE5 { get; set; }
    [Display(Name = "LOTTABLE6", Description = "LOTTABLE6")]
    public decimal? LOTTABLE6 { get; set; }
    [Display(Name = "LOTTABLE7", Description = "LOTTABLE7")]
    public decimal? LOTTABLE7 { get; set; }
    [Display(Name = "LOTTABLE8", Description = "LOTTABLE7")]
    public DateTime? LOTTABLE8 { get; set; }
    [Display(Name = "LOTTABLE9", Description = "LOTTABLE9")]
    public DateTime? LOTTABLE9 { get; set; }
    [Display(Name = "LOTTABLE10", Description = "LOTTABLE10")]
    [MaxLength(50)]
    public string LOTTABLE10 { get; set; }
    [Display(Name = "出货单ID", Description = "出货单ID")]
    public int ORDERID { get; set; }
    [ForeignKey("ORDERID")]
    [Display(Name = "出货单信息", Description = "出货单信息")]
    public virtual ORDER ORDER { get; set; }
  }
}