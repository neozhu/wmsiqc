﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  public partial class PICKDETAIL : Entity
  {
    [Key]
    public int ID { get; set; }
    [Display(Name = "拣货单号", Description = "拣货单号")]
    [Index("PICKDETAIL_IX", IsUnique = true, Order = 1)]
    [Required]
    [MaxLength(20)]
    [DefaultValue("00000001")]
    public string PICKDETAILKEY { get; set; }
    [Display(Name = "仓库号", Description = "仓库号")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("WHSE01")]
    public string WHSEID { get; set; }
    [Display(Name = "出货单号", Description = "出货单号")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("00000001")]
    public string ORDERKEY { get; set; }
    [Display(Name = "行号", Description = "行号")]
    [Required]
    [DefaultValue("001")]
    public string ORDERLINENUMBER { get; set; }

    [Display(Name = "入库批次号", Description = "入库批次号")]
    [MaxLength(30)]
    public string EXTERNRECEIPTKEY { get; set; }
    [Display(Name = "货主", Description = "货主")]
    [MaxLength(50)]
    [Required]
    [DefaultValue("方太")]
    public string STORERKEY { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(3)]
    [DefaultValue("105")]
    public string STATUS { get; set; }

    [Display(Name = "物料编号", Description = "物料编号")]
    [MaxLength(30)]
    [Required]
    public string SKU { get; set; }
    [Display(Name = "物料名称", Description = "物料名称")]
    [MaxLength(80)]
    public string SKUNAME { get; set; }
    [Display(Name = "数量", Description = "数量")]
    [DefaultValue(0)]
    public decimal QTY { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    [Required]
    [DefaultValue("EA")]
    public string UMO { get; set; }
    [Display(Name = "件数", Description = "件数")]
    [DefaultValue(0)]
    public decimal CASECNT { get; set; }
    [Display(Name = "库位", Description = "库位(良品区/不良品区)")]
    [MaxLength(20)]
    public string LOC { get; set; }
    [Display(Name = "批号", Description = "批号")]
    [MaxLength(20)]
    public string LOT { get; set; }
    [Display(Name = "LPN", Description = "LPN")]
    [MaxLength(20)]
    public string LPN { get; set; }
    [Display(Name = "供应商代码", Description = "供应商代码")]
    [MaxLength(20)]
    public string SUPPLIERCODE { get; set; }
    [Display(Name = "供应商名称", Description = "供应商名称")]
    [MaxLength(80)]
    public string SUPPLIERNAME { get; set; }
    
    [Display(Name = "收货单号", Description = "收货单号")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("00000001")]
    public string RECEIPTKEY { get; set; }
    
    [Display(Name = "订单编号", Description = "订单编号")]
    [MaxLength(30)]
    public string POKEY { get; set; }
   
    [Display(Name = "收货单日期", Description = "收货单日期")]
    [DefaultValue("now")]
    public DateTime RECEIPTDATE { get; set; }

    
    
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
   


    [Display(Name = "工厂名称", Description = "工厂名称")]
    [DefaultValue("-")]
    [MaxLength(50)]
    public string LOTTABLE1 { get; set; }
    [Display(Name = "公司名称", Description = "公司名称")]
    [DefaultValue("-")]
    [MaxLength(50)]
    public string LOTTABLE2 { get; set; }
    [Display(Name = "供应商标签", Description = "供应商标签")]
    [DefaultValue("-")]
    [MaxLength(50)]
    public string LOTTABLE3 { get; set; }

    [Display(Name = "发货单号", Description = "发货单号")]
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
    [Display(Name = "出货单ID", Description = "出货单ID")]
    public int ORDERID { get; set; }
    [ForeignKey("ORDERID")]
    [Display(Name = "出货单信息", Description = "出货单信息")]
    public virtual ORDER ORDER { get; set; }
  }
}