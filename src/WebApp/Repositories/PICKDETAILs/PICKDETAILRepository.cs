﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using WebApp.Models;
namespace WebApp.Repositories
{
/// <summary>
/// File: PICKDETAILRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 2019/6/27 8:22:22
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class PICKDETAILRepository  
    {
                 public static IEnumerable<PICKDETAIL> GetByORDERID(this IRepositoryAsync<PICKDETAIL> repository, int orderid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ORDERID==orderid);
             return query;
         }  
                 
	}
}



