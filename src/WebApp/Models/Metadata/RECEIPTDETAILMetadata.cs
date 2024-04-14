using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="RECEIPTDETAILMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2019/6/25 7:29:11 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(RECEIPTDETAILMetadata))]
    public partial class RECEIPTDETAIL
    {
    }

    public partial class RECEIPTDETAILMetadata
    {
        [Display(Name = "RECEIPT",Description ="收货单信息",Prompt = "收货单信息",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public RECEIPT RECEIPT { get; set; }

        [Required(ErrorMessage = "Please enter : ID")]
        [Display(Name = "ID",Description ="ID",Prompt = "ID",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter : 收货单号")]
        [Display(Name = "RECEIPTKEY",Description ="收货单号",Prompt = "收货单号",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(20)]
        public string RECEIPTKEY { get; set; }

        [Display(Name = "EXTERNRECEIPTKEY",Description ="批次号",Prompt = "批次号",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(30)]
        public string EXTERNRECEIPTKEY { get; set; }

        [Display(Name = "POKEY",Description ="订单编号",Prompt = "订单编号",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(30)]
        public string POKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 收货单日期")]
        [Display(Name = "RECEIPTDATE",Description ="收货单日期",Prompt = "收货单日期",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public DateTime RECEIPTDATE { get; set; }

        [Required(ErrorMessage = "Please enter : 仓库号")]
        [Display(Name = "WHSEID",Description ="仓库号",Prompt = "仓库号",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(20)]
        public string WHSEID { get; set; }

        [Display(Name = "LOTTABLE1",Description ="工厂名称",Prompt = "工厂名称",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE1 { get; set; }

        [Display(Name = "LOTTABLE2",Description ="公司名称",Prompt = "公司名称",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE2 { get; set; }

        [Required(ErrorMessage = "Please enter : 货主")]
        [Display(Name = "STORERKEY",Description ="货主",Prompt = "货主",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(50)]
        public string STORERKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 行号")]
        [Display(Name = "RECEIPTLINENUMBER",Description ="行号",Prompt = "行号",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(6)]
        public string RECEIPTLINENUMBER { get; set; }

        [Required(ErrorMessage = "Please enter : 物料编号")]
        [Display(Name = "SKU",Description ="物料编号",Prompt = "物料编号",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(30)]
        public string SKU { get; set; }

        [Display(Name = "SKUNAME",Description ="物料名称",Prompt = "物料名称",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(80)]
        public string SKUNAME { get; set; }

        [Display(Name = "ALTSKU",Description ="供应商物料编号",Prompt = "供应商物料编号",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(30)]
        public string ALTSKU { get; set; }

        [Display(Name = "STAUS",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(3)]
        public string STAUS { get; set; }

        [Display(Name = "SUPPLIERCODE",Description ="供应商代码",Prompt = "供应商代码",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(20)]
        public string SUPPLIERCODE { get; set; }

        [Display(Name = "SUPPLIERNAME",Description ="供应商名称",Prompt = "供应商名称",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(80)]
        public string SUPPLIERNAME { get; set; }

        [Display(Name = "DATERECEIVED",Description ="收货日期",Prompt = "收货日期",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public DateTime DATERECEIVED { get; set; }

        [Required(ErrorMessage = "Please enter : 预收量")]
        [Display(Name = "QTYEXPECTED",Description ="预收量",Prompt = "预收量",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal QTYEXPECTED { get; set; }

        [Required(ErrorMessage = "Please enter : 件数")]
        [Display(Name = "CASECNT",Description ="件数",Prompt = "件数",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal CASECNT { get; set; }

        [Required(ErrorMessage = "Please enter : 栈板数")]
        [Display(Name = "PALLET",Description ="栈板数",Prompt = "栈板数",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal PALLET { get; set; }

        [Required(ErrorMessage = "Please enter : 调整量")]
        [Display(Name = "QTYADJUSTED",Description ="调整量",Prompt = "调整量",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal QTYADJUSTED { get; set; }

        [Required(ErrorMessage = "Please enter : 拒收量")]
        [Display(Name = "QTYREJECTED",Description ="拒收量",Prompt = "拒收量",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal QTYREJECTED { get; set; }

        [Required(ErrorMessage = "Please enter : 已收量")]
        [Display(Name = "QTYRECEIVED",Description ="已收量",Prompt = "已收量",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal QTYRECEIVED { get; set; }

        [Required(ErrorMessage = "Please enter : 单位")]
        [Display(Name = "UMO",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(10)]
        public string UMO { get; set; }

        [Display(Name = "PACKKEY",Description ="包装",Prompt = "包装",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(10)]
        public string PACKKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 毛重(KG)")]
        [Display(Name = "GROSSWGT",Description ="毛重(KG)",Prompt = "毛重(KG)",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal GROSSWGT { get; set; }

        [Required(ErrorMessage = "Please enter : 净重(KG)")]
        [Display(Name = "NETWGT",Description ="净重(KG)",Prompt = "净重(KG)",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal NETWGT { get; set; }

        [Required(ErrorMessage = "Please enter : 体积(M)")]
        [Display(Name = "CUBE",Description ="体积(M)",Prompt = "体积(M)",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal CUBE { get; set; }

        [Display(Name = "NOTES",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(100)]
        public string NOTES { get; set; }

        [Display(Name = "LOTTABLE3",Description ="外箱标签",Prompt = "外箱标签",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE3 { get; set; }

        [Required(ErrorMessage = "Please enter : QC数量")]
        [Display(Name = "PQCQTYINSPECTED",Description ="QC数量",Prompt = "QC数量",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal PQCQTYINSPECTED { get; set; }

        [Required(ErrorMessage = "Please enter : QC不良数")]
        [Display(Name = "PQCQTYREJECTED",Description ="QC不良数",Prompt = "QC不良数",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal PQCQTYREJECTED { get; set; }

        [Display(Name = "PQCSTATUS",Description ="QC状态",Prompt = "QC状态",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(10)]
        public string PQCSTATUS { get; set; }

        [Display(Name = "PQCWHO",Description ="QC人员",Prompt = "QC人员",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(10)]
        public string PQCWHO { get; set; }

        [Display(Name = "PQCDATE",Description ="QC日期",Prompt = "QC日期",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public DateTime PQCDATE { get; set; }

        [Display(Name = "CONDITIONCODE",Description ="入库条件",Prompt = "入库条件",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(10)]
        public string CONDITIONCODE { get; set; }

        [Display(Name = "TOLOC",Description ="目标库位(良品区/不良品区)",Prompt = "目标库位(良品区/不良品区)",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(20)]
        public string TOLOC { get; set; }

        [Display(Name = "TOLOT",Description ="目标批号",Prompt = "目标批号",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(20)]
        public string TOLOT { get; set; }

        [Display(Name = "TOLPN",Description ="目标LPN",Prompt = "目标LPN",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(20)]
        public string TOLPN { get; set; }

        [Display(Name = "EXTERNLINENO",Description ="外部单行号",Prompt = "外部单行号",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(20)]
        public string EXTERNLINENO { get; set; }

        [Display(Name = "LOTTABLE4",Description ="LOTTABLE4",Prompt = "LOTTABLE4",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE4 { get; set; }

        [Display(Name = "LOTTABLE5",Description ="LOTTABLE5",Prompt = "LOTTABLE5",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE5 { get; set; }

        [Display(Name = "LOTTABLE6",Description ="LOTTABLE6",Prompt = "LOTTABLE6",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal LOTTABLE6 { get; set; }

        [Display(Name = "LOTTABLE7",Description ="LOTTABLE7",Prompt = "LOTTABLE7",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public decimal LOTTABLE7 { get; set; }

        [Display(Name = "LOTTABLE8",Description ="LOTTABLE7",Prompt = "LOTTABLE7",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public DateTime LOTTABLE8 { get; set; }

        [Required(ErrorMessage = "Please enter : 收货单ID")]
        [Display(Name = "RECEIPTID",Description ="收货单ID",Prompt = "收货单ID",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public int RECEIPTID { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.RECEIPTDETAIL))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.RECEIPTDETAIL))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

    }

}
