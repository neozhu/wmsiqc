using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Service.Pattern;
using WebApp.Models;
using WebApp.Repositories;
using System.Data;
using System.IO;
using WebApp.Models.ViewModel;

namespace WebApp.Services
{
  /// <summary>
  /// File: IORDERService.cs
  /// Purpose: Service interfaces. Services expose a service interface
  /// to which all inbound messages are sent. You can think of a service interface
  /// as a façade that exposes the business logic implemented in the application
  /// Created Date: 2019/6/27 8:34:07
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public interface IORDERService : IService<ORDER>
  {
    IEnumerable<ORDERDETAIL> GetORDERDETAILSByORDERID(int orderid);
    IEnumerable<PICKDETAIL> GetPICKDETAILSByORDERID(int orderid);

    void ImportDataTable(DataTable datatable);
    Stream ExportExcel(string filterRules = "", string sort = "ID", string order = "asc");
    void ScanPicking(int orderid, string barcode1, string barcode2, decimal qty, string name);
    void UpdateQty(int orderid);
    void PostShipping(POSTQTYSHIPPED[] data,string name);
    void UndoPicked(int pickid, string name);
  }
}