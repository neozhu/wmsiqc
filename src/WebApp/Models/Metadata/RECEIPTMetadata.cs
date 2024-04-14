using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="RECEIPTMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2019/6/25 7:36:07 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(RECEIPTMetadata))]
    public partial class RECEIPT
    {
    }

    public partial class RECEIPTMetadata
    {
        [Display(Name = "RECEIPTDETAILS",Description ="RECEIPTDETAILS",Prompt = "RECEIPTDETAILS",ResourceType = typeof(resource.RECEIPT))]
        public RECEIPTDETAIL RECEIPTDETAILS { get; set; }

        [Required(ErrorMessage = "Please enter : ID")]
        [Display(Name = "ID",Description ="ID",Prompt = "ID",ResourceType = typeof(resource.RECEIPT))]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter : 收货单号")]
        [Display(Name = "RECEIPTKEY",Description ="收货单号",Prompt = "收货单号",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(20)]
        public string RECEIPTKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 仓库号")]
        [Display(Name = "WHSEID",Description ="仓库号",Prompt = "仓库号",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(20)]
        public string WHSEID { get; set; }

        [Display(Name = "EXTERNRECEIPTKEY",Description ="批次号",Prompt = "批次号",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(30)]
        public string EXTERNRECEIPTKEY { get; set; }

        [Display(Name = "POKEY",Description ="订单编号",Prompt = "订单编号",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(30)]
        public string POKEY { get; set; }

        [Display(Name = "SUSR2",Description ="公司名称",Prompt = "公司名称",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(80)]
        public string SUSR2 { get; set; }

        [Display(Name = "SUSR3",Description ="工厂名称",Prompt = "工厂名称",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(80)]
        public string SUSR3 { get; set; }

        [Required(ErrorMessage = "Please enter : 货主")]
        [Display(Name = "STORERKEY",Description ="货主",Prompt = "货主",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(50)]
        public string STORERKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 收货单类型(收货/退货)")]
        [Display(Name = "TYPE",Description ="收货单类型(收货/退货)",Prompt = "收货单类型(收货/退货)",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(1)]
        public string TYPE { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
        [Display(Name = "STATUS",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(3)]
        public string STATUS { get; set; }

        [Required(ErrorMessage = "Please enter : 收货单日期")]
        [Display(Name = "RECEIPTDATE",Description ="收货单日期",Prompt = "收货单日期",ResourceType = typeof(resource.RECEIPT))]
        public DateTime RECEIPTDATE { get; set; }

        [Required(ErrorMessage = "Please enter : 到货通知日期")]
        [Display(Name = "ADVICEDATE",Description ="到货通知日期",Prompt = "到货通知日期",ResourceType = typeof(resource.RECEIPT))]
        public DateTime ADVICEDATE { get; set; }

        [Display(Name = "EXPECTEDRECEIPTDATE",Description ="预计收货日期",Prompt = "预计收货日期",ResourceType = typeof(resource.RECEIPT))]
        public DateTime EXPECTEDRECEIPTDATE { get; set; }

        [Display(Name = "ARRIVALDATETIME",Description ="实际到货日期",Prompt = "实际到货日期",ResourceType = typeof(resource.RECEIPT))]
        public DateTime ARRIVALDATETIME { get; set; }

        [Display(Name = "CLOSEDDATE",Description ="结案日期",Prompt = "结案日期",ResourceType = typeof(resource.RECEIPT))]
        public DateTime CLOSEDDATE { get; set; }

        [Display(Name = "SUPPLIERCODE",Description ="供应商代码",Prompt = "供应商代码",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(20)]
        public string SUPPLIERCODE { get; set; }

        [Display(Name = "SUPPLIERNAME",Description ="供应商名称",Prompt = "供应商名称",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(80)]
        public string SUPPLIERNAME { get; set; }

        [Display(Name = "CARRIERKEY",Description ="承运商代码",Prompt = "承运商代码",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(20)]
        public string CARRIERKEY { get; set; }

        [Display(Name = "CARRIERNAME",Description ="承运商名称",Prompt = "承运商名称",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(80)]
        public string CARRIERNAME { get; set; }

        [Required(ErrorMessage = "Please enter : 合计数量")]
        [Display(Name = "TOTALQTY",Description ="合计数量",Prompt = "合计数量",ResourceType = typeof(resource.RECEIPT))]
        public decimal TOTALQTY { get; set; }

        [Required(ErrorMessage = "Please enter : 合计未收数量")]
        [Display(Name = "OPENQTY",Description ="合计未收数量",Prompt = "合计未收数量",ResourceType = typeof(resource.RECEIPT))]
        public decimal OPENQTY { get; set; }

        [Display(Name = "TOTALPACKAGE",Description ="合计件数",Prompt = "合计件数",ResourceType = typeof(resource.RECEIPT))]
        public decimal TOTALPACKAGE { get; set; }

        [Display(Name = "TOTALGROSSWEIGHT",Description ="合计毛重(KG)",Prompt = "合计毛重(KG)",ResourceType = typeof(resource.RECEIPT))]
        public decimal TOTALGROSSWEIGHT { get; set; }

        [Display(Name = "TOTALVOLUME",Description ="合计体积(M)",Prompt = "合计体积(M)",ResourceType = typeof(resource.RECEIPT))]
        public decimal TOTALVOLUME { get; set; }

        [Display(Name = "NOTES",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(100)]
        public string NOTES { get; set; }

        [Display(Name = "SUSR1",Description ="上传用户",Prompt = "上传用户",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(50)]
        public string SUSR1 { get; set; }

        [Display(Name = "SUSR4",Description ="SUSR4",Prompt = "SUSR4",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(50)]
        public string SUSR4 { get; set; }

        [Display(Name = "SUSR5",Description ="SUSR5",Prompt = "SUSR5",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(50)]
        public string SUSR5 { get; set; }

        [Display(Name = "SUSR6",Description ="SUSR6",Prompt = "SUSR6",ResourceType = typeof(resource.RECEIPT))]
        public decimal SUSR6 { get; set; }

        [Display(Name = "SUSR7",Description ="SUSR7",Prompt = "SUSR7",ResourceType = typeof(resource.RECEIPT))]
        public decimal SUSR7 { get; set; }

        [Display(Name = "SUSR8",Description ="SUSR8",Prompt = "SUSR8",ResourceType = typeof(resource.RECEIPT))]
        public DateTime SUSR8 { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.RECEIPT))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.RECEIPT))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.RECEIPT))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

    }

}
