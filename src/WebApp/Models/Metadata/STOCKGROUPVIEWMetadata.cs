using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="STOCKGROUPVIEWMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2019/7/9 7:29:49 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(STOCKGROUPVIEWMetadata))]
    public partial class STOCKGROUPVIEW
    {
    }

    public partial class STOCKGROUPVIEWMetadata
    {
        [Required(ErrorMessage = "Please enter : RID")]
        [Display(Name = "RID",Description ="RID",Prompt = "RID",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        public int RID { get; set; }

        [Display(Name = "SUPPLIERCODE",Description ="供应商代码",Prompt = "供应商代码",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        [MaxLength(20)]
        public string SUPPLIERCODE { get; set; }

        [Display(Name = "SUPPLIERNAME",Description ="供应商名称",Prompt = "供应商名称",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        [MaxLength(80)]
        public string SUPPLIERNAME { get; set; }

        [Required(ErrorMessage = "Please enter : 物料编号")]
        [Display(Name = "SKU",Description ="物料编号",Prompt = "物料编号",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        [MaxLength(30)]
        public string SKU { get; set; }

        [Display(Name = "SKUNAME",Description ="物料名称",Prompt = "物料名称",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        [MaxLength(80)]
        public string SKUNAME { get; set; }

        [Required(ErrorMessage = "Please enter : 数量")]
        [Display(Name = "TOTAL",Description ="数量",Prompt = "数量",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        public decimal TOTAL { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
        [Display(Name = "STATUS",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        [MaxLength(10)]
        public string STATUS { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.STOCKGROUPVIEW))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

    }

}
