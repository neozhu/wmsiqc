using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
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
  /// File: RECEIPTService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2019/6/25 7:35:53
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class RECEIPTService : Service<RECEIPT>, IRECEIPTService
  {
    private readonly IRepositoryAsync<RECEIPT> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly IRECEIPTDETAILService detailservice;
    private readonly ISTOCKService stockservice;
    private readonly IMessageService messageService;
    public RECEIPTService(
      IMessageService messageService,
      ISTOCKService stockservice,
      IRECEIPTDETAILService detailservice,
      IRepositoryAsync<RECEIPT> repository, IDataTableImportMappingService mappingservice)
        : base(repository)
    {
      this.messageService = messageService;
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.detailservice = detailservice;
      this.stockservice = stockservice;
    }
    public IEnumerable<RECEIPTDETAIL> GetRECEIPTDETAILSByRECEIPTID(int receiptid) => this.repository.GetRECEIPTDETAILSByRECEIPTID(receiptid);



    public void ImportDataTable(System.Data.DataTable datatable)
    {
      var mapping = this.mappingservice.Queryable().Where(x => x.EntitySetName == "RECEIPT" && ( ( x.IsEnabled == true ) || ( x.IsEnabled == false && !( x.DefaultValue == null || x.DefaultValue.Equals(string.Empty) ) ) )).ToList();
      if (mapping == null || mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到RECEIPT对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      foreach (DataRow row in datatable.Rows)
      {
        var item = new RECEIPT();
        var requiredfield = mapping.Where(x => x.IsRequired == true).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null && !row.IsNull(requiredfield) && row[requiredfield] != DBNull.Value && Convert.ToString(row[requiredfield]).Trim() != string.Empty)
        {
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain && !row.IsNull(field.SourceFieldName) && row[field.SourceFieldName] != DBNull.Value && row[field.SourceFieldName].ToString() != string.Empty)
            {
              var receipttype = item.GetType();
              var propertyInfo = receipttype.GetProperty(field.FieldName);
              var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
              var safeValue = ( row[field.SourceFieldName] == null ) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
              propertyInfo.SetValue(item, safeValue, null);
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var receipttype = item.GetType();
              var propertyInfo = receipttype.GetProperty(field.FieldName);
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
      var receipts = this.Query(new RECEIPTQuery().Withfilter(filters)).OrderBy(n => n.OrderBy(sort, order)).Select().ToList();
      var datarows = receipts.Select(n => new
      {

        RECEIPTDETAILS = n.RECEIPTDETAILS,
        ID = n.ID,
        RECEIPTKEY = n.RECEIPTKEY,
        WHSEID = n.WHSEID,
        EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
        POKEY = n.POKEY,
        SUSR2 = n.SUSR2,
        SUSR3 = n.SUSR3,
        STORERKEY = n.STORERKEY,
        TYPE = n.TYPE,
        STATUS = n.STATUS,
        RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
        ADVICEDATE = n.ADVICEDATE.ToString("yyyy/MM/dd HH:mm:ss"),
        EXPECTEDRECEIPTDATE = n.EXPECTEDRECEIPTDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
        ARRIVALDATETIME = n.ARRIVALDATETIME?.ToString("yyyy/MM/dd HH:mm:ss"),
        CLOSEDDATE = n.CLOSEDDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
        SUPPLIERCODE = n.SUPPLIERCODE,
        SUPPLIERNAME = n.SUPPLIERNAME,
        CARRIERKEY = n.CARRIERKEY,
        CARRIERNAME = n.CARRIERNAME,
        TOTALQTY = n.TOTALQTY,
        OPENQTY = n.OPENQTY,
        TOTALPACKAGE = n.TOTALPACKAGE,
        TOTALGROSSWEIGHT = n.TOTALGROSSWEIGHT,
        TOTALVOLUME = n.TOTALVOLUME,
        NOTES = n.NOTES,
        SUSR1 = n.SUSR1,
        SUSR4 = n.SUSR4,
        SUSR5 = n.SUSR5,
        SUSR6 = n.SUSR6,
        SUSR7 = n.SUSR7,
        SUSR8 = n.SUSR8?.ToString("yyyy/MM/dd HH:mm:ss")
      }).ToList();
      return ExcelHelper.ExportExcel(typeof(RECEIPT), datarows);
    }

    public void ImportExcel(DataTable datatable, string user)
    {
      var mapping1 = this.mappingservice.Queryable().Where(x => x.EntitySetName == "RECEIPT" && ( ( x.IsEnabled == true ) || ( x.IsEnabled == false && !( x.DefaultValue == null || x.DefaultValue.Equals(string.Empty) ) ) )).ToList();
      var mapping2 = this.mappingservice.Queryable().Where(x => x.EntitySetName == "RECEIPTDETAIL" && ( ( x.IsEnabled == true ) || ( x.IsEnabled == false && !( x.DefaultValue == null || x.DefaultValue.Equals(string.Empty) ) ) )).ToList();
      var detaillist = new List<RECEIPTDETAIL>();
      foreach (DataRow row in datatable.Rows)
      {
        var item = new RECEIPTDETAIL();
        var requiredfield = mapping2.Where(x => x.IsRequired == true).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null && !row.IsNull(requiredfield) && row[requiredfield] != DBNull.Value && Convert.ToString(row[requiredfield]).Trim() != string.Empty)
        {
          foreach (var field in mapping2)
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
                  var receiptid = "";
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
          detaillist.Add(item);
        }

      }

      if (detaillist.Count > 0)
      {
        var lot = detaillist.GroupBy(x => new {x.SUPPLIERCODE, x.EXTERNRECEIPTKEY, x.RECEIPTDATE })
                  .Select(x => new { x.Key, ITEMS = x.ToList() });

        foreach (var g in lot)
        {
          var exkey = g.Key.EXTERNRECEIPTKEY;
          var dt = g.Key.RECEIPTDATE;
          var suppliercode = g.Key.SUPPLIERCODE;
          var exist = this.Queryable().Where(x => x.EXTERNRECEIPTKEY == exkey && x.RECEIPTDATE == dt).Any();
          if (exist)
          {
            throw new Exception($"该批次[{exkey}]-[{dt.ToString("yyyy-MM-dd")}]已经导入!");
          }
          var receiptkey = KeyGenerator.GetNextReceiptKey();
          var head = new RECEIPT();
          head.RECEIPTKEY = receiptkey;
          head.RECEIPTDATE = dt;
          head.EXTERNRECEIPTKEY = exkey;
          head.ADVICEDATE = dt;
          head.EXPECTEDRECEIPTDATE = dt.AddHours(3);
          head.NOTES = g.ITEMS.First().NOTES;
          head.OPENQTY = g.ITEMS.Sum(x => x.QTYEXPECTED);
          head.TOTALQTY = g.ITEMS.Sum(x => x.QTYEXPECTED);
          head.POKEY = g.ITEMS.First().POKEY;
          head.SUPPLIERCODE = g.ITEMS.First().SUPPLIERCODE;
          head.SUPPLIERNAME = g.ITEMS.First().SUPPLIERNAME;
          head.SUSR1 = user;
          head.SUSR2 = g.ITEMS.First().LOTTABLE2;
          head.SUSR3 = g.ITEMS.First().LOTTABLE1;
          head.SUSR4 = g.ITEMS.First().LOTTABLE4;
          head.TOTALGROSSWEIGHT = g.ITEMS.Sum(x => x.GROSSWGT);
          head.TOTALPACKAGE = g.ITEMS.Sum(x => x.CASECNT);
          head.TOTALQTY = g.ITEMS.Sum(x => x.QTYEXPECTED);
          head.TOTALVOLUME = g.ITEMS.Sum(x => x.CUBE);
          head.TYPE = ( g.ITEMS.First().LOTTABLE5 == "入库" ? "1" : "2" );
          head.WHSEID = g.ITEMS.First().WHSEID;

          head.STORERKEY = g.ITEMS.First().STORERKEY;
          head.STATUS = g.ITEMS.First().STATUS;
          head.TrackingState = TrackableEntities.TrackingState.Added;
          var line = 0;
          foreach (var item in g.ITEMS)
          {
            ++line;
            item.RECEIPTLINENUMBER = line.ToString("000");
            item.RECEIPTKEY = head.RECEIPTKEY;
            item.TrackingState = TrackableEntities.TrackingState.Added;
            item.RECEIPTID = head.ID;
            head.RECEIPTDETAILS.Add(item);
          }

          this.ApplyChanges(head);



        }

      }



    }
    //扫描入库
    public int ScanInput(int id,string barcode, decimal qty, string name)
    {
      var method = "入库扫描";
      var array = barcode.Split(new string[] { "——" },StringSplitOptions.RemoveEmptyEntries);
      if (array.Length == 4)
      {
        var receiptkey = "";
        var pokey = "";
        var suppliercode = array[0];
        var dt = array[1];
        var sku = array[2];
        var no = array[3];
        var date = this.getdate(dt);
        var item = this.detailservice.Queryable()
          .Where(x => x.RECEIPTID==id && x.SUPPLIERCODE == suppliercode
          && x.STATUS == "100"
          && x.SKU == sku
          && DbFunctions.TruncateTime(x.RECEIPTDATE) == date).FirstOrDefault();

        if (item == null)
        {
          this.messageService.AddLog(MessageType.Error, method, id.ToString(), sku, $"条码[{barcode}]没有匹配得记录", name);
          new Exception($"条码[{barcode}]没有匹配得记录");
          
        }
        else
        {
          receiptkey = item.RECEIPTKEY;
          pokey = item.POKEY;
          var now = DateTime.Now;
          var tolot = KeyGenerator.GetNextLotKey();
          item.STATUS = "100";
          item.QTYRECEIVED = 0;
          item.DATERECEIVED = null;
          item.TOLOT = null;
          item.TOLOC = null;
          item.LOTTABLE3 = barcode;
          this.detailservice.Update(item);
          //添加库存
          //var stock = new STOCK();
          //stock.CASECNT = item.CASECNT;
          //stock.CUBE = item.CUBE;
          //stock.EXTERNORDERKEY = null;
          //stock.EXTERNRECEIPTKEY = item.EXTERNRECEIPTKEY;
          //stock.GROSSWGT = item.GROSSWGT;
          //stock.INPUTDATETIME = DateTime.Now;
          //stock.LOC = "STAGE";
          //stock.LOT = tolot;
          //stock.LOTTABLE1 = item.LOTTABLE1;
          //stock.LOTTABLE2 = item.LOTTABLE2;
          //stock.LOTTABLE3 = item.LOTTABLE3;
          //stock.LOTTABLE4 = item.LOTTABLE4;
          //stock.LOTTABLE5 = item.LOTTABLE5;
          //stock.LOTTABLE6 = item.LOTTABLE6;
          //stock.LOTTABLE7 = item.LOTTABLE7;
          //stock.LOTTABLE8 = item.LOTTABLE8;
          //stock.LPN = item.TOLPN;
          //stock.NETWGT = item.NETWGT;
          //stock.NOTES = item.NOTES;
          //stock.PACKKEY = item.PACKKEY;
          //stock.PALLET = item.PALLET;
          //stock.POKEY = item.POKEY;
          //stock.QTY = item.QTYRECEIVED;
          //stock.RECEIPTDATE = item.RECEIPTDATE;
          //stock.RECEIPTKEY = item.RECEIPTKEY;
          //stock.SKU = item.SKU;
          //stock.SKUNAME = item.SKUNAME;
          //stock.STATUS = item.STATUS;
          //stock.STORERKEY = item.STORERKEY;
          //stock.SUPPLIERCODE = item.SUPPLIERCODE;
          //stock.SUPPLIERNAME = item.SUPPLIERNAME;
          //stock.UMO = item.UMO;
          //stock.WHSEID = item.WHSEID;
          //stock.ALTSKU = item.ALTSKU;
          //this.stockservice.Insert(stock);
          this.messageService.AddLog(MessageType.Information, method, receiptkey, pokey, $"{item.SUPPLIERCODE},{item.SKU},{item.QTYEXPECTED} 条码[{barcode}]比对成功", name);
          return item.ID;
        }
        return 0;
      }
      else
      {
        this.messageService.AddLog(MessageType.Error, method, id.ToString(), barcode, $"条码[{barcode}]条码长度不符合", name);
        throw new Exception("条码长度不符合");
        
      }

    }
    private DateTime getdate(string dt)
    {
      var array = dt.ToCharArray();
      if (array.Length == 3)
      {
        var year = Convert.ToInt32("201" + array[0]);
        var m = 0;
        if ((int)array[1] > 55)
        {
          m = ( (int)array[1] ) - 55;
        }
        else
        {
          m = array[1] - 48;
        }
        var d = 0;
        if ((int)array[2] > 55)
        {
          d = ( (int)array[2] ) - 55;
        }
        else
        {
          d = array[2] - 48;
        }

        return new DateTime(year, m, d);
      }
      else
      {
        throw new Exception("日期标签符合和格式,例:[9BK]");
      }
    }
    //取消入库
    public void Undo(int id, string name)
    {
      var method = "取消入库";
      var receiptkey = "";
      var pokey = "";
      var tolot = "";
      var item = this.detailservice.Queryable().Where(x => x.ID == id).FirstOrDefault();
      if (item != null)
      {
        tolot = item.TOLOT;
        item.STATUS = "100";
        item.QTYRECEIVED = 0;
        item.DATERECEIVED = null;
        item.TOLOT = null;
        item.TOLOC = null;
        item.LOTTABLE3 = null;
        this.detailservice.Update(item);
        receiptkey = item.RECEIPTKEY;
        pokey = item.POKEY;
   
        var stock = this.stockservice.Queryable().Where(x => x.LOT == tolot).FirstOrDefault();
        if (stock != null)
        {
          this.stockservice.Delete(stock.ID);
        }

        this.messageService.AddLog(MessageType.Information, method, receiptkey, pokey, $"{tolot} 取消入库", name);
      }
    }

    public void ChangeSate(int[] id, string state, string name) {
      var items = this.detailservice.Queryable().Where(x => id.Contains(x.ID));
      foreach (var item in items)
      {
        item.STATUS = state;
        this.detailservice.Update(item);
        var stock = this.stockservice.Queryable().Where(x => x.LOT == item.TOLOT).FirstOrDefault();
        if (stock != null)
        {
          stock.STATUS = state;
          stock.LOC = state == "104" ? "不良品区" : "良品区";
          this.stockservice.Update(stock);
        }
      }
    }
    //收货入库
    public void PostInputStock(POSTQTYRECEIVED[] data, string name)
    {
      var method = "确认收货";
      var id = data.Select(x => x.ID).ToArray();
      var items = this.detailservice.Queryable().Where(x => id.Contains(x.ID)).ToList();
      if (items == null)
      {
        this.messageService.AddLog(MessageType.Error, method, data[0].RECEIPTID.ToString(), data[0].ID.ToString(), $"没有找到收货单明细", name);
        throw new Exception("没有找到收货单明细");
      }
      var receiptid = items.First().RECEIPTID;
      var count = this.detailservice.Queryable().Where(x => x.RECEIPTID == receiptid).Count();
      var now = DateTime.Now;
      foreach (var item in items)
      {

        var tolot = KeyGenerator.GetNextLotKey();
        var receiptkey = item.RECEIPTKEY;
        var qtyreceived = data.Where(x => x.ID == item.ID).First().QTYRECEIVED;

        item.STATUS = "103";
        item.QTYRECEIVED = qtyreceived;
        item.DATERECEIVED = now;
        item.TOLOT = tolot;
        item.TOLOC = "STAGE";
        if (item.QTYRECEIVED < item.QTYEXPECTED)
        {
          var diffqty = item.QTYEXPECTED - item.QTYRECEIVED;
          item.QTYEXPECTED = qtyreceived;
          item.QTYADJUSTED = diffqty;


          var newitem = new RECEIPTDETAIL()
          {
            ALTSKU = item.ALTSKU,
            CASECNT = item.CASECNT,
            CONDITIONCODE = item.CONDITIONCODE,
            CUBE = item.CUBE,
            DATERECEIVED = item.DATERECEIVED,
            EXTERNLINENO = item.EXTERNLINENO,
            EXTERNRECEIPTKEY = item.EXTERNRECEIPTKEY,
            GROSSWGT = item.GROSSWGT,
            LOTTABLE1 = item.LOTTABLE1,
            LOTTABLE2 = item.LOTTABLE2,
            LOTTABLE3 = item.LOTTABLE3,
            LOTTABLE4 = item.LOTTABLE4,
            LOTTABLE5 = item.LOTTABLE5,
            LOTTABLE6 = item.LOTTABLE6,
            LOTTABLE7 = item.LOTTABLE7,
            LOTTABLE8 = item.LOTTABLE8,
            NETWGT = item.NETWGT,
            NOTES = item.NOTES,
            PACKKEY = item.PACKKEY,
            PALLET = item.PALLET,
            POKEY = item.POKEY,
            PQCDATE = item.PQCDATE,
            PQCQTYINSPECTED = item.PQCQTYINSPECTED,
            PQCQTYREJECTED = item.PQCQTYREJECTED,
            PQCSTATUS = item.PQCSTATUS,
            PQCWHO = item.PQCWHO,
            QTYADJUSTED = 0,
            QTYEXPECTED = diffqty,
            QTYRECEIVED = 0,
            RECEIPTDATE = item.RECEIPTDATE,
            QTYREJECTED = 0,
            RECEIPTID = item.RECEIPTID,
            RECEIPTKEY = item.RECEIPTKEY,
            RECEIPTLINENUMBER = ( ++count ).ToString("000"),
            SKU = item.SKU,
            SKUNAME = item.SKUNAME,
            STATUS = "100",
            STORERKEY = item.STORERKEY,
            SUPPLIERCODE = item.SUPPLIERCODE,
            SUPPLIERNAME = item.SUPPLIERNAME,
            UMO = item.UMO,
            WHSEID = item.WHSEID

          };
          this.detailservice.Insert(newitem);

        }
        //item.LOTTABLE3 = "人工确认";
        this.detailservice.Update(item);



        //添加库存
        var stock = new STOCK
        {
          CASECNT = item.CASECNT,
          CUBE = item.CUBE,
          EXTERNORDERKEY = null,
          EXTERNRECEIPTKEY = item.EXTERNRECEIPTKEY,
          GROSSWGT = item.GROSSWGT,
          INPUTDATETIME = DateTime.Now,
          LOC = "STAGE",
          LOT = tolot,
          LOTTABLE1 = item.LOTTABLE1,
          LOTTABLE2 = item.LOTTABLE2,
          LOTTABLE3 = item.LOTTABLE3,
          LOTTABLE4 = item.LOTTABLE4,
          LOTTABLE5 = item.LOTTABLE5,
          LOTTABLE6 = item.LOTTABLE6,
          LOTTABLE7 = item.LOTTABLE7,
          LOTTABLE8 = item.LOTTABLE8,
          LPN = item.TOLPN,
          NETWGT = item.NETWGT,
          NOTES = item.NOTES,
          PACKKEY = item.PACKKEY,
          PALLET = item.PALLET,
          POKEY = item.POKEY,
          QTY = item.QTYRECEIVED,
          RECEIPTDATE = item.RECEIPTDATE,
          RECEIPTKEY = item.RECEIPTKEY,
          SKU = item.SKU,
          SKUNAME = item.SKUNAME,
          STATUS = item.STATUS,
          STORERKEY = item.STORERKEY,
          SUPPLIERCODE = item.SUPPLIERCODE,
          SUPPLIERNAME = item.SUPPLIERNAME,
          UMO = item.UMO,
          WHSEID = item.WHSEID,
          ALTSKU = item.ALTSKU
        };
        this.stockservice.Insert(stock);

        this.messageService.AddLog(MessageType.Information, method, receiptkey, tolot, $"{tolot},{stock.SUPPLIERCODE},{stock.SKU},{stock.QTY}入库成功", name);
      }

    }

    public void UpdateOpenQty(int receiptid) {
      var items = this.detailservice.Queryable().Where(x=>x.RECEIPTID==receiptid).ToList();
      
        var total1 = items.Sum(x => x.QTYRECEIVED);
        var total2 = items.Sum(x => x.QTYEXPECTED);
        
        var head = this.Find(receiptid);
        head.OPENQTY = total2 - total1;
        head.TOTALQTY = total2;
        head.STATUS = ( head.OPENQTY > 0 ) ? "101" : "103";
      if (head.STATUS == "103")
      {
        head.ARRIVALDATETIME = DateTime.Now;
      }
        this.Update(head);

      }
    
  }
}



