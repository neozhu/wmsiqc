using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="PICKDETAILMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2019/6/27 8:22:40 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(PICKDETAILMetadata))]
    public partial class PICKDETAIL
    {
    }

    public partial class PICKDETAILMetadata
    {
        [Display(Name = "ORDER",Description ="出货单信息",Prompt = "出货单信息",ResourceType = typeof(resource.PICKDETAIL))]
        public ORDER ORDER { get; set; }

        [Required(ErrorMessage = "Please enter : ID")]
        [Display(Name = "ID",Description ="ID",Prompt = "ID",ResourceType = typeof(resource.PICKDETAIL))]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter : 拣货单号")]
        [Display(Name = "PICKDETAILKEY",Description ="拣货单号",Prompt = "拣货单号",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(20)]
        public string PICKDETAILKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 仓库号")]
        [Display(Name = "WHSEID",Description ="仓库号",Prompt = "仓库号",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(20)]
        public string WHSEID { get; set; }

        [Required(ErrorMessage = "Please enter : 出货单号")]
        [Display(Name = "ORDERKEY",Description ="出货单号",Prompt = "出货单号",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(20)]
        public string ORDERKEY { get; set; }

        [Display(Name = "EXTERNRECEIPTKEY",Description ="入库批次号",Prompt = "入库批次号",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(30)]
        public string EXTERNRECEIPTKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 货主")]
        [Display(Name = "STORERKEY",Description ="货主",Prompt = "货主",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(50)]
        public string STORERKEY { get; set; }

        [Display(Name = "STATUS",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(3)]
        public string STATUS { get; set; }

        [Required(ErrorMessage = "Please enter : 物料编号")]
        [Display(Name = "SKU",Description ="物料编号",Prompt = "物料编号",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(30)]
        public string SKU { get; set; }

        [Display(Name = "SKUNAME",Description ="物料名称",Prompt = "物料名称",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(80)]
        public string SKUNAME { get; set; }

        [Required(ErrorMessage = "Please enter : 数量")]
        [Display(Name = "QTY",Description ="数量",Prompt = "数量",ResourceType = typeof(resource.PICKDETAIL))]
        public decimal QTY { get; set; }

        [Required(ErrorMessage = "Please enter : 单位")]
        [Display(Name = "UMO",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(10)]
        public string UMO { get; set; }

        [Required(ErrorMessage = "Please enter : 件数")]
        [Display(Name = "CASECNT",Description ="件数",Prompt = "件数",ResourceType = typeof(resource.PICKDETAIL))]
        public decimal CASECNT { get; set; }

        [Display(Name = "LOC",Description ="库位(良品区/不良品区)",Prompt = "库位(良品区/不良品区)",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(20)]
        public string LOC { get; set; }

        [Display(Name = "LOT",Description ="批号",Prompt = "批号",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(20)]
        public string LOT { get; set; }

        [Display(Name = "LPN",Description ="LPN",Prompt = "LPN",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(20)]
        public string LPN { get; set; }

        [Display(Name = "SUPPLIERCODE",Description ="供应商代码",Prompt = "供应商代码",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(20)]
        public string SUPPLIERCODE { get; set; }

        [Display(Name = "SUPPLIERNAME",Description ="供应商名称",Prompt = "供应商名称",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(80)]
        public string SUPPLIERNAME { get; set; }

        [Required(ErrorMessage = "Please enter : 收货单号")]
        [Display(Name = "RECEIPTKEY",Description ="收货单号",Prompt = "收货单号",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(20)]
        public string RECEIPTKEY { get; set; }

        [Display(Name = "POKEY",Description ="订单编号",Prompt = "订单编号",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(30)]
        public string POKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 收货单日期")]
        [Display(Name = "RECEIPTDATE",Description ="收货单日期",Prompt = "收货单日期",ResourceType = typeof(resource.PICKDETAIL))]
        public DateTime RECEIPTDATE { get; set; }

        [Required(ErrorMessage = "Please enter : 栈板数")]
        [Display(Name = "PALLET",Description ="栈板数",Prompt = "栈板数",ResourceType = typeof(resource.PICKDETAIL))]
        public decimal PALLET { get; set; }

        [Display(Name = "PACKKEY",Description ="包装",Prompt = "包装",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(10)]
        public string PACKKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 毛重(KG)")]
        [Display(Name = "GROSSWGT",Description ="毛重(KG)",Prompt = "毛重(KG)",ResourceType = typeof(resource.PICKDETAIL))]
        public decimal GROSSWGT { get; set; }

        [Required(ErrorMessage = "Please enter : 净重(KG)")]
        [Display(Name = "NETWGT",Description ="净重(KG)",Prompt = "净重(KG)",ResourceType = typeof(resource.PICKDETAIL))]
        public decimal NETWGT { get; set; }

        [Required(ErrorMessage = "Please enter : 体积(M)")]
        [Display(Name = "CUBE",Description ="体积(M)",Prompt = "体积(M)",ResourceType = typeof(resource.PICKDETAIL))]
        public decimal CUBE { get; set; }

        [Display(Name = "LOTTABLE1",Description ="工厂名称",Prompt = "工厂名称",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE1 { get; set; }

        [Display(Name = "LOTTABLE2",Description ="公司名称",Prompt = "公司名称",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE2 { get; set; }

        [Display(Name = "LOTTABLE3",Description ="入库外箱标签",Prompt = "入库外箱标签",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE3 { get; set; }

        [Display(Name = "LOTTABLE4",Description ="发货单号",Prompt = "发货单号",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE4 { get; set; }

        [Display(Name = "LOTTABLE5",Description ="出库外箱标签",Prompt = "出库外箱标签",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE5 { get; set; }

        [Display(Name = "LOTTABLE6",Description ="LOTTABLE6",Prompt = "LOTTABLE6",ResourceType = typeof(resource.PICKDETAIL))]
        public decimal LOTTABLE6 { get; set; }

        [Display(Name = "LOTTABLE7",Description ="LOTTABLE7",Prompt = "LOTTABLE7",ResourceType = typeof(resource.PICKDETAIL))]
        public decimal LOTTABLE7 { get; set; }

        [Display(Name = "LOTTABLE8",Description ="LOTTABLE7",Prompt = "LOTTABLE7",ResourceType = typeof(resource.PICKDETAIL))]
        public DateTime LOTTABLE8 { get; set; }

        [Required(ErrorMessage = "Please enter : 出货单ID")]
        [Display(Name = "ORDERID",Description ="出货单ID",Prompt = "出货单ID",ResourceType = typeof(resource.PICKDETAIL))]
        public int ORDERID { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.PICKDETAIL))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.PICKDETAIL))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.PICKDETAIL))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

    }

}
