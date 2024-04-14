﻿using System;
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
/// File: ISTOCKService.cs
/// Purpose: Service interfaces. Services expose a service interface
/// to which all inbound messages are sent. You can think of a service interface
/// as a façade that exposes the business logic implemented in the application
/// Created Date: 2019/6/26 13:33:30
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public interface ISTOCKService:IService<STOCK>
    {
                  
		void ImportDataTable(DataTable datatable);
		Stream ExportExcel( string filterRules = "",string sort = "ID", string order = "asc");
    void ChangeSate(POSTQCQTY[] data, string state, string name);
  }
}