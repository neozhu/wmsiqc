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
  public partial class RECEIPTDETAIL:Entity
  {
    [Key]
    public int ID { get; set; }
    [Display(Name = "收货单号", Description = "收货单号")]
    [MaxLength(20)]
    [Index("RECEIPTDETAIL_IX", IsUnique = true,Order =1)]
    [Required]
    [DefaultValue("00000001")]
    public string RECEIPTKEY { get; set; }
    [Display(Name = "批次号", Description = "批次号")]
    [MaxLength(30)]
    public string EXTERNRECEIPTKEY { get; set; }
    [Display(Name = "订单编号", Description = "订单编号")]
    [MaxLength(30)]
    public string POKEY { get; set; }
    [Display(Name = "收货单日期", Description = "收货单日期")]
    [DefaultValue("now")]
    public DateTime RECEIPTDATE { get; set; }
    
    [Display(Name = "仓库号", Description = "仓库号")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("WHSE01")]
    public string WHSEID { get; set; }
    [Display(Name = "工厂名称", Description = "工厂名称")]
    [DefaultValue("-")]
    [MaxLength(50)]
    public string LOTTABLE1 { get; set; }
    [Display(Name = "公司名称", Description = "公司名称")]
    [MaxLength(50)]
    [DefaultValue("-")]
    public string LOTTABLE2 { get; set; }

    [Display(Name = "货主", Description = "货主")]
    [MaxLength(50)]
    [Required]
    [DefaultValue("方太")]
    public string STORERKEY { get; set; }
    [Display(Name = "行号", Description = "行号")]
    [MaxLength(6)]
    [Required]
    [Index("RECEIPTDETAIL_IX", IsUnique = true, Order = 2)]
    [DefaultValue("001")]
    public string RECEIPTLINENUMBER { get; set; }
    
    
    [Display(Name = "物料编号", Description = "物料编号")]
    [MaxLength(30)]
    [Required]
    public string SKU { get; set; }
    [Display(Name = "物料名称", Description = "物料名称")]
    [MaxLength(80)]
    public string SKUNAME { get; set; }
    [Display(Name = "供应商物料编号", Description = "供应商物料编号")]
    [MaxLength(30)]
    public string ALTSKU { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(3)]
    [DefaultValue("0")]
    public string STATUS { get; set; }
    [Display(Name = "供应商代码", Description = "供应商代码")]
    [MaxLength(20)]
    public string SUPPLIERCODE { get; set; }
    [Display(Name = "供应商名称", Description = "供应商名称")]
    [MaxLength(80)]
    public string SUPPLIERNAME { get; set; }
   
    [Display(Name = "收货日期", Description = "收货日期")]
    [DefaultValue(null)]
    public DateTime? DATERECEIVED { get; set; }
    [Display(Name = "预收量", Description = "预收量")]
    [DefaultValue(0)]
    public decimal QTYEXPECTED { get; set; }
    [Display(Name = "件数", Description = "件数")]
    [DefaultValue(0)]
    public decimal CASECNT { get; set; }
    [Display(Name = "栈板数", Description = "栈板数")]
    [DefaultValue(0)]
    public decimal PALLET { get; set; }

    [Display(Name = "调整量", Description = "调整量")]
    [DefaultValue(0)]
    public decimal QTYADJUSTED { get; set; }
    [Display(Name = "拒收量", Description = "拒收量")]
    [DefaultValue(0)]
    public decimal QTYREJECTED { get; set; }
    [Display(Name = "已收量", Description = "已收量")]
    [DefaultValue(0)]
    public decimal QTYRECEIVED { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    [Required]
    [DefaultValue("EA")]
    public string UMO { get; set; }
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

    [Display(Name = "供应商标签", Description = "供应商标签")]
    [DefaultValue("-")]
    [MaxLength(50)]
    public string LOTTABLE3 { get; set; }

    [Display(Name = "QC数量", Description = "QC数量")]
    [DefaultValue(0)]
    public decimal PQCQTYINSPECTED { get; set; }
    [Display(Name = "QC不良数", Description = "QC不良数")]
    [DefaultValue(0)]
    public decimal PQCQTYREJECTED { get; set;  }
    [Display(Name = "QC状态", Description = "QC状态")]
    [MaxLength(10)]
    public string PQCSTATUS { get; set; }
    [Display(Name = "QC人员", Description = "QC人员")]
    [MaxLength(10)]
    public string PQCWHO { get; set; }
    [Display(Name = "QC日期", Description = "QC日期")]
    [DefaultValue(null)]
    public DateTime? PQCDATE { get; set; }
    [Display(Name = "入库条件", Description = "入库条件")]
    [DefaultValue("OK")]
    [MaxLength(10)]
    public string CONDITIONCODE { get; set; }

    [Display(Name = "目标库位", Description = "目标库位(良品区/不良品区)")]
    [MaxLength(20)]
    public string TOLOC { get; set; }
    [Display(Name = "目标批号", Description = "目标批号")]
    [MaxLength(20)]
    public string TOLOT { get; set; }
    [Display(Name = "目标LPN", Description = "目标LPN")]
    [MaxLength(20)]
    public string TOLPN { get; set; }
    [Display(Name = "外部单行号", Description = "外部单行号")]
    [MaxLength(20)]
    public string EXTERNLINENO { get; set; }

    

    
    

    [Display(Name = "发货单号", Description = "发货单号")]
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
    [Display(Name = "收货单ID", Description = "收货单ID")]
    public int RECEIPTID { get; set; }
    [ForeignKey("RECEIPTID")]
    [Display(Name = "收货单信息", Description = "收货单信息")]
    public virtual RECEIPT RECEIPT { get; set; }



  }
}