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
/// File: ORDERDETAILQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2019/6/27 8:28:01
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class ORDERDETAILQuery:QueryObject<ORDERDETAIL>
   {
		public ORDERDETAILQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "STORERKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.STORERKEY.Contains(rule.value));
						}
						if (rule.field == "EXTERNORDERKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EXTERNORDERKEY.Contains(rule.value));
						}
						if (rule.field == "ORDERLINENUMBER"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ORDERLINENUMBER.Contains(rule.value));
						}
						if (rule.field == "STATUS"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.STATUS.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE2.Contains(rule.value));
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
						if (rule.field == "ALTSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ALTSKU.Contains(rule.value));
						}
						if (rule.field == "ORIGINALQTY" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ORIGINALQTY == val);
                                break;
                            case "notequal":
                                And(x => x.ORIGINALQTY != val);
                                break;
                            case "less":
                                And(x => x.ORIGINALQTY < val);
                                break;
                            case "lessorequal":
                                And(x => x.ORIGINALQTY <= val);
                                break;
                            case "greater":
                                And(x => x.ORIGINALQTY > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ORIGINALQTY >= val);
                                break;
                            default:
                                And(x => x.ORIGINALQTY == val);
                                break;
                        }
						}
						if (rule.field == "UMO"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.UMO.Contains(rule.value));
						}
						if (rule.field == "CASECNT" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.CASECNT == val);
                                break;
                            case "notequal":
                                And(x => x.CASECNT != val);
                                break;
                            case "less":
                                And(x => x.CASECNT < val);
                                break;
                            case "lessorequal":
                                And(x => x.CASECNT <= val);
                                break;
                            case "greater":
                                And(x => x.CASECNT > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.CASECNT >= val);
                                break;
                            default:
                                And(x => x.CASECNT == val);
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
						if (rule.field == "SHIPPEDQTY" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.SHIPPEDQTY == val);
                                break;
                            case "notequal":
                                And(x => x.SHIPPEDQTY != val);
                                break;
                            case "less":
                                And(x => x.SHIPPEDQTY < val);
                                break;
                            case "lessorequal":
                                And(x => x.SHIPPEDQTY <= val);
                                break;
                            case "greater":
                                And(x => x.SHIPPEDQTY > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.SHIPPEDQTY >= val);
                                break;
                            default:
                                And(x => x.SHIPPEDQTY == val);
                                break;
                        }
						}
						if (rule.field == "QTYPICKED" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.QTYPICKED == val);
                                break;
                            case "notequal":
                                And(x => x.QTYPICKED != val);
                                break;
                            case "less":
                                And(x => x.QTYPICKED < val);
                                break;
                            case "lessorequal":
                                And(x => x.QTYPICKED <= val);
                                break;
                            case "greater":
                                And(x => x.QTYPICKED > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.QTYPICKED >= val);
                                break;
                            default:
                                And(x => x.QTYPICKED == val);
                                break;
                        }
						}
						if (rule.field == "PALLET" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PALLET == val);
                                break;
                            case "notequal":
                                And(x => x.PALLET != val);
                                break;
                            case "less":
                                And(x => x.PALLET < val);
                                break;
                            case "lessorequal":
                                And(x => x.PALLET <= val);
                                break;
                            case "greater":
                                And(x => x.PALLET > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PALLET >= val);
                                break;
                            default:
                                And(x => x.PALLET == val);
                                break;
                        }
						}
						if (rule.field == "PACKKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PACKKEY.Contains(rule.value));
						}
						if (rule.field == "GROSSWGT" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.GROSSWGT == val);
                                break;
                            case "notequal":
                                And(x => x.GROSSWGT != val);
                                break;
                            case "less":
                                And(x => x.GROSSWGT < val);
                                break;
                            case "lessorequal":
                                And(x => x.GROSSWGT <= val);
                                break;
                            case "greater":
                                And(x => x.GROSSWGT > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.GROSSWGT >= val);
                                break;
                            default:
                                And(x => x.GROSSWGT == val);
                                break;
                        }
						}
						if (rule.field == "NETWGT" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.NETWGT == val);
                                break;
                            case "notequal":
                                And(x => x.NETWGT != val);
                                break;
                            case "less":
                                And(x => x.NETWGT < val);
                                break;
                            case "lessorequal":
                                And(x => x.NETWGT <= val);
                                break;
                            case "greater":
                                And(x => x.NETWGT > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.NETWGT >= val);
                                break;
                            default:
                                And(x => x.NETWGT == val);
                                break;
                        }
						}
						if (rule.field == "CUBE" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.CUBE == val);
                                break;
                            case "notequal":
                                And(x => x.CUBE != val);
                                break;
                            case "less":
                                And(x => x.CUBE < val);
                                break;
                            case "lessorequal":
                                And(x => x.CUBE <= val);
                                break;
                            case "greater":
                                And(x => x.CUBE > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.CUBE >= val);
                                break;
                            default:
                                And(x => x.CUBE == val);
                                break;
                        }
						}
						if (rule.field == "NOTES"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.NOTES.Contains(rule.value));
						}
						if (rule.field == "PQCQTYINSPECTED" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PQCQTYINSPECTED == val);
                                break;
                            case "notequal":
                                And(x => x.PQCQTYINSPECTED != val);
                                break;
                            case "less":
                                And(x => x.PQCQTYINSPECTED < val);
                                break;
                            case "lessorequal":
                                And(x => x.PQCQTYINSPECTED <= val);
                                break;
                            case "greater":
                                And(x => x.PQCQTYINSPECTED > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PQCQTYINSPECTED >= val);
                                break;
                            default:
                                And(x => x.PQCQTYINSPECTED == val);
                                break;
                        }
						}
						if (rule.field == "PQCQTYREJECTED" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PQCQTYREJECTED == val);
                                break;
                            case "notequal":
                                And(x => x.PQCQTYREJECTED != val);
                                break;
                            case "less":
                                And(x => x.PQCQTYREJECTED < val);
                                break;
                            case "lessorequal":
                                And(x => x.PQCQTYREJECTED <= val);
                                break;
                            case "greater":
                                And(x => x.PQCQTYREJECTED > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PQCQTYREJECTED >= val);
                                break;
                            default:
                                And(x => x.PQCQTYREJECTED == val);
                                break;
                        }
						}
						if (rule.field == "PQCSTATUS"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PQCSTATUS.Contains(rule.value));
						}
						if (rule.field == "PQCWHO"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PQCWHO.Contains(rule.value));
						}
						if (rule.field == "PQCDATE" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.PQCDATE) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.PQCDATE) <= 0);
						    }
						}
						if (rule.field == "LOTTABLE3"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE3.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE1.Contains(rule.value));
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
						if (rule.field == "EXTERNLINENO"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EXTERNLINENO.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE4"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE4.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE5"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE5.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE6" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.LOTTABLE6 == val);
                                break;
                            case "notequal":
                                And(x => x.LOTTABLE6 != val);
                                break;
                            case "less":
                                And(x => x.LOTTABLE6 < val);
                                break;
                            case "lessorequal":
                                And(x => x.LOTTABLE6 <= val);
                                break;
                            case "greater":
                                And(x => x.LOTTABLE6 > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.LOTTABLE6 >= val);
                                break;
                            default:
                                And(x => x.LOTTABLE6 == val);
                                break;
                        }
						}
						if (rule.field == "LOTTABLE7" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.LOTTABLE7 == val);
                                break;
                            case "notequal":
                                And(x => x.LOTTABLE7 != val);
                                break;
                            case "less":
                                And(x => x.LOTTABLE7 < val);
                                break;
                            case "lessorequal":
                                And(x => x.LOTTABLE7 <= val);
                                break;
                            case "greater":
                                And(x => x.LOTTABLE7 > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.LOTTABLE7 >= val);
                                break;
                            default:
                                And(x => x.LOTTABLE7 == val);
                                break;
                        }
						}
						if (rule.field == "LOTTABLE8" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.LOTTABLE8) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.LOTTABLE8) <= 0);
						    }
						}
						if (rule.field == "ORDERID" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ORDERID == val);
                                break;
                            case "notequal":
                                And(x => x.ORDERID != val);
                                break;
                            case "less":
                                And(x => x.ORDERID < val);
                                break;
                            case "lessorequal":
                                And(x => x.ORDERID <= val);
                                break;
                            case "greater":
                                And(x => x.ORDERID > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ORDERID >= val);
                                break;
                            default:
                                And(x => x.ORDERID == val);
                                break;
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
         public  ORDERDETAILQuery ByORDERIDWithfilter(int orderid, IEnumerable<filterRule> filters)
         {
            And(x => x.ORDERID == orderid);
            if (filters != null)
           {
               foreach (var rule in filters)
               {     
						if (rule.field == "ID" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
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
						if (rule.field == "STORERKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.STORERKEY.Contains(rule.value));
						}
						if (rule.field == "EXTERNORDERKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EXTERNORDERKEY.Contains(rule.value));
						}
						if (rule.field == "ORDERLINENUMBER"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ORDERLINENUMBER.Contains(rule.value));
						}
						if (rule.field == "STATUS"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.STATUS.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE2.Contains(rule.value));
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
						if (rule.field == "ALTSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ALTSKU.Contains(rule.value));
						}
						if (rule.field == "ORIGINALQTY" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ORIGINALQTY == val);
                                break;
                            case "notequal":
                                And(x => x.ORIGINALQTY != val);
                                break;
                            case "less":
                                And(x => x.ORIGINALQTY < val);
                                break;
                            case "lessorequal":
                                And(x => x.ORIGINALQTY <= val);
                                break;
                            case "greater":
                                And(x => x.ORIGINALQTY > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ORIGINALQTY >= val);
                                break;
                            default:
                                And(x => x.ORIGINALQTY == val);
                                break;
                        }
						}
						if (rule.field == "UMO"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.UMO.Contains(rule.value));
						}
						if (rule.field == "CASECNT" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.CASECNT == val);
                                break;
                            case "notequal":
                                And(x => x.CASECNT != val);
                                break;
                            case "less":
                                And(x => x.CASECNT < val);
                                break;
                            case "lessorequal":
                                And(x => x.CASECNT <= val);
                                break;
                            case "greater":
                                And(x => x.CASECNT > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.CASECNT >= val);
                                break;
                            default:
                                And(x => x.CASECNT == val);
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
						if (rule.field == "SHIPPEDQTY" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.SHIPPEDQTY == val);
                                break;
                            case "notequal":
                                And(x => x.SHIPPEDQTY != val);
                                break;
                            case "less":
                                And(x => x.SHIPPEDQTY < val);
                                break;
                            case "lessorequal":
                                And(x => x.SHIPPEDQTY <= val);
                                break;
                            case "greater":
                                And(x => x.SHIPPEDQTY > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.SHIPPEDQTY >= val);
                                break;
                            default:
                                And(x => x.SHIPPEDQTY == val);
                                break;
                        }
						}
						if (rule.field == "QTYPICKED" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.QTYPICKED == val);
                                break;
                            case "notequal":
                                And(x => x.QTYPICKED != val);
                                break;
                            case "less":
                                And(x => x.QTYPICKED < val);
                                break;
                            case "lessorequal":
                                And(x => x.QTYPICKED <= val);
                                break;
                            case "greater":
                                And(x => x.QTYPICKED > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.QTYPICKED >= val);
                                break;
                            default:
                                And(x => x.QTYPICKED == val);
                                break;
                        }
						}
						if (rule.field == "PALLET" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PALLET == val);
                                break;
                            case "notequal":
                                And(x => x.PALLET != val);
                                break;
                            case "less":
                                And(x => x.PALLET < val);
                                break;
                            case "lessorequal":
                                And(x => x.PALLET <= val);
                                break;
                            case "greater":
                                And(x => x.PALLET > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PALLET >= val);
                                break;
                            default:
                                And(x => x.PALLET == val);
                                break;
                        }
						}
						if (rule.field == "PACKKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PACKKEY.Contains(rule.value));
						}
						if (rule.field == "GROSSWGT" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.GROSSWGT == val);
                                break;
                            case "notequal":
                                And(x => x.GROSSWGT != val);
                                break;
                            case "less":
                                And(x => x.GROSSWGT < val);
                                break;
                            case "lessorequal":
                                And(x => x.GROSSWGT <= val);
                                break;
                            case "greater":
                                And(x => x.GROSSWGT > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.GROSSWGT >= val);
                                break;
                            default:
                                And(x => x.GROSSWGT == val);
                                break;
                        }
						}
						if (rule.field == "NETWGT" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.NETWGT == val);
                                break;
                            case "notequal":
                                And(x => x.NETWGT != val);
                                break;
                            case "less":
                                And(x => x.NETWGT < val);
                                break;
                            case "lessorequal":
                                And(x => x.NETWGT <= val);
                                break;
                            case "greater":
                                And(x => x.NETWGT > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.NETWGT >= val);
                                break;
                            default:
                                And(x => x.NETWGT == val);
                                break;
                        }
						}
						if (rule.field == "CUBE" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.CUBE == val);
                                break;
                            case "notequal":
                                And(x => x.CUBE != val);
                                break;
                            case "less":
                                And(x => x.CUBE < val);
                                break;
                            case "lessorequal":
                                And(x => x.CUBE <= val);
                                break;
                            case "greater":
                                And(x => x.CUBE > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.CUBE >= val);
                                break;
                            default:
                                And(x => x.CUBE == val);
                                break;
                        }
						}
						if (rule.field == "NOTES"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.NOTES.Contains(rule.value));
						}
						if (rule.field == "PQCQTYINSPECTED" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PQCQTYINSPECTED == val);
                                break;
                            case "notequal":
                                And(x => x.PQCQTYINSPECTED != val);
                                break;
                            case "less":
                                And(x => x.PQCQTYINSPECTED < val);
                                break;
                            case "lessorequal":
                                And(x => x.PQCQTYINSPECTED <= val);
                                break;
                            case "greater":
                                And(x => x.PQCQTYINSPECTED > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PQCQTYINSPECTED >= val);
                                break;
                            default:
                                And(x => x.PQCQTYINSPECTED == val);
                                break;
                        }
						}
						if (rule.field == "PQCQTYREJECTED" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PQCQTYREJECTED == val);
                                break;
                            case "notequal":
                                And(x => x.PQCQTYREJECTED != val);
                                break;
                            case "less":
                                And(x => x.PQCQTYREJECTED < val);
                                break;
                            case "lessorequal":
                                And(x => x.PQCQTYREJECTED <= val);
                                break;
                            case "greater":
                                And(x => x.PQCQTYREJECTED > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PQCQTYREJECTED >= val);
                                break;
                            default:
                                And(x => x.PQCQTYREJECTED == val);
                                break;
                        }
						}
						if (rule.field == "PQCSTATUS"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PQCSTATUS.Contains(rule.value));
						}
						if (rule.field == "PQCWHO"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PQCWHO.Contains(rule.value));
						}
						if (rule.field == "PQCDATE" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.PQCDATE) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.PQCDATE) <= 0);
						    }
                        }
						if (rule.field == "LOTTABLE3"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE3.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE1.Contains(rule.value));
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
						if (rule.field == "EXTERNLINENO"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EXTERNLINENO.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE4"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE4.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE5"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE5.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE6" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.LOTTABLE6 == val);
                                break;
                            case "notequal":
                                And(x => x.LOTTABLE6 != val);
                                break;
                            case "less":
                                And(x => x.LOTTABLE6 < val);
                                break;
                            case "lessorequal":
                                And(x => x.LOTTABLE6 <= val);
                                break;
                            case "greater":
                                And(x => x.LOTTABLE6 > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.LOTTABLE6 >= val);
                                break;
                            default:
                                And(x => x.LOTTABLE6 == val);
                                break;
                        }
						}
						if (rule.field == "LOTTABLE7" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.LOTTABLE7 == val);
                                break;
                            case "notequal":
                                And(x => x.LOTTABLE7 != val);
                                break;
                            case "less":
                                And(x => x.LOTTABLE7 < val);
                                break;
                            case "lessorequal":
                                And(x => x.LOTTABLE7 <= val);
                                break;
                            case "greater":
                                And(x => x.LOTTABLE7 > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.LOTTABLE7 >= val);
                                break;
                            default:
                                And(x => x.LOTTABLE7 == val);
                                break;
                        }
						}
						if (rule.field == "LOTTABLE8" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.LOTTABLE8) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.LOTTABLE8) <= 0);
						    }
                        }
						if (rule.field == "ORDERID" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							int val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.ORDERID == val);
                                break;
                            case "notequal":
                                And(x => x.ORDERID != val);
                                break;
                            case "less":
                                And(x => x.ORDERID < val);
                                break;
                            case "lessorequal":
                                And(x => x.ORDERID <= val);
                                break;
                            case "greater":
                                And(x => x.ORDERID > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.ORDERID >= val);
                                break;
                            default:
                                And(x => x.ORDERID == val);
                                break;
                        }
						}
               }
            }
            return this;
         }    
    }
}
