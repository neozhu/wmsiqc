﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
  public static class KeyGenerator
  {
    public static string NextVersion()
    {
      return DateTime.Now.ToString("yyyyMMddHHmmss");
    }
    //获取自定义流水号
    public static string GetNextCustomsKey()
    {
      var db = SqlHelper2.DatabaseFactory.CreateDatabase();
      //通过MS SQL Sequence产生递增序列
      var result = db.ExecuteScalar<object>("SELECT NEXT VALUE FOR [dbo].[KeySequence3]");
      return Convert.ToInt32(result).ToString("00000000");


    }
    public static string GetNextLotKey()
    {
      var db = SqlHelper2.DatabaseFactory.CreateDatabase();
      //通过MS SQL Sequence产生递增序列
      var result = db.ExecuteScalar<object>("SELECT NEXT VALUE FOR [dbo].[KeySequence3]");
      return Convert.ToInt32(result).ToString("00000000");


    }
    public static string GetNextPickKey()
    {
      var db = SqlHelper2.DatabaseFactory.CreateDatabase();
      //通过MS SQL Sequence产生递增序列
      var result = db.ExecuteScalar<object>("SELECT NEXT VALUE FOR [dbo].[KeySequence4]");
      return Convert.ToInt32(result).ToString("00000000");


    }
    public static string GetNextReceiptKey()
    {
      var db = SqlHelper2.DatabaseFactory.CreateDatabase();
      //通过MS SQL Sequence产生递增序列
      var result = db.ExecuteScalar<object>("SELECT NEXT VALUE FOR [dbo].[KeySequence1]");
      return Convert.ToInt32(result).ToString("00000000");


    }
    public static string GetNextSOKey()
    {
      var db = SqlHelper2.DatabaseFactory.CreateDatabase();
      //通过MS SQL Sequence产生递增序列
      var result = db.ExecuteScalar<object>("SELECT NEXT VALUE FOR [dbo].[KeySequence2]");
      return Convert.ToInt32(result).ToString("00000000");


    }
  }
}