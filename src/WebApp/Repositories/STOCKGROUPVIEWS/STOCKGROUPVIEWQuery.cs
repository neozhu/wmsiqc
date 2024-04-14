using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.SqlServer;
using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using System.Web.WebPages;
using WebApp.Models;

namespace WebApp.Repositories
{
/// <summary>
/// File: STOCKGROUPVIEWQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2019/7/9 7:29:32
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class STOCKGROUPVIEWQuery:QueryObject<STOCKGROUPVIEW>
   {
		public STOCKGROUPVIEWQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
						if (rule.field == "RID" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.RID == val);
                                break;
                            case "notequal":
                                And(x => x.RID != val);
                                break;
                            case "less":
                                And(x => x.RID < val);
                                break;
                            case "lessorequal":
                                And(x => x.RID <= val);
                                break;
                            case "greater":
                                And(x => x.RID > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.RID >= val);
                                break;
                            default:
                                And(x => x.RID == val);
                                break;
                        }
						}
						if (rule.field == "SUPPLIERCODE"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUPPLIERCODE.Contains(rule.value));
						}
						if (rule.field == "SUPPLIERNAME"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUPPLIERNAME.Contains(rule.value));
						}
						if (rule.field == "SKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SKU.Contains(rule.value));
						}
						if (rule.field == "SKUNAME"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SKUNAME.Contains(rule.value));
						}
						if (rule.field == "TOTAL" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TOTAL == val);
                                break;
                            case "notequal":
                                And(x => x.TOTAL != val);
                                break;
                            case "less":
                                And(x => x.TOTAL < val);
                                break;
                            case "lessorequal":
                                And(x => x.TOTAL <= val);
                                break;
                            case "greater":
                                And(x => x.TOTAL > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TOTAL >= val);
                                break;
                            default:
                                And(x => x.TOTAL == val);
                                break;
                        }
						}
						if (rule.field == "STATUS"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.STATUS.Contains(rule.value));
						}
						if (rule.field == "CreatedDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.CreatedDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.CreatedDate) <= 0);
						    }
						}
						if (rule.field == "CreatedBy"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CreatedBy.Contains(rule.value));
						}
						if (rule.field == "LastModifiedDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.LastModifiedDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.LastModifiedDate) <= 0);
						    }
						}
						if (rule.field == "LastModifiedBy"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LastModifiedBy.Contains(rule.value));
						}
     
               }
           }
            return this;
        }
    }
}
