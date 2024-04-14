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
/// File: RECEIPTRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 2019/6/25 7:35:11
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class RECEIPTRepository  
    {
                        public static IEnumerable<RECEIPTDETAIL>   GetRECEIPTDETAILSByRECEIPTID (this IRepositoryAsync<RECEIPT> repository,int receiptid)
        {
			var receiptdetailRepository = repository.GetRepository<RECEIPTDETAIL>(); 
            return receiptdetailRepository.Queryable().Include(x => x.RECEIPT).Where(n => n.RECEIPTID == receiptid);
        }
         
	}
}



