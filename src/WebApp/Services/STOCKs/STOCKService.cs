using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Newtonsoft.Json;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Service.Pattern;
using WebApp.Models;
using WebApp.Models.ViewModel;
using WebApp.Repositories;

namespace WebApp.Services
{
  /// <summary>
  /// File: STOCKService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2019/6/26 13:33:36
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class STOCKService : Service<STOCK>, ISTOCKService
  {
    private readonly IRepositoryAsync<STOCK> repository;
    private readonly IDataTableImportMappingService mappingservice;
    public STOCKService(IRepositoryAsync<STOCK> repository, IDataTableImportMappingService mappingservice)
        : base(repository)
    {
      this.repository = repository;
      this.mappingservice = mappingservice;
    }



    public void ImportDataTable(System.Data.DataTable datatable)
    {
      var mapping = this.mappingservice.Queryable().Where(x => x.EntitySetName == "STOCK" && ( ( x.IsEnabled == true ) || ( x.IsEnabled == false && !( x.DefaultValue == null || x.DefaultValue.Equals(string.Empty) ) ) )).ToList();
      if (mapping == null || mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到STOCK对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      foreach (DataRow row in datatable.Rows)
      {
        var item = new STOCK();
        var requiredfield = mapping.Where(x => x.IsRequired == true).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null && !row.IsNull(requiredfield) && row[requiredfield] != DBNull.Value && Convert.ToString(row[requiredfield]).Trim() != string.Empty)
        {
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain && !row.IsNull(field.SourceFieldName) && row[field.SourceFieldName] != DBNull.Value && row[field.SourceFieldName].ToString() != string.Empty)
            {
              var stocktype = item.GetType();
              var propertyInfo = stocktype.GetProperty(field.FieldName);
              var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
              var safeValue = ( row[field.SourceFieldName] == null ) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
              propertyInfo.SetValue(item, safeValue, null);
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var stocktype = item.GetType();
              var propertyInfo = stocktype.GetProperty(field.FieldName);
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
      var stocks = this.Query(new STOCKQuery().Withfilter(filters)).OrderBy(n => n.OrderBy(sort, order)).Select().ToList();
      var ignoredColumns = new string[] {
        "ID","ALTSKU","PALLET","PACKKEY","GROSSWGT","NETWGT",
        "CUBE",
        "LOTTABLE1","EXTERNLINENO","LOTTABLE2",
        "LOTTABLE5","LOTTABLE6","LOTTABLE7","LOTTABLE8","LOTTABLE10",
        "LOTTABLE11","LOTTABLE12"
      };
      var datarows = stocks.Select(n => new
      {

        ID = n.ID,
        LOT = n.LOT,
        WHSEID = n.WHSEID,
        EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
        POKEY = n.POKEY,
        STORERKEY = n.STORERKEY,
        RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
        LOC = n.LOC,
        LPN = n.LPN,
        SUPPLIERCODE = n.SUPPLIERCODE,
        SUPPLIERNAME = n.SUPPLIERNAME,
        SKU = n.SKU,
        SKUNAME = n.SKUNAME,
        QTY = n.QTY,
        UMO = n.UMO,
        STATUS = n.STATUS,
        CASECNT = n.CASECNT,
        PALLET = n.PALLET,
        PACKKEY = n.PACKKEY,
        GROSSWGT = n.GROSSWGT,
        NETWGT = n.NETWGT,
        CUBE = n.CUBE,
        RECEIPTKEY = n.RECEIPTKEY,
        ALTSKU = n.ALTSKU,
        INPUTDATETIME = n.INPUTDATETIME.ToString("yyyy/MM/dd HH:mm:ss"),
        EXTERNORDERKEY = n.EXTERNORDERKEY,
        PICKDETAILKEY = n.PICKDETAILKEY,
        ORDERKEY = n.ORDERKEY,
        OUTPUTDATETIME = n.OUTPUTDATETIME?.ToString("yyyy/MM/dd HH:mm:ss"),
        NOTES = n.NOTES,
        LOTTABLE1 = n.LOTTABLE1,
        LOTTABLE2 = n.LOTTABLE2,
        LOTTABLE4 = n.LOTTABLE4,
        LOTTABLE5 = n.LOTTABLE5,
        LOTTABLE6 = n.LOTTABLE6,
        LOTTABLE7 = n.LOTTABLE7,
        LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
        LOTTABLE3 = n.LOTTABLE3,
        LOTTABLE9 = n.LOTTABLE9,
        LOTTABLE10 = n.LOTTABLE10,
        LOTTABLE11 = n.LOTTABLE11,
        LOTTABLE12 = n.LOTTABLE12
      }).ToList();
      var stream = ExcelHelper.ExportExcel(typeof(STOCK), datarows, ignoredColumns);
      //var stream1 = ExcelHelper.ExportExcel(typeof(STOCK), datarows, ignoredColumns);
      //this.sendMail(stream,"new163@163.com");
      return stream;

    }
    private void sendMail(Stream stream,string to) {
      var stmptserver = System.Configuration.ConfigurationManager.AppSettings["smtp"].Split(':')[0];
      var port =Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtp"].Split(':')[1]);
      var user = System.Configuration.ConfigurationManager.AppSettings["smtpuser"];
      var password = System.Configuration.ConfigurationManager.AppSettings["smtppassword"];
      using (var mailClient = new SmtpClient(stmptserver, port))
      using (var message = new MailMessage("28440117@qq.com", to, $"库存报表{DateTime.Now.ToString("MM-dd")}", "请看附件..."))
      {
        stream.Position = 0;     // read from the start of what was written
        var attachment = new Attachment(stream, $"供应商库存报表{DateTime.Now.ToString("yyyy-MM-dd")}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
          ContentType = new ContentType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        };
        message.Attachments.Add(attachment);
        mailClient.EnableSsl = true;
        mailClient.UseDefaultCredentials = true;
        mailClient.Credentials = new NetworkCredential(user, password);
        mailClient.Send(message);
      }

    }
    public void ChangeSate(POSTQCQTY[] data, string state, string name)
    {
      var id = data.Select(x => x.ID);
      var items = this.Queryable().Where(x => id.Contains(x.ID)).ToList();
      foreach (var item in items)
      {
        var qcqty = data.Where(x => x.ID == item.ID).First().PQCQTYINSPECTED;
        if (qcqty > item.QTY)
        {
          qcqty = item.QTY;
        }
        var diffqty = item.QTY - qcqty;
        if (diffqty == 0)
        {
          item.STATUS = state;
          if (state == "105")
          {
            item.PQCQTYREJECTED = 0;
          }
          else
          {
            item.PQCQTYREJECTED = item.QTY;
          }
          item.QTY = qcqty;
          item.PQCQTYINSPECTED = item.QTY;
          item.PQCSTATUS = state;
          item.PQCWHO = name;
          item.PQCDATE = DateTime.Now;
          this.Update(item);
        }
        else
        {
          item.STATUS = state;
          item.QTY = qcqty;
          if (state == "105")
          {
            item.PQCQTYREJECTED = 0;
          }
          else
          {
            item.PQCQTYREJECTED = item.QTY;
          }
          item.PQCQTYINSPECTED = item.QTY;
          item.PQCSTATUS = state;
          item.PQCWHO = name;
          item.PQCDATE = DateTime.Now;
          this.Update(item);

          var newitem = new STOCK()
          {
            ALTSKU = item.ALTSKU,
            CASECNT = item.CASECNT,
            CUBE = item.CUBE,
            EXTERNORDERKEY = item.EXTERNORDERKEY,
            EXTERNRECEIPTKEY = item.EXTERNRECEIPTKEY,
            GROSSWGT = item.GROSSWGT,
            INPUTDATETIME = item.INPUTDATETIME,
            LOC = item.LOC,
            LOT = KeyGenerator.GetNextLotKey(),
            LOTTABLE1 = item.LOTTABLE1,
            LOTTABLE10 = item.LOTTABLE10,
            LOTTABLE11 = item.LOTTABLE11,
            LOTTABLE12 = item.LOTTABLE12,
            LOTTABLE2 = item.LOTTABLE2,
            LOTTABLE3 = item.LOTTABLE3,
            LOTTABLE4 = item.LOTTABLE4,
            LOTTABLE5 = item.LOTTABLE5,
            LOTTABLE6 = item.LOTTABLE6,
            LOTTABLE7 = item.LOTTABLE7,
            LOTTABLE8 = item.LOTTABLE8,
            LOTTABLE9 = item.LOTTABLE9,
            LPN = item.LPN,
            NETWGT = item.NETWGT,
            NOTES = item.NOTES,
            ORDERKEY = item.ORDERKEY,
            PACKKEY = item.PACKKEY,
            OUTPUTDATETIME = item.OUTPUTDATETIME,
            PALLET = item.PALLET,
            PICKDETAILKEY = item.PICKDETAILKEY,
            POKEY = item.POKEY,
            QTY = diffqty,
            RECEIPTDATE = item.RECEIPTDATE,
            RECEIPTKEY = item.RECEIPTKEY,
            SKU = item.SKU,
            SKUNAME = item.SKUNAME,
            STATUS = "103",
            STORERKEY = item.STORERKEY,
            SUPPLIERCODE = item.SUPPLIERCODE,
            SUPPLIERNAME = item.SUPPLIERNAME,
            UMO = item.UMO,
            WHSEID = item.WHSEID


          };
          this.Insert(newitem);
        }
      }
    }
  }
}



