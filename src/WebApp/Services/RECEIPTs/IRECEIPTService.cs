using System.Collections.Generic;
using System.Data;
using System.IO;
using Service.Pattern;
using WebApp.Models;
using WebApp.Models.ViewModel;

namespace WebApp.Services
{
  /// <summary>
  /// File: IRECEIPTService.cs
  /// Purpose: Service interfaces. Services expose a service interface
  /// to which all inbound messages are sent. You can think of a service interface
  /// as a façade that exposes the business logic implemented in the application
  /// Created Date: 2019/6/25 7:35:45
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public interface IRECEIPTService : IService<RECEIPT>
  {
    IEnumerable<RECEIPTDETAIL> GetRECEIPTDETAILSByRECEIPTID(int receiptid);

    void ImportDataTable(DataTable datatable);
    Stream ExportExcel(string filterRules = "", string sort = "ID", string order = "asc");

    void ImportExcel(DataTable datatable,string user);
    int ScanInput(int id,string barcode,decimal qty,string name);
    void Undo(int id, string name);
    void ChangeSate(int[] id, string state, string name);
    void PostInputStock(POSTQTYRECEIVED[] data, string name);
    void UpdateOpenQty(int id);
  }
}