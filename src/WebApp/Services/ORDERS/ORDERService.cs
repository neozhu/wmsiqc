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
using WebApp.Models.ViewModel;
using WebApp.Repositories;
namespace WebApp.Services
{
  /// <summary>
  /// File: ORDERService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2019/6/27 8:34:11
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class ORDERService : Service<ORDER>, IORDERService
  {
    private readonly IRepositoryAsync<ORDER> repository;
    private readonly IORDERDETAILService detailservice;
    private readonly IPICKDETAILService pickservice;
    private readonly ISTOCKService stockservice;
    private readonly IMessageService messageService;
    private readonly IDataTableImportMappingService mappingservice;

    public ORDERService(
      IMessageService messageService,
      IORDERDETAILService detailservice,
      IPICKDETAILService pickservice,
      ISTOCKService stockservice,

      IRepositoryAsync<ORDER> repository, IDataTableImportMappingService mappingservice)
        : base(repository)
    {
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.detailservice = detailservice;
      this.pickservice = pickservice;
      this.stockservice = stockservice;
      this.messageService = messageService;

    }
    public IEnumerable<ORDERDETAIL> GetORDERDETAILSByORDERID(int orderid) => this.repository.GetORDERDETAILSByORDERID(orderid);
    public IEnumerable<PICKDETAIL> GetPICKDETAILSByORDERID(int orderid) => this.repository.GetPICKDETAILSByORDERID(orderid);


    #region 注销
    //public void ImportDataTable(System.Data.DataTable datatable)
    //{
    //  var mapping = this.mappingservice.Queryable().Where(x => x.EntitySetName == "ORDER" && ( ( x.IsEnabled == true ) || ( x.IsEnabled == false && !( x.DefaultValue == null || x.DefaultValue.Equals(string.Empty) ) ) )).ToList();
    //  if (mapping == null || mapping.Count == 0)
    //  {
    //    throw new KeyNotFoundException("没有找到ORDER对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
    //  }
    //  foreach (DataRow row in datatable.Rows)
    //  {
    //    var item = new ORDER();
    //    var requiredfield = mapping.Where(x => x.IsRequired == true).FirstOrDefault()?.SourceFieldName;
    //    if (requiredfield != null && !row.IsNull(requiredfield) && row[requiredfield] != DBNull.Value && Convert.ToString(row[requiredfield]).Trim() != string.Empty)
    //    {
    //      foreach (var field in mapping)
    //      {
    //        var defval = field.DefaultValue;
    //        var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
    //        if (contain && !row.IsNull(field.SourceFieldName) && row[field.SourceFieldName] != DBNull.Value && row[field.SourceFieldName].ToString() != string.Empty)
    //        {
    //          var ordertype = item.GetType();
    //          var propertyInfo = ordertype.GetProperty(field.FieldName);
    //          var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
    //          var safeValue = ( row[field.SourceFieldName] == null ) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
    //          propertyInfo.SetValue(item, safeValue, null);
    //        }
    //        else if (!string.IsNullOrEmpty(defval))
    //        {
    //          var ordertype = item.GetType();
    //          var propertyInfo = ordertype.GetProperty(field.FieldName);
    //          if (string.Equals(defval, "now", StringComparison.OrdinalIgnoreCase) && ( propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(Nullable<DateTime>) ))
    //          {
    //            var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
    //            var safeValue = Convert.ChangeType(DateTime.Now, safetype);
    //            propertyInfo.SetValue(item, safeValue, null);
    //          }
    //          else if (string.Equals(defval, "guid", StringComparison.OrdinalIgnoreCase))
    //          {
    //            propertyInfo.SetValue(item, Guid.NewGuid().ToString(), null);
    //          }
    //          else
    //          {
    //            var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
    //            var safeValue = Convert.ChangeType(defval, safetype);
    //            propertyInfo.SetValue(item, safeValue, null);
    //          }
    //        }
    //      }
    //      this.Insert(item);
    //    }
    //  }
    //}
    #endregion
    public void ImportDataTable(System.Data.DataTable datatable)
    {
      var detaillist = new List<ORDERDETAIL>();
      var mapping = this.mappingservice.Queryable().Where(x => x.EntitySetName == "ORDERDETAIL" && ( ( x.IsEnabled == true ) || ( x.IsEnabled == false && !( x.DefaultValue == null || x.DefaultValue.Equals(string.Empty) ) ) )).ToList();
      if (mapping == null || mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到ORDERDETAIL对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      foreach (DataRow row in datatable.Rows)
      {
        var item = new ORDERDETAIL();
        var requiredfield = mapping.Where(x => x.IsRequired == true).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null && !row.IsNull(requiredfield) && row[requiredfield] != DBNull.Value && Convert.ToString(row[requiredfield]).Trim() != string.Empty)
        {
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain && !row.IsNull(field.SourceFieldName) && row[field.SourceFieldName] != DBNull.Value && row[field.SourceFieldName].ToString() != string.Empty)
            {
              var orderdetailtype = item.GetType();
              var propertyInfo = orderdetailtype.GetProperty(field.FieldName);
              var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
              var safeValue = ( row[field.SourceFieldName] == null ) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
              propertyInfo.SetValue(item, safeValue, null);


            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var orderdetailtype = item.GetType();
              var propertyInfo = orderdetailtype.GetProperty(field.FieldName);
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

          item.OPENQTY = item.ORIGINALQTY;
          detaillist.Add(item);
        }

      }
      if (detaillist.Count > 0)
      {
        var goup = detaillist.GroupBy(x => new { x.EXTERNORDERKEY, x.REQUESTEDSHIPDATE })
                  .Select(x => new { x.Key, ITEMS = x.ToList() });
        foreach (var g in goup)
        {
          var exkey = g.Key.EXTERNORDERKEY;
          var dt = g.Key.REQUESTEDSHIPDATE;
          var exist = this.Queryable().Where(x => x.EXTERNORDERKEY == exkey && x.REQUESTEDSHIPDATE == dt).Any();
          if (exist)
          {
            throw new Exception($"该批次[{exkey}]-[{dt.Value.ToString("yyyy-MM-dd")}]已经导入!");
          }
          var orderkey = KeyGenerator.GetNextSOKey();
          var head = new ORDER();
          var now = DateTime.Now;
          head.ORDERKEY = orderkey;
          head.ORDERDATE = now;
          head.EXTERNORDERKEY = exkey;
          head.REQUESTEDSHIPDATE = dt;
          head.DELIVERYDATE = g.ITEMS.First().DELIVERYDATE;
          head.NOTES = g.ITEMS.First().NOTES;
          head.OPENQTY = g.ITEMS.Sum(x => x.ORIGINALQTY);
          head.TOTALQTY = g.ITEMS.Sum(x => x.ORIGINALQTY);
          head.SUPPLIERCODE = g.ITEMS.First().SUPPLIERCODE;
          head.SUPPLIERNAME = g.ITEMS.First().SUPPLIERNAME;
          head.SUSR1 = g.ITEMS.First().LOTTABLE1;
          head.SUSR2 = g.ITEMS.First().LOTTABLE2;
          head.SUSR3 = g.ITEMS.First().LOTTABLE3;
          head.SUSR4 = g.ITEMS.First().LOTTABLE4;
          head.SUSR5 = g.ITEMS.First().LOTTABLE5;
          head.TOTALGROSSWEIGHT = g.ITEMS.Sum(x => x.GROSSWGT);
          head.TOTALPACKAGE = g.ITEMS.Sum(x => x.CASECNT);
          head.TOTALVOLUME = g.ITEMS.Sum(x => x.CUBE);
          head.TYPE = "1";
          head.WHSEID = g.ITEMS.First().WHSEID;
          head.STORERKEY = g.ITEMS.First().STORERKEY;
          head.STATUS = g.ITEMS.First().STATUS;
          head.TrackingState = TrackableEntities.TrackingState.Added;
          var line = 0;
          foreach (var item in g.ITEMS)
          {
            ++line;
            item.ORDERLINENUMBER = line.ToString("000");
            item.ORDERKEY = head.ORDERKEY;
            item.TrackingState = TrackableEntities.TrackingState.Added;
            item.ORDERID = head.ID;
            head.ORDERDETAILS.Add(item);
          }

          this.ApplyChanges(head);



        }
      }
    }
    public Stream ExportExcel(string filterRules = "", string sort = "ID", string order = "asc")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var orders = this.Query(new ORDERQuery().Withfilter(filters)).OrderBy(n => n.OrderBy(sort, order)).Select().ToList();
      var datarows = orders.Select(n => new
      {

        ORDERDETAILS = n.ORDERDETAILS,
        PICKDETAILS = n.PICKDETAILS,
        ID = n.ID,
        ORDERKEY = n.ORDERKEY,
        WHSEID = n.WHSEID,
        EXTERNORDERKEY = n.EXTERNORDERKEY,
        STATUS = n.STATUS,
        ORDERDATE = n.ORDERDATE.ToString("yyyy/MM/dd HH:mm:ss"),
        REQUESTEDSHIPDATE = n.REQUESTEDSHIPDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
        DELIVERYDATE = n.DELIVERYDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
        STORERKEY = n.STORERKEY,
        TYPE = n.TYPE,
        SUPPLIERCODE = n.SUPPLIERCODE,
        SUPPLIERNAME = n.SUPPLIERNAME,
        SUSR2 = n.SUSR2,
        CONSIGNEEKEY = n.CONSIGNEEKEY,
        CONSIGNEENAME = n.CONSIGNEENAME,
        CONSIGNEEADDRESS = n.CONSIGNEEADDRESS,
        CARRIERNAME = n.CARRIERNAME,
        DRIVERNAME = n.DRIVERNAME,
        CARRIERPHONE = n.CARRIERPHONE,
        TRAILERNUMBER = n.TRAILERNUMBER,
        ACTUALSHIPDATE = n.ACTUALSHIPDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
        ACTUALDELIVERYDATE = n.ACTUALDELIVERYDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
        CLOSEDDATE = n.CLOSEDDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
        TOTALQTY = n.TOTALQTY,
        OPENQTY = n.OPENQTY,
        TOTALPACKAGE = n.TOTALPACKAGE,
        TOTALGROSSWEIGHT = n.TOTALGROSSWEIGHT,
        TOTALVOLUME = n.TOTALVOLUME,
        NOTES = n.NOTES,
        SUSR1 = n.SUSR1,
        SUSR3 = n.SUSR3,
        SUSR4 = n.SUSR4,
        SUSR5 = n.SUSR5,
        SUSR6 = n.SUSR6,
        SUSR7 = n.SUSR7,
        SUSR8 = n.SUSR8?.ToString("yyyy/MM/dd HH:mm:ss")
      }).ToList();
      return ExcelHelper.ExportExcel(typeof(ORDER), datarows);
    }

