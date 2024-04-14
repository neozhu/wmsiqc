using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Repository.Pattern.Infrastructure;
using Service.Pattern;
using WebApp.Models;
using WebApp.Repositories;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
namespace WebApp.Services
{
/// <summary>
/// File: PICKDETAILService.cs
/// Purpose: Within the service layer, you define and implement 
/// the service interface and the data contracts (or message types).
/// One of the more important concepts to keep in mind is that a service
/// should never expose details of the internal processes or 
/// the business entities used within the application. 
/// Created Date: 2019/6/27 8:22:34
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public class PICKDETAILService : Service< PICKDETAIL >, IPICKDETAILService
    {
        private readonly IRepositoryAsync<PICKDETAIL> repository;
		private readonly IDataTableImportMappingService mappingservice;
        public  PICKDETAILService(IRepositoryAsync< PICKDETAIL> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            this.repository=repository;
			this.mappingservice = mappingservice;
        }
                 public  IEnumerable<PICKDETAIL> GetByORDERID(int  orderid) => repository.GetByORDERID(orderid);
                   
        
        		 
                private int getORDERIDByORDERKEY(string orderkey)
        {
            var orderRepository = this.repository.GetRepository<ORDER>();
            var order = orderRepository.Queryable().Where(x => x.ORDERKEY == orderkey).FirstOrDefault();
            if (order == null)
            {
                throw new Exception("not found ForeignKey:ORDERID with " + orderkey);
            }
            else
            {
                return order.ID;
            }
        }
                public void ImportDataTable(System.Data.DataTable datatable)
        {
            var mapping = this.mappingservice.Queryable().Where(x => x.EntitySetName == "PICKDETAIL" && ( ( x.IsEnabled == true ) || ( x.IsEnabled == false && !( x.DefaultValue == null || x.DefaultValue.Equals(string.Empty) ) ) )).ToList();
            if (mapping == null || mapping.Count == 0)
            {
                throw new KeyNotFoundException("没有找到PICKDETAIL对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
            }
            foreach (DataRow row in datatable.Rows)
            {
                var item = new PICKDETAIL();
                var requiredfield = mapping.Where(x => x.IsRequired == true).FirstOrDefault()?.SourceFieldName;
                if (requiredfield != null && !row.IsNull(requiredfield) &&  row[requiredfield] != DBNull.Value && Convert.ToString(row[requiredfield]).Trim() != string.Empty)
                {
                    foreach (var field in mapping)
                    {
						var defval = field.DefaultValue;
						var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
						if (contain && !row.IsNull(field.SourceFieldName) && row[field.SourceFieldName] != DBNull.Value && row[field.SourceFieldName].ToString()!=string.Empty )
						{
                            var pickdetailtype = item.GetType();
							var propertyInfo = pickdetailtype.GetProperty(field.FieldName);
                                                        //关联外键查询获取Id
                            switch (field.FieldName) {
                                                                 case "ORDERID":
                                     var orderkey =  row[field.SourceFieldName].ToString();
                                     var orderid = this.getORDERIDByORDERKEY(orderkey);
                                     propertyInfo.SetValue(item, Convert.ChangeType(orderid, propertyInfo.PropertyType), null);
                                     break;
                                                                default:
                                    var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                    var safeValue = (row[field.SourceFieldName] == null) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
                                    propertyInfo.SetValue(item, safeValue, null);
                                    break;
                            }
                                                    }
						else if (!string.IsNullOrEmpty(defval))
						{
							var pickdetailtype = item.GetType();
							var propertyInfo = pickdetailtype.GetProperty(field.FieldName);
							if (string.Equals(defval, "now", StringComparison.OrdinalIgnoreCase) && (propertyInfo.PropertyType ==typeof(DateTime) || propertyInfo.PropertyType == typeof(Nullable<DateTime>)))
                            {
                                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                var safeValue = Convert.ChangeType(DateTime.Now, safetype);
                                propertyInfo.SetValue(item, safeValue, null);
                            }
                            else if(string.Equals(defval, "guid", StringComparison.OrdinalIgnoreCase))
                            {
                                propertyInfo.SetValue(item, Guid.NewGuid().ToString(), null);
                            }
                            else
                            {
                                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                var safeValue = Convert.ChangeType(defval, safetype);
                                propertyInfo.SetValue(item, safeValue, null);
                            }
						}
                    }
                    this.Insert(item);
               }
            }
        }
				public Stream ExportExcel(string filterRules = "",string sort = "ID", string order = "asc")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
                                   var pickdetails  = this.Query(new PICKDETAILQuery().Withfilter(filters)).Include(p => p.ORDER).OrderBy(n=>n.OrderBy(sort,order)).Select().ToList();
            
                        var datarows = pickdetails .Select(  n => new { 

    ORDERORDERKEY = n.ORDER?.ORDERKEY,
    ID = n.ID,
    PICKDETAILKEY = n.PICKDETAILKEY,
    WHSEID = n.WHSEID,
    ORDERKEY = n.ORDERKEY,
    EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
    STORERKEY = n.STORERKEY,
    STATUS = n.STATUS,
    SKU = n.SKU,
    SKUNAME = n.SKUNAME,
    QTY = n.QTY,
    UMO = n.UMO,
    CASECNT = n.CASECNT,
    LOC = n.LOC,
    LOT = n.LOT,
    LPN = n.LPN,
    SUPPLIERCODE = n.SUPPLIERCODE,
    SUPPLIERNAME = n.SUPPLIERNAME,
    RECEIPTKEY = n.RECEIPTKEY,
    POKEY = n.POKEY,
    RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
    PALLET = n.PALLET,
    PACKKEY = n.PACKKEY,
    GROSSWGT = n.GROSSWGT,
    NETWGT = n.NETWGT,
    CUBE = n.CUBE,
    LOTTABLE1 = n.LOTTABLE1,
    LOTTABLE2 = n.LOTTABLE2,
    LOTTABLE3 = n.LOTTABLE3,
    LOTTABLE4 = n.LOTTABLE4,
    LOTTABLE5 = n.LOTTABLE5,
    LOTTABLE6 = n.LOTTABLE6,
    LOTTABLE7 = n.LOTTABLE7,
    LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
    ORDERID = n.ORDERID
}).ToList();
            return ExcelHelper.ExportExcel(typeof(PICKDETAIL), datarows);
        }
    }
}



