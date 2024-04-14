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
/// File: ORDERQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2019/6/27 8:34:03
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class ORDERQuery:QueryObject<ORDER>
   {
		public ORDERQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
						if (rule.field == "ID" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ID == val);
                                break;
                            case "notequal":
                                And(x => x.ID != val);
                                break;
                            case "less":
                                And(x => x.ID < val);
                                break;
                            case "lessorequal":
                                And(x => x.ID <= val);
                                break;
                            case "greater":
                                And(x => x.ID > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ID >= val);
                                break;
                            default:
                                And(x => x.ID == val);
                                break;
                        }
						}
						if (rule.field == "ORDERKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ORDERKEY.Contains(rule.value));
						}
						if (rule.field == "WHSEID"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WHSEID.Contains(rule.value));
						}
						if (rule.field == "EXTERNORDERKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EXTERNORDERKEY.Contains(rule.value));
						}
						if (rule.field == "STATUS"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.STATUS.Contains(rule.value));
						}
						if (rule.field == "ORDERDATE" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ORDERDATE) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ORDERDATE) <= 0);
						    }
						}
						if (rule.field == "REQUESTEDSHIPDATE" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.REQUESTEDSHIPDATE) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.REQUESTEDSHIPDATE) <= 0);
						    }
						}
						if (rule.field == "DELIVERYDATE" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.DELIVERYDATE) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.DELIVERYDATE) <= 0);
						    }
						}
						if (rule.field == "STORERKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.STORERKEY.Contains(rule.value));
						}
						if (rule.field == "TYPE"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TYPE.Contains(rule.value));
						}
						if (rule.field == "SUPPLIERCODE"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUPPLIERCODE.Contains(rule.value));
						}
						if (rule.field == "SUPPLIERNAME"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUPPLIERNAME.Contains(rule.value));
						}
						if (rule.field == "SUSR2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUSR2.Contains(rule.value));
						}
						if (rule.field == "CONSIGNEEKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CONSIGNEEKEY.Contains(rule.value));
						}
						if (rule.field == "CONSIGNEENAME"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CONSIGNEENAME.Contains(rule.value));
						}
						if (rule.field == "CONSIGNEEADDRESS"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CONSIGNEEADDRESS.Contains(rule.value));
						}
						if (rule.field == "CARRIERNAME"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CARRIERNAME.Contains(rule.value));
						}
						if (rule.field == "DRIVERNAME"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.DRIVERNAME.Contains(rule.value));
						}
						if (rule.field == "CARRIERPHONE"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CARRIERPHONE.Contains(rule.value));
						}
						if (rule.field == "TRAILERNUMBER"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.TRAILERNUMBER.Contains(rule.value));
						}
						if (rule.field == "ACTUALSHIPDATE" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ACTUALSHIPDATE) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ACTUALSHIPDATE) <= 0);
						    }
						}
						if (rule.field == "ACTUALDELIVERYDATE" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.ACTUALDELIVERYDATE) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.ACTUALDELIVERYDATE) <= 0);
						    }
						}
						if (rule.field == "CLOSEDDATE" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.CLOSEDDATE) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.CLOSEDDATE) <= 0);
						    }
						}
						if (rule.field == "TOTALQTY" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TOTALQTY == val);
                                break;
                            case "notequal":
                                And(x => x.TOTALQTY != val);
                                break;
                            case "less":
                                And(x => x.TOTALQTY < val);
                                break;
                            case "lessorequal":
                                And(x => x.TOTALQTY <= val);
                                break;
                            case "greater":
                                And(x => x.TOTALQTY > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TOTALQTY >= val);
                                break;
                            default:
                                And(x => x.TOTALQTY == val);
                                break;
                        }
						}
						if (rule.field == "OPENQTY" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.OPENQTY == val);
                                break;
                            case "notequal":
                                And(x => x.OPENQTY != val);
                                break;
                            case "less":
                                And(x => x.OPENQTY < val);
                                break;
                            case "lessorequal":
                                And(x => x.OPENQTY <= val);
                                break;
                            case "greater":
                                And(x => x.OPENQTY > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.OPENQTY >= val);
                                break;
                            default:
                                And(x => x.OPENQTY == val);
                                break;
                        }
						}
						if (rule.field == "TOTALPACKAGE" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TOTALPACKAGE == val);
                                break;
                            case "notequal":
                                And(x => x.TOTALPACKAGE != val);
                                break;
                            case "less":
                                And(x => x.TOTALPACKAGE < val);
                                break;
                            case "lessorequal":
                                And(x => x.TOTALPACKAGE <= val);
                                break;
                            case "greater":
                                And(x => x.TOTALPACKAGE > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TOTALPACKAGE >= val);
                                break;
                            default:
                                And(x => x.TOTALPACKAGE == val);
                                break;
                        }
						}
						if (rule.field == "TOTALGROSSWEIGHT" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TOTALGROSSWEIGHT == val);
                                break;
                            case "notequal":
                                And(x => x.TOTALGROSSWEIGHT != val);
                                break;
                            case "less":
                                And(x => x.TOTALGROSSWEIGHT < val);
                                break;
                            case "lessorequal":
                                And(x => x.TOTALGROSSWEIGHT <= val);
                                break;
                            case "greater":
                                And(x => x.TOTALGROSSWEIGHT > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TOTALGROSSWEIGHT >= val);
                                break;
                            default:
                                And(x => x.TOTALGROSSWEIGHT == val);
                                break;
                        }
						}
						if (rule.field == "TOTALVOLUME" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TOTALVOLUME == val);
                                break;
                            case "notequal":
                                And(x => x.TOTALVOLUME != val);
                                break;
                            case "less":
                                And(x => x.TOTALVOLUME < val);
                                break;
                            case "lessorequal":
                                And(x => x.TOTALVOLUME <= val);
                                break;
                            case "greater":
                                And(x => x.TOTALVOLUME > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TOTALVOLUME >= val);
                                break;
                            default:
                                And(x => x.TOTALVOLUME == val);
                                break;
                        }
						}
						if (rule.field == "NOTES"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.NOTES.Contains(rule.value));
						}
						if (rule.field == "SUSR1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUSR1.Contains(rule.value));
						}
						if (rule.field == "SUSR3"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUSR3.Contains(rule.value));
						}
						if (rule.field == "SUSR4"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUSR4.Contains(rule.value));
						}
						if (rule.field == "SUSR5"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUSR5.Contains(rule.value));
						}
						if (rule.field == "SUSR6" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.SUSR6 == val);
                                break;
                            case "notequal":
                                And(x => x.SUSR6 != val);
                                break;
                            case "less":
                                And(x => x.SUSR6 < val);
                                break;
                            case "lessorequal":
                                And(x => x.SUSR6 <= val);
                                break;
                            case "greater":
                                And(x => x.SUSR6 > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.SUSR6 >= val);
                                break;
                            default:
                                And(x => x.SUSR6 == val);
                                break;
                        }
						}
						if (rule.field == "SUSR7" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.SUSR7 == val);
                                break;
                            case "notequal":
                                And(x => x.SUSR7 != val);
                                break;
                            case "less":
                                And(x => x.SUSR7 < val);
                                break;
                            case "lessorequal":
                                And(x => x.SUSR7 <= val);
                                break;
                            case "greater":
                                And(x => x.SUSR7 > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.SUSR7 >= val);
                                break;
                            default:
                                And(x => x.SUSR7 == val);
                                break;
                        }
						}
						if (rule.field == "SUSR8" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.SUSR8) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.SUSR8) <= 0);
						    }
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