    public void ScanPicking(int orderid, string barcode1, string barcode2, decimal qty, string name) {
      var method = "出库扫描";
      var code1 = resolverBarcode1(barcode1);
      var code2= resolverBarcode2(barcode2);
      var lot = code2.lot;
      var now = DateTime.Now;
      if (code1.sku != code2.sku)
      {
        this.messageService.AddLog(MessageType.Error, method, barcode1, barcode2, $"供应商条码SKU[{code1.sku}]方太条码[{code2.sku}]不匹配", name);

        throw new Exception($"供应商条码SKU[{code1.sku}]方太条码[{code2.sku}]不匹配");
      }
      var orderdetails = this.detailservice.Queryable()
          .Where(x => x.ORDERID == orderid && x.SKU == code2.sku).ToList();
      if (orderdetails == null || orderdetails.Count == 0)
      {
        this.messageService.AddLog(MessageType.Error, method, barcode1, barcode2, $"批号[{code2.lot}]SKU[{code2.sku}]无法匹配出货单明细", name);
        throw new Exception($"批号[{code2.lot}]SKU[{code2.sku}]无法匹配出货单明细");
      }
      foreach (var item in orderdetails)
      {
        //回填标签
        item.LOTTABLE5 = barcode2;
        item.LOTTABLE3 = barcode1;
        //需要拣货的数量
        //假设 需求数量 500;
        var pickqty = item.ORIGINALQTY-item.QTYPICKED;
        var suppliercode = item.SUPPLIERCODE;
        var sku = item.SKU;
        //改行已经全部拣货完成
        if (pickqty <= 0)
        {
          continue;
        }
        //查找库存
        var stockitems = this.stockservice.Queryable()
            .Where(x => x.LOTTABLE3 == barcode1
            && x.SUPPLIERCODE == item.SUPPLIERCODE
            && x.POKEY==lot
            && x.SKU == item.SKU
            && x.STATUS == "105")
            .OrderBy(x => x.RECEIPTDATE).ToList();
        if (stockitems.Count == 0)
        {
          throw new Exception($"供应商[{item.SUPPLIERCODE}]SKU[{code2.sku}]没有合格的库存");
        }
        foreach (var st in stockitems)
        {
          //假设 找到符合的第一个库存600;
          // 500-600=-100
          // 500-500=0
          // 500-100=400
          var inqty = st.QTY;
          pickqty = pickqty - inqty;
          if (pickqty < 0)
          {
            item.QTYPICKED += ( inqty + pickqty );
            item.STATUS = "106";
            //分拆库存行
            var newitem = new STOCK()
            {
              ALTSKU = st.ALTSKU,
              CASECNT = st.CASECNT,
              CUBE = st.CUBE,
              EXTERNORDERKEY = st.EXTERNORDERKEY,
              EXTERNRECEIPTKEY = st.EXTERNRECEIPTKEY,
              GROSSWGT = st.GROSSWGT,
              INPUTDATETIME = st.INPUTDATETIME,
              LOC = st.LOC,
              LOT = KeyGenerator.GetNextLotKey(),
              LOTTABLE1 = st.LOTTABLE1,
              LOTTABLE10 = st.LOTTABLE10,
              LOTTABLE11 = st.LOTTABLE11,
              LOTTABLE12 = st.LOTTABLE12,
              LOTTABLE2 = st.LOTTABLE2,
              LOTTABLE3 = st.LOTTABLE3,
              LOTTABLE4 = st.LOTTABLE4,
              LOTTABLE5 = st.LOTTABLE5,
              LOTTABLE6 = st.LOTTABLE6,
              LOTTABLE7 = st.LOTTABLE7,
              LOTTABLE8 = st.LOTTABLE8,
              LOTTABLE9 = st.LOTTABLE9,
              LPN = st.LPN,
              NETWGT = st.NETWGT,
              NOTES = st.NOTES,
              ORDERKEY = st.ORDERKEY,
              PACKKEY = st.PACKKEY,
              OUTPUTDATETIME = st.OUTPUTDATETIME,
              PALLET = st.PALLET,
              PICKDETAILKEY = st.PICKDETAILKEY,
              POKEY = st.POKEY,
              QTY = -pickqty,
              RECEIPTDATE = st.RECEIPTDATE,
              RECEIPTKEY = st.RECEIPTKEY,
              SKU = st.SKU,
              SKUNAME = st.SKUNAME,
              STATUS = st.STATUS,
              STORERKEY = st.STORERKEY,
              SUPPLIERCODE = st.SUPPLIERCODE,
              SUPPLIERNAME = st.SUPPLIERNAME,
              UMO = st.UMO,
              WHSEID = st.WHSEID


            };
            this.stockservice.Insert(newitem);
            //新增拣货明细
            var pick = new PICKDETAIL()
            {
              CASECNT = st.CASECNT,
              CUBE = st.CUBE,
              GROSSWGT = st.GROSSWGT,
              EXTERNRECEIPTKEY = st.EXTERNRECEIPTKEY,
              LOC = st.LOC,
              LOT = st.LOT,
              LOTTABLE1 = st.LOTTABLE1,
              LOTTABLE2 = st.LOTTABLE2,
              LOTTABLE3 = st.LOTTABLE3,
              LOTTABLE4 = st.LOTTABLE4,
              LOTTABLE5 = barcode2,
              LOTTABLE6 = st.LOTTABLE6,
              LOTTABLE7 = st.LOTTABLE7,
              LOTTABLE8 = st.LOTTABLE8,
              LPN = st.LPN,
              NETWGT = st.NETWGT,
              ORDERID = item.ORDERID,
              ORDERKEY = item.ORDERKEY,
              ORDERLINENUMBER = item.ORDERLINENUMBER,
              PACKKEY = st.PACKKEY,
              PALLET = st.PALLET,
              PICKDETAILKEY = KeyGenerator.GetNextPickKey(),
              POKEY = st.POKEY,
              QTY = ( inqty + pickqty ),
              RECEIPTDATE = st.RECEIPTDATE,
              RECEIPTKEY = st.RECEIPTKEY,
              SKU = st.SKU,
              SKUNAME = st.SKUNAME,
              STATUS = "106",
              STORERKEY = st.STORERKEY,
              SUPPLIERCODE = st.SUPPLIERCODE,
              SUPPLIERNAME = st.SUPPLIERNAME,
              UMO = st.UMO,
              WHSEID = st.WHSEID
            };
            this.pickservice.Insert(pick);

            st.QTY = ( inqty + pickqty );
            st.ORDERKEY = item.ORDERKEY;
            st.OUTPUTDATETIME = now;
            st.STATUS = "106";
            st.PICKDETAILKEY = pick.PICKDETAILKEY;
            st.LOTTABLE9 = barcode2;
            st.EXTERNORDERKEY = item.EXTERNORDERKEY;
            this.stockservice.Update(st);
            break;
          }
          else if (pickqty == 0)
          {
            item.QTYPICKED += ( inqty + pickqty );
            item.STATUS = "106";
            var pick = new PICKDETAIL()
            {
              CASECNT = st.CASECNT,
              CUBE = st.CUBE,
              GROSSWGT = st.GROSSWGT,
              EXTERNRECEIPTKEY = st.EXTERNRECEIPTKEY,
              LOC = st.LOC,
              LOT = st.LOT,
              LOTTABLE1 = st.LOTTABLE1,
              LOTTABLE2 = st.LOTTABLE2,
              LOTTABLE3 = st.LOTTABLE3,
              LOTTABLE4 = st.LOTTABLE4,
              LOTTABLE5 = barcode2,
              LOTTABLE6 = st.LOTTABLE6,
              LOTTABLE7 = st.LOTTABLE7,
              LOTTABLE8 = st.LOTTABLE8,
              LPN = st.LPN,
              NETWGT = st.NETWGT,
              ORDERID = item.ORDERID,
              ORDERKEY = item.ORDERKEY,
              ORDERLINENUMBER = item.ORDERLINENUMBER,
              PACKKEY = st.PACKKEY,
              PALLET = st.PALLET,
              PICKDETAILKEY = KeyGenerator.GetNextPickKey(),
              POKEY = st.POKEY,
              QTY = ( inqty + pickqty ),
              RECEIPTDATE = st.RECEIPTDATE,
              RECEIPTKEY = st.RECEIPTKEY,
              SKU = st.SKU,
              SKUNAME = st.SKUNAME,
              STATUS = "106",
              STORERKEY = st.STORERKEY,
              SUPPLIERCODE = st.SUPPLIERCODE,
              SUPPLIERNAME = st.SUPPLIERNAME,
              UMO = st.UMO,
              WHSEID = st.WHSEID
            };
            this.pickservice.Insert(pick);
            st.QTY = ( inqty + pickqty );
            st.ORDERKEY = item.ORDERKEY;
            st.OUTPUTDATETIME = now;
            st.PICKDETAILKEY = pick.PICKDETAILKEY;
            st.LOTTABLE9 = barcode2;
            st.STATUS = "106";
            this.stockservice.Update(st);
            break;
          }
          else
          {
            item.QTYPICKED += inqty;
            item.STATUS = "102";
            var pick = new PICKDETAIL()
            {
              CASECNT = st.CASECNT,
              CUBE = st.CUBE,
              GROSSWGT = st.GROSSWGT,
              EXTERNRECEIPTKEY = st.EXTERNRECEIPTKEY,
              LOC = st.LOC,
              LOT = st.LOT,
              LOTTABLE1 = st.LOTTABLE1,
              LOTTABLE2 = st.LOTTABLE2,
              LOTTABLE3 = st.LOTTABLE3,
              LOTTABLE4 = st.LOTTABLE4,
              LOTTABLE5 = barcode2,
              LOTTABLE6 = st.LOTTABLE6,
              LOTTABLE7 = st.LOTTABLE7,
              LOTTABLE8 = st.LOTTABLE8,
              LPN = st.LPN,
              NETWGT = st.NETWGT,
              ORDERID = item.ORDERID,
              ORDERKEY = item.ORDERKEY,
              ORDERLINENUMBER = item.ORDERLINENUMBER,
              PACKKEY = st.PACKKEY,
              PALLET = st.PALLET,
              PICKDETAILKEY = KeyGenerator.GetNextPickKey(),
              POKEY = st.POKEY,
              QTY = inqty,
              RECEIPTDATE = st.RECEIPTDATE,
              RECEIPTKEY = st.RECEIPTKEY,
              SKU = st.SKU,
              SKUNAME = st.SKUNAME,
              STATUS = "106",
              STORERKEY = st.STORERKEY,
              SUPPLIERCODE = st.SUPPLIERCODE,
              SUPPLIERNAME = st.SUPPLIERNAME,
              UMO = st.UMO,
              WHSEID = st.WHSEID
            };
            this.pickservice.Insert(pick);
            st.QTY = inqty;
            st.ORDERKEY = item.ORDERKEY;
            st.OUTPUTDATETIME = now;
            st.PICKDETAILKEY = pick.PICKDETAILKEY;
            st.LOTTABLE9 = barcode2;
            st.STATUS = "106";
            this.stockservice.Update(st);
          }

        }
        this.detailservice.Update(item);
        this.messageService.AddLog(MessageType.Information, method, item.ORDERKEY,item.SKU, $"{item.SUPPLIERCODE},{item.SKU},{item.QTYPICKED}-{barcode1},{barcode2}拣货完成", name);
      }


        
      
      

    }

