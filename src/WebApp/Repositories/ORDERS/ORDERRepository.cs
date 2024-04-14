using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using WebApp.Models;
namespace WebApp.Repositories
{
/// <summary>
/// File: ORDERRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 2019/6/27 8:33:59
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class ORDERRepository  
    {
                        public static IEnumerable<ORDERDETAIL>   GetORDERDETAILSByORDERID (this IRepositoryAsync<ORDER> repository,int orderid)
        {
			var orderdetailRepository = repository.GetRepository<ORDERDETAIL>(); 
            return orderdetailRepository.Queryable().Include(x => x.ORDER).Where(n => n.ORDERID == orderid);
        }
                public static IEnumerable<PICKDETAIL>   GetPICKDETAILSByORDERID (this IRepositoryAsync<ORDER> repository,int orderid)
        {
			var pickdetailRepository = repository.GetRepository<PICKDETAIL>(); 
            return pickdetailRepository.Queryable().Include(x => x.ORDER).Where(n => n.ORDERID == orderid);
        }
         
	}
}



