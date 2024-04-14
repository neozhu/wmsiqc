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
/// File: STOCKQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 2019/6/26 13:33:27
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class STOCKQuery:QueryObject<STOCK>
   {
		public STOCKQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "WHSEID"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WHSEID.Contains(rule.value));
						}
						if (rule.field == "LOT"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOT.Contains(rule.value));
						}
						if (rule.field == "EXTERNRECEIPTKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EXTERNRECEIPTKEY.Contains(rule.value));
						}
						if (rule.field == "POKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.POKEY.Contains(rule.value));
						}
						if (rule.field == "STORERKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.STORERKEY.Contains(rule.value));
						}
						if (rule.field == "RECEIPTDATE" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.RECEIPTDATE) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.RECEIPTDATE) <= 0);
						    }
						}
						if (rule.field == "LOC"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOC.Contains(rule.value));
						}
						if (rule.field == "LPN"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LPN.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE3"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE3.Contains(rule.value));
						}
						if (rule.field == "STATUS"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.STATUS.Contains(rule.value));
						}
						if (rule.field == "SKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SKU.Contains(rule.value));
						}
						if (rule.field == "SKUNAME"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SKUNAME.Contains(rule.value));
						}
						if (rule.field == "QTY" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.QTY == val);
                                break;
                            case "notequal":
                                And(x => x.QTY != val);
                                break;
                            case "less":
                                And(x => x.QTY < val);
                                break;
                            case "lessorequal":
                                And(x => x.QTY <= val);
                                break;
                            case "greater":
                                And(x => x.QTY > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.QTY >= val);
                                break;
                            default:
                                And(x => x.QTY == val);
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
						if (rule.field == "RECEIPTKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.RECEIPTKEY.Contains(rule.value));
						}
						if (rule.field == "ALTSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ALTSKU.Contains(rule.value));
						}
						if (rule.field == "SUPPLIERCODE"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUPPLIERCODE.Contains(rule.value));
						}
						if (rule.field == "SUPPLIERNAME"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SUPPLIERNAME.Contains(rule.value));
						}
						if (rule.field == "INPUTDATETIME" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.INPUTDATETIME) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.INPUTDATETIME) <= 0);
						    }
						}
						if (rule.field == "EXTERNORDERKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.EXTERNORDERKEY.Contains(rule.value));
						}
						if (rule.field == "PICKDETAILKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PICKDETAILKEY.Contains(rule.value));
						}
						if (rule.field == "ORDERKEY"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ORDERKEY.Contains(rule.value));
						}
						if (rule.field == "OUTPUTDATETIME" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.OUTPUTDATETIME) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.OUTPUTDATETIME) <= 0);
						    }
						}
						if (rule.field == "NOTES"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.NOTES.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE1.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE2.Contains(rule.value));
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
						if (rule.field == "LOTTABLE9"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE9.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE10"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE10.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE11"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE11.Contains(rule.value));
						}
						if (rule.field == "LOTTABLE12"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LOTTABLE12.Contains(rule.value));
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
