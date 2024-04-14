using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Service.Pattern;
using WebApp.Models;
using WebApp.Repositories;
namespace WebApp.Services
{
  /// <summary>
  /// File: RECEIPTDETAILService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2019/6/25 7:28:47
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class RECEIPTDETAILService : Service<RECEIPTDETAIL>, IRECEIPTDETAILService
  {
    private readonly IRepositoryAsync<RECEIPTDETAIL> repository;
    private readonly IDataTableImportMappingService mappingservice;
    public RECEIPTDETAILService(IRepositoryAsync<RECEIPTDETAIL> repository, IDataTableImportMappingService mappingservice)
        : base(repository)
    {
      this.repository = repository;
      this.mappingservice = mappingservice;
    }
    public IEnumerable<RECEIPTDETAIL> GetByRECEIPTID(int receiptid) => this.repository.GetByRECEIPTID(receiptid);



    private int getRECEIPTIDByRECEIPTKEY(string receiptkey)
    {
      var receiptRepository = this.repository.GetRepository<RECEIPT>();
      var receipt = receiptRepository.Queryable().Where(x => x.RECEIPTKEY == receiptkey).FirstOrDefault();
      if (receipt == null)
      {
        throw new Exception("not found ForeignKey:RECEIPTID with " + receiptkey);
      }
      else
      {
        return receipt.ID;
      }
    }
    public void ImportDataTable(System.Data.DataTable datatable)
    {
      var mapping = this.mappingservice.Queryable().Where(x => x.EntitySetName == "RECEIPTDETAIL" && ( ( x.IsEnabled == true ) || ( x.IsEnabled == false && !( x.DefaultValue == null || x.DefaultValue.Equals(string.Empty) ) ) )).ToList();
      if (mapping == null || mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到RECEIPTDETAIL对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      foreach (DataRow row in datatable.Rows)
      {
        var item = new RECEIPTDETAIL();
        var requiredfield = mapping.Where(x => x.IsRequired == true).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null && !row.IsNull(requiredfield) && row[requiredfield] != DBNull.Value && Convert.ToString(row[requiredfield]).Trim() != string.Empty)
        {
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain && !row.IsNull(field.SourceFieldName) && row[field.SourceFieldName] != DBNull.Value && row[field.SourceFieldName].ToString() != string.Empty)
            {
              var receiptdetailtype = item.GetType();
              var propertyInfo = receiptdetailtype.GetProperty(field.FieldName);
              //关联外键查询获取Id
              switch (field.FieldName)
              {
                case "RECEIPTID":
                  var receiptkey = row[field.SourceFieldName].ToString();
                  var receiptid = this.getRECEIPTIDByRECEIPTKEY(receiptkey);
                  propertyInfo.SetValue(item, Convert.ChangeType(receiptid, propertyInfo.PropertyType), null);
                  break;
                default:
                  var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                  var safeValue = ( row[field.SourceFieldName] == null ) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
                  propertyInfo.SetValue(item, safeValue, null);
                  break;
              }
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var receiptdetailtype = item.GetType();
              var propertyInfo = receiptdetailtype.GetProperty(field.FieldName);
              if (string.Equals(defval, "now", StringComparison.OrdinalIgnoreCase) && ( propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(Nullable<DateTime>) ))
              {
                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = Convert.ChangeType(DateTime.Now, safetype);
                propertyInfo.SetValue(item, safeValue, null);
              }
              else if (string.Equals(defval, "guid", StringComparison.OrdinalIgnoreCase))
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
    public Stream ExportExcel(string filterRules = "", string sort = "ID", string order = "asc")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var receiptdetails = this.Query(new RECEIPTDETAILQuery().Withfilter(filters)).Include(p => p.RECEIPT).OrderBy(n => n.OrderBy(sort, order)).Select().ToList();
      var ignoredColumns = new string[] {
        "RECEIPTRECEIPTKEY","ID", "LOTTABLE2", "ALTSKU","PALLET","QTYADJUSTED",
        "QTYREJECTED","PACKKEY","GROSSWGT","NETWGT","CUBE","PQCQTYINSPECTED",
        "PQCQTYREJECTED","PQCQTYREJECTED","PQCSTATUS","PQCWHO","PQCDATE",
        "CONDITIONCODE","TOLOC","TOLOT","TOLPN","EXTERNLINENO","LOTTABLE4",
        "LOTTABLE5","LOTTABLE6","LOTTABLE7","LOTTABLE8","RECEIPTID"
      };
      var datarows = receiptdetails.Select(n => new
      {

        RECEIPTRECEIPTKEY = n.RECEIPT?.RECEIPTKEY,
        ID = n.ID,
        RECEIPTKEY = n.RECEIPTKEY,
        EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
        POKEY = n.POKEY,
        RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
        WHSEID = n.WHSEID,
        LOTTABLE1 = n.LOTTABLE1,
        LOTTABLE2 = n.LOTTABLE2,
        STORERKEY = n.STORERKEY,
        RECEIPTLINENUMBER = n.RECEIPTLINENUMBER,
        SKU = n.SKU,
        SKUNAME = n.SKUNAME,
        ALTSKU = n.ALTSKU,
        STATUS = n.STATUS,
        SUPPLIERCODE = n.SUPPLIERCODE,
        SUPPLIERNAME = n.SUPPLIERNAME,
        DATERECEIVED = n.DATERECEIVED?.ToString("yyyy/MM/dd HH:mm:ss"),
        QTYEXPECTED = n.QTYEXPECTED,
        QTYRECEIVED = n.QTYRECEIVED,
        CASECNT = n.CASECNT,
        PALLET = n.PALLET,
        QTYADJUSTED = n.QTYADJUSTED,
        QTYREJECTED = n.QTYREJECTED,
        UMO = n.UMO,
        PACKKEY = n.PACKKEY,
        GROSSWGT = n.GROSSWGT,
        NETWGT = n.NETWGT,
        CUBE = n.CUBE,
        NOTES = n.NOTES,
        LOTTABLE3 = n.LOTTABLE3,
        PQCQTYINSPECTED = n.PQCQTYINSPECTED,
        PQCQTYREJECTED = n.PQCQTYREJECTED,
        PQCSTATUS = n.PQCSTATUS,
        PQCWHO = n.PQCWHO,
        PQCDATE = n.PQCDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
        CONDITIONCODE = n.CONDITIONCODE,
        TOLOC = n.TOLOC,
        TOLOT = n.TOLOT,
        TOLPN = n.TOLPN,
        EXTERNLINENO = n.EXTERNLINENO,
        LOTTABLE4 = n.LOTTABLE4,
        LOTTABLE5 = n.LOTTABLE5,
        LOTTABLE6 = n.LOTTABLE6,
        LOTTABLE7 = n.LOTTABLE7,
        LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
        RECEIPTID = n.RECEIPTID
      }).ToList();
      return ExcelHelper.ExportExcel(typeof(RECEIPTDETAIL), datarows,ignoredColumns);
    }
  }
}