    //解析供应商标签
    private (string suppliercode, DateTime dt,string sku,string lineno) resolverBarcode1(string barcode1) {
      var array = barcode1.Split(new string[] { "——" },StringSplitOptions.RemoveEmptyEntries);
      if (array.Length == 4)
      {
        var suppliercode = array[0];
        var dt = array[1];
        var sku = array[2];
        var no = array[3];
        var date = this.getdate(dt);
        return (suppliercode, date, sku, no);
      }
      else
      {
        throw new Exception("条码长度不符合");
      }
  }
    private (string lot, DateTime dt, string sku, string lineno) resolverBarcode2(string barcode2)
    {
      var array = barcode2.Split(new string[] { "——" }, StringSplitOptions.RemoveEmptyEntries);
      if (array.Length == 3 || array.Length==4)
      {
        var lot = array[0];
        var dt = array[1];
        var sku ="";
        var no = "";
        if (array.Length == 3)
        {
          var arr = array[2].Split('_');
          sku = arr[0];
          no = arr[1];
        }
        else
        {
          sku = array[2];
          no = array[3];
        }
        
        var date = this.getdate(dt);
        return (lot, date, sku, no);
      }
      else
      {
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
    public void UpdateQty(int orderid) {
      var items = this.detailservice.Queryable().Where(x => x.ORDERID == orderid).ToList();
      var head = this.Find(orderid);
      var total1 = items.Sum(x => x.ORIGINALQTY);
      var total2= items.Sum(x => x.CASECNT);
      var total3= items.Sum(x => x.OPENQTY);
      var total4 = items.Sum(x => x.SHIPPEDQTY);
      var total5 = items.Sum(x => x.QTYPICKED);
      var total6 = items.Sum(x => x.PALLET);
      var total7 = items.Sum(x => x.GROSSWGT);
      var total8 = items.Sum(x => x.NETWGT);
      var total9 = items.Sum(x => x.CUBE);
      head.TOTALQTY = total1;
      head.OPENQTY = total3;
      head.TOTALPACKAGE = total2;
      head.TOTALGROSSWEIGHT = total7;
      head.TOTALVOLUME = total9;
      head.TOTALQTYPICKED = total5;

      if (total5 == total1 && total4 == 0)
      {
        head.STATUS = "106";
      }
      else if (total1 > total5 && total4 == 0)
      {
        head.STATUS = "102";
      }
      else if (total4 == total1)
      {
        head.STATUS = "108";
      }
      else if(total4 > 0 )
      {
        head.STATUS = "107";
      }
      this.Update(head);

    }
    //出货发运
    public void PostShipping(POSTQTYSHIPPED[] data,string name) {
      var method = "出货发运";
      var detailid = data.Select(x => x.ID).ToArray();
      var orderid = data[0].ORDERID;
      var items = this.detailservice.Queryable().Where(x => detailid.Contains(x.ID)).ToList();
      if (items == null)
      {

        throw new Exception("没有找到需要发运的记录");
      }
      var now = DateTime.Now;
      var count = this.detailservice.Queryable().Where(x => x.ORDERID == orderid).Count();
      foreach (var item in items)
      {
        var shippedqty = data.Where(x => x.ID == item.ID).First().QTYSHIPPED;
        if (shippedqty > item.QTYPICKED)
        {
          //发运数量不能超过拣货数量
          shippedqty = item.QTYPICKED;
        }
        else
        if (shippedqty < item.QTYPICKED)
        {
          //这种情况不允许也不存在
          //需要拆分出货
          //如果拣货数量100,本次出货60,剩下40下次出货
          //如果订单需求数量120,拣货60,剩下40已检,20未拣货,
          var diffqty = item.QTYPICKED - shippedqty;
          var newitem = new ORDERDETAIL()
          {
            ALTSKU = item.ALTSKU,
            CASECNT = item.CASECNT,
            CUBE = item.CUBE,
            DELIVERYDATE = item.DELIVERYDATE,
            EXTERNLINENO = item.EXTERNLINENO,
            EXTERNORDERKEY = item.EXTERNORDERKEY,
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
            OPENQTY = item.ORIGINALQTY - shippedqty,
            ORDERKEY = item.ORDERKEY,
            ORDERID = item.ORDERID,
            ORDERLINENUMBER = ( ++count ).ToString("000"),
            ORIGINALQTY = item.ORIGINALQTY - shippedqty,
            PACKKEY = item.PACKKEY,
            PALLET = item.PALLET,
            PQCDATE = item.PQCDATE,
            PQCQTYINSPECTED = item.PQCQTYINSPECTED,
            PQCQTYREJECTED = item.PQCQTYREJECTED,
            PQCSTATUS = item.PQCSTATUS,
            PQCWHO = item.PQCWHO,
            QTYPICKED = item.QTYPICKED - shippedqty,
            REQUESTEDSHIPDATE = item.REQUESTEDSHIPDATE,
            SHIPPEDQTY = 0,
            SKU = item.SKU,
            SKUNAME = item.SKUNAME,
            STATUS = ( diffqty == ( item.ORIGINALQTY - shippedqty ) ? "106" : "102" ),
            STORERKEY = item.STORERKEY,
            SUPPLIERCODE = item.SUPPLIERCODE,
            SUPPLIERNAME = item.SUPPLIERNAME,
            TYPE = item.TYPE,
            UMO = item.UMO,
            WHSEID = item.WHSEID

          };
          this.detailservice.Insert(newitem);

          item.ORIGINALQTY = shippedqty;
          item.OPENQTY = 0;
          item.SHIPPEDQTY = shippedqty;
          item.QTYPICKED = shippedqty;
          item.STATUS = "108";
          item.ACTUALSHIPDATE = now;
          this.detailservice.Update(item);
        }
        else
        {
          //item.ORIGINALQTY = shippedqty;
          item.OPENQTY = (item.ORIGINALQTY - shippedqty);
          item.SHIPPEDQTY = shippedqty;
          //item.QTYPICKED = shippedqty;
          if (item.OPENQTY > 0)
          {
            item.STATUS = "107";
          }
          else
          {
            item.STATUS = "108";
          }
          
          item.ACTUALSHIPDATE = now;
          this.detailservice.Update(item);
        }
        //修改拣货明细状态
        var pickdetails = this.pickservice.Queryable().Where(x => x.ORDERID == item.ORDERID &&
          x.ORDERLINENUMBER == item.ORDERLINENUMBER &&
          x.STATUS=="106").ToList();
        foreach (var pick in pickdetails)
        {
          pick.STATUS = "108";
          pick.LOTTABLE8 = now;
          this.pickservice.Update(pick);
          //更新库存状态为出货;
          var stdetails = this.stockservice.Queryable().Where(x => x.PICKDETAILKEY == pick.PICKDETAILKEY).ToList();
          foreach (var st in stdetails)
          {
            st.STATUS = "108";
            st.LOTTABLE8 = now;
            this.stockservice.Update(st);
          }
        }
        this.messageService.AddLog(MessageType.Information, method, item.ORDERKEY, item.SKU, $"{item.SUPPLIERCODE},{item.SKU},{item.SHIPPEDQTY}出货完成", name);
      }
    }
    //取消拣货
    public void UndoPicked(int pickid, string name) {
      var method = "取消拣货";
      var pickitem = this.pickservice.Find(pickid);
      var orderdetail = this.detailservice.Queryable()
        .Where(x => x.ORDERID == pickitem.ORDERID &&
         x.ORDERLINENUMBER == pickitem.ORDERLINENUMBER).FirstOrDefault();
      if (orderdetail != null)
      {
        orderdetail.QTYPICKED = orderdetail.QTYPICKED - pickitem.QTY;
        if (orderdetail.QTYPICKED == 0)
        {
          orderdetail.STATUS = "100";
        }
        else
        {
          orderdetail.STATUS = "102";
        }
        this.detailservice.Update(orderdetail);
        this.messageService.AddLog(MessageType.Information, method, orderdetail.ORDERKEY, orderdetail.SKU, $"{orderdetail.SUPPLIERCODE},{orderdetail.SKU},{orderdetail.ORIGINALQTY}取消拣货", name);
      }
      var stock = this.stockservice.Queryable().Where(x => x.PICKDETAILKEY == pickitem.PICKDETAILKEY).FirstOrDefault();
      if (stock != null)
      {
        stock.PICKDETAILKEY = null;
        stock.ORDERKEY = null;
        stock.OUTPUTDATETIME = null;
        stock.STATUS = "105";
        stock.LOTTABLE9 = null;
        this.stockservice.Update(stock);
      }
      this.pickservice.Delete(pickitem.ID);

      
    }
  }
}



