using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ORDERMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2019/6/27 8:34:19 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ORDERMetadata))]
    public partial class ORDER
    {
    }

    public partial class ORDERMetadata
    {
        [Display(Name = "ORDERDETAILS",Description ="ORDERDETAILS",Prompt = "ORDERDETAILS",ResourceType = typeof(resource.ORDER))]
        public ORDERDETAIL ORDERDETAILS { get; set; }

        [Display(Name = "PICKDETAILS",Description ="PICKDETAILS",Prompt = "PICKDETAILS",ResourceType = typeof(resource.ORDER))]
        public PICKDETAIL PICKDETAILS { get; set; }

        [Required(ErrorMessage = "Please enter : ID")]
        [Display(Name = "ID",Description ="ID",Prompt = "ID",ResourceType = typeof(resource.ORDER))]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter : 出货单号")]
        [Display(Name = "ORDERKEY",Description ="出货单号",Prompt = "出货单号",ResourceType = typeof(resource.ORDER))]
        [MaxLength(20)]
        public string ORDERKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 仓库号")]
        [Display(Name = "WHSEID",Description ="仓库号",Prompt = "仓库号",ResourceType = typeof(resource.ORDER))]
        [MaxLength(20)]
        public string WHSEID { get; set; }

        [Display(Name = "EXTERNORDERKEY",Description ="批次号",Prompt = "批次号",ResourceType = typeof(resource.ORDER))]
        [MaxLength(50)]
        public string EXTERNORDERKEY { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
        [Display(Name = "STATUS",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.ORDER))]
        [MaxLength(3)]
        public string STATUS { get; set; }

        [Required(ErrorMessage = "Please enter : 订单日期")]
        [Display(Name = "ORDERDATE",Description ="订单日期",Prompt = "订单日期",ResourceType = typeof(resource.ORDER))]
        public DateTime ORDERDATE { get; set; }

        [Display(Name = "REQUESTEDSHIPDATE",Description ="配送日期",Prompt = "配送日期",ResourceType = typeof(resource.ORDER))]
        public DateTime REQUESTEDSHIPDATE { get; set; }

        [Display(Name = "DELIVERYDATE",Description ="要求到货时间",Prompt = "要求到货时间",ResourceType = typeof(resource.ORDER))]
        public DateTime DELIVERYDATE { get; set; }

        [Required(ErrorMessage = "Please enter : 货主")]
        [Display(Name = "STORERKEY",Description ="货主",Prompt = "货主",ResourceType = typeof(resource.ORDER))]
        [MaxLength(50)]
        public string STORERKEY { get; set; }

        [Display(Name = "TYPE",Description ="出货方式",Prompt = "出货方式",ResourceType = typeof(resource.ORDER))]
        [MaxLength(10)]
        public string TYPE { get; set; }

        [Display(Name = "SUPPLIERCODE",Description ="供应商代码",Prompt = "供应商代码",ResourceType = typeof(resource.ORDER))]
        [MaxLength(20)]
        public string SUPPLIERCODE { get; set; }

        [Display(Name = "SUPPLIERNAME",Description ="供应商名称",Prompt = "供应商名称",ResourceType = typeof(resource.ORDER))]
        [MaxLength(80)]
        public string SUPPLIERNAME { get; set; }

        [Display(Name = "SUSR2",Description ="配送厂区",Prompt = "配送厂区",ResourceType = typeof(resource.ORDER))]
        [MaxLength(80)]
        public string SUSR2 { get; set; }

        [Display(Name = "CONSIGNEEKEY",Description ="收货单位",Prompt = "收货单位",ResourceType = typeof(resource.ORDER))]
        [MaxLength(30)]
        public string CONSIGNEEKEY { get; set; }

        [Display(Name = "CONSIGNEENAME",Description ="收货单位名称",Prompt = "收货单位名称",ResourceType = typeof(resource.ORDER))]
        [MaxLength(80)]
        public string CONSIGNEENAME { get; set; }

        [Display(Name = "CONSIGNEEADDRESS",Description ="收货地址",Prompt = "收货地址",ResourceType = typeof(resource.ORDER))]
        [MaxLength(80)]
        public string CONSIGNEEADDRESS { get; set; }

        [Display(Name = "CARRIERNAME",Description ="承运人",Prompt = "承运人",ResourceType = typeof(resource.ORDER))]
        [MaxLength(80)]
        public string CARRIERNAME { get; set; }

        [Display(Name = "DRIVERNAME",Description ="司机",Prompt = "司机",ResourceType = typeof(resource.ORDER))]
        [MaxLength(20)]
        public string DRIVERNAME { get; set; }

        [Display(Name = "CARRIERPHONE",Description ="司机电话",Prompt = "司机电话",ResourceType = typeof(resource.ORDER))]
        [MaxLength(20)]
        public string CARRIERPHONE { get; set; }

        [Display(Name = "TRAILERNUMBER",Description ="运输车辆",Prompt = "运输车辆",ResourceType = typeof(resource.ORDER))]
        [MaxLength(80)]
        public string TRAILERNUMBER { get; set; }

        [Display(Name = "ACTUALSHIPDATE",Description ="实际配送日期",Prompt = "实际配送日期",ResourceType = typeof(resource.ORDER))]
        public DateTime ACTUALSHIPDATE { get; set; }

        [Display(Name = "ACTUALDELIVERYDATE",Description ="实际到货时间",Prompt = "实际到货时间",ResourceType = typeof(resource.ORDER))]
        public DateTime ACTUALDELIVERYDATE { get; set; }

        [Display(Name = "CLOSEDDATE",Description ="结案日期",Prompt = "结案日期",ResourceType = typeof(resource.ORDER))]
        public DateTime CLOSEDDATE { get; set; }

        [Required(ErrorMessage = "Please enter : 合计出货数量")]
        [Display(Name = "TOTALQTY",Description ="合计出货数量",Prompt = "合计出货数量",ResourceType = typeof(resource.ORDER))]
        public decimal TOTALQTY { get; set; }

        [Required(ErrorMessage = "Please enter : 合计未出数量")]
        [Display(Name = "OPENQTY",Description ="合计未出数量",Prompt = "合计未出数量",ResourceType = typeof(resource.ORDER))]
        public decimal OPENQTY { get; set; }

        [Display(Name = "TOTALPACKAGE",Description ="合计件数",Prompt = "合计件数",ResourceType = typeof(resource.ORDER))]
        public decimal TOTALPACKAGE { get; set; }

        [Display(Name = "TOTALGROSSWEIGHT",Description ="合计毛重(KG)",Prompt = "合计毛重(KG)",ResourceType = typeof(resource.ORDER))]
        public decimal TOTALGROSSWEIGHT { get; set; }

        [Display(Name = "TOTALVOLUME",Description ="合计体积(M)",Prompt = "合计体积(M)",ResourceType = typeof(resource.ORDER))]
        public decimal TOTALVOLUME { get; set; }

        [Display(Name = "NOTES",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.ORDER))]
        [MaxLength(100)]
        public string NOTES { get; set; }

        [Display(Name = "SUSR1",Description ="上传用户",Prompt = "上传用户",ResourceType = typeof(resource.ORDER))]
        [MaxLength(50)]
        public string SUSR1 { get; set; }

        [Display(Name = "SUSR3",Description ="SUSR3",Prompt = "SUSR3",ResourceType = typeof(resource.ORDER))]
        [MaxLength(80)]
        public string SUSR3 { get; set; }

        [Display(Name = "SUSR4",Description ="SUSR4",Prompt = "SUSR4",ResourceType = typeof(resource.ORDER))]
        [MaxLength(50)]
        public string SUSR4 { get; set; }

        [Display(Name = "SUSR5",Description ="SUSR5",Prompt = "SUSR5",ResourceType = typeof(resource.ORDER))]
        [MaxLength(50)]
        public string SUSR5 { get; set; }

        [Display(Name = "SUSR6",Description ="SUSR6",Prompt = "SUSR6",ResourceType = typeof(resource.ORDER))]
        public decimal SUSR6 { get; set; }

        [Display(Name = "SUSR7",Description ="SUSR7",Prompt = "SUSR7",ResourceType = typeof(resource.ORDER))]
        public decimal SUSR7 { get; set; }

        [Display(Name = "SUSR8",Description ="SUSR8",Prompt = "SUSR8",ResourceType = typeof(resource.ORDER))]
        public DateTime SUSR8 { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.ORDER))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.ORDER))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.ORDER))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.ORDER))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

    }

}
