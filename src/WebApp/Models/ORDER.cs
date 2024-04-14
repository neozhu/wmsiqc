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
  public partial class ORDER:Entity
  {
    public ORDER()
    {
      this.ORDERDETAILS = new HashSet<ORDERDETAIL>();
      this.PICKDETAILS = new HashSet<PICKDETAIL>();
    }
    [Key]
    public int ID { get; set; }
    [Display(Name = "出货单号", Description = "出货单号")]
    [MaxLength(20)]
    [Index(IsUnique = true)]
    [Required]
    [DefaultValue("00000001")]
    public string ORDERKEY { get; set; }
    
    [Display(Name = "仓库号", Description = "仓库号")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("WHSE01")]
    public string WHSEID { get; set; }
    [Display(Name = "批次号", Description = "批次号")]
    [MaxLength(50)]
    public string EXTERNORDERKEY { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(3)]
    [Required]
    [DefaultValue("100")]
    public string STATUS { get; set; }
    [Display(Name = "订单日期", Description = "订单日期")]
    [DefaultValue("now")]
    public DateTime ORDERDATE { get; set; }
    [Display(Name = "配送日期", Description = "配送日期")]
    [DefaultValue("now")]
    public DateTime? REQUESTEDSHIPDATE { get; set; }
    [Display(Name = "要求到货时间", Description = "要求到货时间")]
    [DefaultValue("now")]
    public DateTime? DELIVERYDATE { get; set; }
    [Display(Name = "货主", Description = "货主")]
    [MaxLength(50)]
    [Required]
    [DefaultValue("方太")]
    public string STORERKEY { get; set; }
    [Display(Name = "出货方式", Description = "出货方式")]
    [MaxLength(10)]
    [DefaultValue("1")]
    public string TYPE { get; set; }
    
    [Display(Name = "供应商代码", Description = "供应商代码")]
    [MaxLength(20)]

    public string SUPPLIERCODE { get; set; }
    [Display(Name = "供应商名称", Description = "供应商名称")]
    [MaxLength(80)]

    public string SUPPLIERNAME { get; set; }



    [Display(Name = "配送厂区", Description = "配送厂区")]
    [MaxLength(80)]
    [DefaultValue("-")]
    public string SUSR2 { get; set; }
    [Display(Name = "收货单位", Description = "收货单位")]
    [MaxLength(30)]
    public string CONSIGNEEKEY { get; set; }
    [Display(Name = "收货单位名称", Description = "收货单位名称")]
    [MaxLength(80)]
    public string CONSIGNEENAME { get; set; }
    [Display(Name = "收货地址", Description = "收货地址")]
    [MaxLength(80)]
    public string CONSIGNEEADDRESS { get; set; }

    [Display(Name = "承运人", Description = "承运人")]
    [MaxLength(80)]
    public string CARRIERNAME { get; set; }
    [Display(Name = "司机", Description = "司机")]
    [MaxLength(20)]
    public string DRIVERNAME { get; set; }
    [Display(Name = "司机电话", Description = "司机电话")]
    [MaxLength(20)]
    public string CARRIERPHONE { get; set; }
    [Display(Name = "运输车辆", Description = "运输车辆")]
    [MaxLength(80)]
    public string TRAILERNUMBER { get; set; }
    [Display(Name = "实际配送日期", Description = "实际配送日期")]
    [DefaultValue(null)]
    public DateTime? ACTUALSHIPDATE { get; set; }
    [Display(Name = "实际到货时间", Description = "实际到货时间")]
    [DefaultValue(null)]
    public DateTime? ACTUALDELIVERYDATE { get; set; }

    [Display(Name = "结案日期", Description = "结案日期")]
    [DefaultValue(null)]
    public DateTime? CLOSEDDATE { get; set; }
    [Display(Name = "合计出货数量", Description = "合计出货数量")]

    public decimal TOTALQTY { get; set; }

    [Display(Name = "合计未出数量", Description = "合计未出数量")]
    public decimal OPENQTY { get; set; }

    [Display(Name = "合计拣货数量", Description = "合计拣货数量")]

    public decimal TOTALQTYPICKED { get; set; }

    [Display(Name = "合计件数", Description = "合计件数")]

    public decimal? TOTALPACKAGE { get; set; }
    [Display(Name = "合计毛重(KG)", Description = "合计毛重(KG)")]
    public decimal? TOTALGROSSWEIGHT { get; set; }
    [Display(Name = "合计体积(M)", Description = "合计体积(M)")]
    public decimal? TOTALVOLUME { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(100)]
    public string NOTES { get; set; }
   

    
    
    [Display(Name = "上传用户", Description = "上传用户")]
    [MaxLength(50)]
    [DefaultValue("user")]
    public string SUSR1 { get; set; }


    [Display(Name = "SUSR3", Description = "SUSR3")]
    [MaxLength(80)]
    [DefaultValue("-")]
    public string SUSR3 { get; set; }
    [Display(Name = "SUSR4", Description = "SUSR4")]
    [MaxLength(60)]
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

    public virtual ICollection<ORDERDETAIL> ORDERDETAILS { get; set; }
    public virtual ICollection<PICKDETAIL> PICKDETAILS { get; set; }

    

  }
}