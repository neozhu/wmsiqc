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
  public partial class RECEIPT : Entity
  {
    public RECEIPT()
    {
      this.RECEIPTDETAILS = new HashSet<RECEIPTDETAIL>();
    }
    [Key]
    public int ID { get; set; }
    [Display(Name = "收货单号", Description = "收货单号")]
    [MaxLength(20)]
    [Index(IsUnique = true)]
    [Required]
    [DefaultValue("00000001")]
    public string RECEIPTKEY { get; set; }
    [Display(Name = "仓库号", Description = "仓库号")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("WHSE01")]
    public string WHSEID { get; set; }
    [Display(Name = "批次号", Description = "批次号")]
    [MaxLength(30)]
    public string EXTERNRECEIPTKEY { get; set; }
    [Display(Name = "订单编号", Description = "订单编号")]
    [MaxLength(30)]
    public string POKEY { get; set; }

    [Display(Name = "公司名称", Description = "公司名称")]
    [MaxLength(80)]
    [DefaultValue("-")]
    public string SUSR2 { get; set; }
    [Display(Name = "工厂名称", Description = "工厂名称")]
    [MaxLength(80)]
    [DefaultValue("-")]
    public string SUSR3 { get; set; }
    [Display(Name = "货主", Description = "货主")]
    [MaxLength(50)]
    [Required]
    [DefaultValue("方太")]
    public string STORERKEY { get; set; }
    [Display(Name = "收货单类型", Description = "收货单类型(收货/退货)")]
    [MaxLength(1)]
    [Required]
    [DefaultValue("1")]
    public string TYPE { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(3)]
    [Required]
    [DefaultValue("100")]
    public string STATUS { get; set; }

    [Display(Name = "收货单日期", Description = "收货单日期")]
    [DefaultValue("now")]
    public DateTime RECEIPTDATE { get; set; }

    [Display(Name = "到货通知日期", Description = "到货通知日期")]
    [DefaultValue("now")]
    public DateTime ADVICEDATE { get; set; }
    [Display(Name = "预计收货日期", Description = "预计收货日期")]
    [DefaultValue(null)]
    public DateTime? EXPECTEDRECEIPTDATE{ get; set; }
    [Display(Name = "实际到货日期", Description = "实际到货日期")]
    [DefaultValue(null)]
    public DateTime? ARRIVALDATETIME { get; set; }
    
    [Display(Name = "结案日期", Description = "结案日期")]
    [DefaultValue(null)]
    public DateTime? CLOSEDDATE { get; set; }
    [Display(Name = "供应商代码", Description = "供应商代码")]
    [MaxLength(20)]
   
    public string SUPPLIERCODE { get; set; }
    [Display(Name = "供应商名称", Description = "供应商名称")]
    [MaxLength(80)]

    public string SUPPLIERNAME { get; set; }
    [Display(Name = "承运商代码", Description = "承运商代码")]
    [MaxLength(20)]
    public string CARRIERKEY { get; set; }
    [Display(Name = "承运商名称", Description = "承运商名称")]
    [MaxLength(80)]
    public string CARRIERNAME { get; set; }
    
    [Display(Name = "合计数量", Description = "合计数量")]
    [DefaultValue(0)]
    public decimal TOTALQTY { get; set; }
    [Display(Name = "合计未收数量", Description = "合计未收数量")]
    [DefaultValue(0)]
    public decimal OPENQTY { get; set; }
    [DefaultValue(0)]
    [Display(Name = "合计件数", Description = "合计件数")]

    public decimal? TOTALPACKAGE { get; set; }
    [Display(Name = "合计毛重(KG)", Description = "合计毛重(KG)")]
    [DefaultValue(0)]
    public decimal? TOTALGROSSWEIGHT { get; set; }
    [Display(Name = "合计体积(M)", Description = "合计体积(M)")]
    [DefaultValue(0)]
    public decimal? TOTALVOLUME { get; set; }

    [Display(Name = "备注", Description = "备注")]
    [MaxLength(100)]
    public string NOTES { get; set; }


    [Display(Name = "上传用户", Description = "上传用户")]
    [MaxLength(50)]
    [DefaultValue("user")]
    public string SUSR1 { get; set; }

    
    
    [Display(Name = "发货单号", Description = "发货单号")]
    [MaxLength(50)]
    public string SUSR4 { get; set; }
    [Display(Name = "SUSR5", Description = "SUSR5")]
    [MaxLength(50)]
    public string SUSR5 { get; set; }
    [Display(Name = "SUSR6", Description = "SUSR6")]
    [DefaultValue(null)]
    public decimal? SUSR6 { get; set; }
    [Display(Name = "SUSR7", Description = "SUSR7")]
    [DefaultValue(null)]
    public decimal? SUSR7 { get; set; }
    [Display(Name = "SUSR8", Description = "SUSR8")]
    [DefaultValue(null)]
    public DateTime? SUSR8 { get; set; }

    public virtual ICollection<RECEIPTDETAIL> RECEIPTDETAILS { get; set; }





  }
}