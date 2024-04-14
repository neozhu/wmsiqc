using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using LazyCache;
using SqlHelper2;

namespace WebApp.Controllers
{
  [Authorize]
  [RoutePrefix("Home")]
  public class HomeController : Controller
  {
    private readonly IAppCache cache;
    private readonly IDatabaseAsync db;
    public HomeController(IAppCache cache) {

      this.cache = cache;
      this.db = SqlHelper2.DatabaseFactory.CreateDatabase();


    }
    [HttpGet]
    public async Task<JsonResult> GetTotalData() {
      var sql1 = "select count(1) from [dbo].[RECEIPTDETAILs] where [STATUS]!='103'";
      var sql2 = "select count(1) from [dbo].[ORDERDETAILs] where [STATUS] < '107'";
      var sql3 = "select count(1) from [dbo].[ORDERDETAILs] where [STATUS] < '107' and [DELIVERYDATE] < GETDATE()";
      var sql4 = "select sum(qty) from [dbo].[STOCKs] where [STATUS] = '104'";
      var total1 =await Task.Run(()=>  this.db.ExecuteScalar<int>(sql1));
      var total2 = await Task.Run(() => this.db.ExecuteScalar<int>(sql2));
      var total3 = await Task.Run(() => this.db.ExecuteScalar<int>(sql3));
      var total4 = await Task.Run(() => this.db.ExecuteScalar<decimal>(sql4));
      return Json(new { total1, total2, total3, total4 }, JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    public async Task<JsonResult> GetTodayData()
    {
      var sql10 = "select count(1) from [dbo].[RECEIPTDETAILs] where [STATUS]='103' and CAST([RECEIPTDATE] as DATE)  = CAST(GETDATE() AS DATE)";
      var sql11 = "select count(1) from [dbo].[RECEIPTDETAILs] where  CAST([RECEIPTDATE] as DATE)  = CAST(GETDATE() AS DATE)";
      var sql20 = "select count(1) from [dbo].[ORDERDETAILs] where [STATUS] = '108' and CAST([REQUESTEDSHIPDATE] as DATE)  = CAST(GETDATE() AS DATE)";
      var sql21 = "select count(1) from [dbo].[ORDERDETAILs] where CAST([REQUESTEDSHIPDATE] as DATE)  = CAST(GETDATE() AS DATE)";
      var sql22 = "select count(1) from [dbo].[ORDERDETAILs] where [STATUS] in ( '106','107','108') and CAST([REQUESTEDSHIPDATE] as DATE)  = CAST(GETDATE() AS DATE)";
      var sql40 = "select count(1) from [dbo].[STOCKs] where [STATUS] in ('104','105') and CAST([INPUTDATETIME] as DATE)  = CAST(GETDATE() AS DATE)";
      var sql41 = "select count(1) from [dbo].[STOCKs] where [STATUS] in ('103','104','105') and CAST([INPUTDATETIME] as DATE)  = CAST(GETDATE() AS DATE)";
      var total1 = await Task.Run(() => this.db.ExecuteScalar<int>(sql10));
      var total2 = await Task.Run(() => this.db.ExecuteScalar<int>(sql11));
      var total3 = await Task.Run(() => this.db.ExecuteScalar<int>(sql20));
      var total4 = await Task.Run(() => this.db.ExecuteScalar<int>(sql21));
      var total5 = await Task.Run(() => this.db.ExecuteScalar<int>(sql22));
      var total6 = await Task.Run(() => this.db.ExecuteScalar<int>(sql40));
      var total7 = await Task.Run(() => this.db.ExecuteScalar<int>(sql41));

    
      return Json(new { total1, total2, total3, total4, total5, total6, total7 }, JsonRequestBehavior.AllowGet);
    }

    public async Task<ActionResult> Index()
    {
      

      //var user = Auth.CurrentApplicationUser;
      //throw new Exception();
      //string subjectString = "validType:'length[0,50]'";
      //var match = Regex.Split(subjectString, @"\D+", RegexOptions.IgnorePatternWhitespace).Where(x=>!string.IsNullOrEmpty(x)).ToArray();

      //  SqlHelper2.DatabaseFactory.CreateDatabase().ExecuteNonQuery(sql, parameters);

      //  var sqllist = new List<string>();
      //  sqllist.Add("INSERT INTO [dbo].[Tb1]([f1]) VALUES ('a')");
      //  sqllist.Add("INSERT INTO [dbo].[Tb1]([f1]) VALUES ('b')");
      //  sqllist.Add("INSERT INTO [dbo].[Tb1]([f1]) VALUES ('c')");
      //  SqlHelper2.DatabaseFactory.CreateDatabase().ExecuteNonQuery(sqllist);
      //  //var ds= SqlHelper2.DatabaseFactory.CreateDatabase().ExecuteDataSet("select * from tb1",null);
      //  SqlHelper2.DatabaseFactory.CreateDatabase().ExecuteDataSet("select * from tb1", null,ds=> {
      //    Console.Write(ds);
      //});
      //  SqlHelper2.DatabaseFactory.CreateDatabase().ExecuteDataTable("select * from tb1", null, dt => {
      //      Console.Write(dt);
      //  });

      await SqlHelper2.DatabaseFactory.CreateDatabase().ExecuteDataReaderAsync("select * from tb1", null, dr =>
       {
         Console.WriteLine(dr[0]);
       });
      var data = await SqlHelper2.DatabaseFactory.CreateDatabase().ExecuteDataReaderAsync<string>("select * from tb1", null, dr =>
       {
         return dr[0].ToString();
         ;
       });
      foreach (var item in data)
      {
        Console.WriteLine(item);
      }
      //var list = CodeListSet.CLS["AccountType"].EnumRecords();
      //var val = CodeListSet.CLS["AccountType"].Code2Value("1");
      return this.View();
    }

    public ActionResult About()
    {
      this.ViewBag.Message = "Your application description page.";

      return this.View();
    }

    public ActionResult GetTime() =>
        //ViewBag.Message = "Your application description page.";

        this.View();
    public ActionResult BlankPage() => this.View();
    public ActionResult AgileBoard() => this.View();


    public ActionResult Contact()
    {
      this.ViewBag.Message = "Your contact page.";

      return this.View();
    }
    public ActionResult Chat() => this.View();




  }
}