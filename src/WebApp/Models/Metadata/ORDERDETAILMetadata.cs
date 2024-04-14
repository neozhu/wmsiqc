using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ORDERDETAILMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2019/6/27 8:28:17 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ORDERDETAILMetadata))]
    public partial class ORDERDETAIL
    {
    }

    public partial class ORDERDETAILMetadata
    {
        [Display(Name = "ORDER",Description ="出货单信息",Prompt = "出货单信息",ResourceType = typeof(resource.ORDERDETAIL))]
        public ORDER ORDER { get; set; }

        [Required(ErrorMessage = "Please enter : ID")]
        [Display(Name = "ID",Description ="ID",Prompt = "ID",ResourceType = typeof(resource.ORDERDETAIL))]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter : 出货单号")]
        [Display(Name = "ORDERKEY",Description ="出货单号",Prompt = "出货单号",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(20)]
        public string ORDERKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 仓库号")]
        [Display(Name = "WHSEID",Description ="仓库号",Prompt = "仓库号",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(20)]
        public string WHSEID { get; set; }

        [Required(ErrorMessage = "Please enter : 货主")]
        [Display(Name = "STORERKEY",Description ="货主",Prompt = "货主",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(50)]
        public string STORERKEY { get; set; }

        [Display(Name = "EXTERNORDERKEY",Description ="批次号",Prompt = "批次号",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(50)]
        public string EXTERNORDERKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 行号")]
        [Display(Name = "ORDERLINENUMBER",Description ="行号",Prompt = "行号",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(6)]
        public string ORDERLINENUMBER { get; set; }

        [Display(Name = "STATUS",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(3)]
        public string STATUS { get; set; }

        [Display(Name = "LOTTABLE2",Description ="配送厂区",Prompt = "配送厂区",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE2 { get; set; }

        [Display(Name = "SUPPLIERCODE",Description ="供应商代码",Prompt = "供应商代码",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(20)]
        public string SUPPLIERCODE { get; set; }

        [Display(Name = "SUPPLIERNAME",Description ="供应商名称",Prompt = "供应商名称",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(80)]
        public string SUPPLIERNAME { get; set; }

        [Required(ErrorMessage = "Please enter : 供应商物料编号")]
        [Display(Name = "SKU",Description ="供应商物料编号",Prompt = "供应商物料编号",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(30)]
        public string SKU { get; set; }

        [Display(Name = "SKUNAME",Description ="供应商物料名称",Prompt = "供应商物料名称",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(80)]
        public string SKUNAME { get; set; }

        [Display(Name = "ALTSKU",Description ="第三方物料编号",Prompt = "第三方物料编号",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(30)]
        public string ALTSKU { get; set; }

        [Required(ErrorMessage = "Please enter : 配送数量")]
        [Display(Name = "ORIGINALQTY",Description ="配送数量",Prompt = "配送数量",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal ORIGINALQTY { get; set; }

        [Required(ErrorMessage = "Please enter : 单位")]
        [Display(Name = "UMO",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(10)]
        public string UMO { get; set; }

        [Required(ErrorMessage = "Please enter : 配送件数")]
        [Display(Name = "CASECNT",Description ="配送件数",Prompt = "配送件数",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal CASECNT { get; set; }

        [Required(ErrorMessage = "Please enter : 未出货数量")]
        [Display(Name = "OPENQTY",Description ="未出货数量",Prompt = "未出货数量",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal OPENQTY { get; set; }

        [Required(ErrorMessage = "Please enter : 已发运数量")]
        [Display(Name = "SHIPPEDQTY",Description ="已发运数量",Prompt = "已发运数量",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal SHIPPEDQTY { get; set; }

        [Required(ErrorMessage = "Please enter : 已捡货数量")]
        [Display(Name = "QTYPICKED",Description ="已捡货数量",Prompt = "已捡货数量",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal QTYPICKED { get; set; }

        [Required(ErrorMessage = "Please enter : 栈板数")]
        [Display(Name = "PALLET",Description ="栈板数",Prompt = "栈板数",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal PALLET { get; set; }

        [Display(Name = "PACKKEY",Description ="包装",Prompt = "包装",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(10)]
        public string PACKKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 毛重(KG)")]
        [Display(Name = "GROSSWGT",Description ="毛重(KG)",Prompt = "毛重(KG)",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal GROSSWGT { get; set; }

        [Required(ErrorMessage = "Please enter : 净重(KG)")]
        [Display(Name = "NETWGT",Description ="净重(KG)",Prompt = "净重(KG)",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal NETWGT { get; set; }

        [Required(ErrorMessage = "Please enter : 体积(M)")]
        [Display(Name = "CUBE",Description ="体积(M)",Prompt = "体积(M)",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal CUBE { get; set; }

        [Display(Name = "NOTES",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(100)]
        public string NOTES { get; set; }

        [Required(ErrorMessage = "Please enter : QC数量")]
        [Display(Name = "PQCQTYINSPECTED",Description ="QC数量",Prompt = "QC数量",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal PQCQTYINSPECTED { get; set; }

        [Required(ErrorMessage = "Please enter : QC不良数")]
        [Display(Name = "PQCQTYREJECTED",Description ="QC不良数",Prompt = "QC不良数",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal PQCQTYREJECTED { get; set; }

        [Display(Name = "PQCSTATUS",Description ="QC状态",Prompt = "QC状态",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(10)]
        public string PQCSTATUS { get; set; }

        [Display(Name = "PQCWHO",Description ="QC人员",Prompt = "QC人员",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(10)]
        public string PQCWHO { get; set; }

        [Display(Name = "PQCDATE",Description ="QC日期",Prompt = "QC日期",ResourceType = typeof(resource.ORDERDETAIL))]
        public DateTime PQCDATE { get; set; }

        [Display(Name = "LOTTABLE3",Description ="外箱标签",Prompt = "外箱标签",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE3 { get; set; }

        [Display(Name = "LOTTABLE1",Description ="上传用户",Prompt = "上传用户",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE1 { get; set; }

        [Display(Name = "REQUESTEDSHIPDATE",Description ="配送日期",Prompt = "配送日期",ResourceType = typeof(resource.ORDERDETAIL))]
        public DateTime REQUESTEDSHIPDATE { get; set; }

        [Display(Name = "DELIVERYDATE",Description ="要求到货时间",Prompt = "要求到货时间",ResourceType = typeof(resource.ORDERDETAIL))]
        public DateTime DELIVERYDATE { get; set; }

        [Display(Name = "EXTERNLINENO",Description ="外部行号",Prompt = "外部行号",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(20)]
        public string EXTERNLINENO { get; set; }

        [Display(Name = "LOTTABLE4",Description ="LOTTABLE4",Prompt = "LOTTABLE4",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE4 { get; set; }

        [Display(Name = "LOTTABLE5",Description ="LOTTABLE5",Prompt = "LOTTABLE5",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(50)]
        public string LOTTABLE5 { get; set; }

        [Display(Name = "LOTTABLE6",Description ="LOTTABLE6",Prompt = "LOTTABLE6",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal LOTTABLE6 { get; set; }

        [Display(Name = "LOTTABLE7",Description ="LOTTABLE7",Prompt = "LOTTABLE7",ResourceType = typeof(resource.ORDERDETAIL))]
        public decimal LOTTABLE7 { get; set; }

        [Display(Name = "LOTTABLE8",Description ="LOTTABLE7",Prompt = "LOTTABLE7",ResourceType = typeof(resource.ORDERDETAIL))]
        public DateTime LOTTABLE8 { get; set; }

        [Required(ErrorMessage = "Please enter : 出货单ID")]
        [Display(Name = "ORDERID",Description ="出货单ID",Prompt = "出货单ID",ResourceType = typeof(resource.ORDERDETAIL))]
        public int ORDERID { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.ORDERDETAIL))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.ORDERDETAIL))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.ORDERDETAIL))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

    }

}
