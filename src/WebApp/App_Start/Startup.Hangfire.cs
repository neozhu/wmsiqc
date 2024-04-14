using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using WebApp.Models;

[assembly: OwinStartup(typeof(WebApp.Startup))]
namespace WebApp
{
  public partial class Startup
  {
    // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
    public void ConfigureHangfire(IAppBuilder app)
    {
      GlobalConfiguration.Configuration
         .UseSimpleAssemblyNameTypeSerializer()
         .UseRecommendedSerializerSettings()
         .UseSqlServerStorage(
              "DefaultConnection",
              new SqlServerStorageOptions
              {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
              });



      app.UseHangfireDashboard("/hangfire", new DashboardOptions
      {
        Authorization = new[] { new NoAuthorizationFilter() }
      });
      app.UseHangfireServer();
      //每10分钟执行一个方法
      //RecurringJob.AddOrUpdate(() => this.ExecuteProcess(), Cron.Hourly);


      ////Fire - and - forget jobs are executed only once and almost immediately after creation.
      //var jobId1 = BackgroundJob.Enqueue(
      //        () => Console.WriteLine($"{DateTime.Now}:executed"));


      ////Delayed jobs are executed only once too, but not immediately, after a certain time interval.
      //var jobId2 = BackgroundJob.Schedule(
      //        () => Console.WriteLine($"{DateTime.Now}:Delayed!"),
      //    TimeSpan.FromDays(7));
    

      ////Recurring jobs fire many times on the specified CRON schedule.
      //RecurringJob.AddOrUpdate(
      //        () => this.SendStocksToSupplier(),
      //    "0 0 * * *");
      ////Continuations are executed when its parent job has been finished.
      //BackgroundJob.ContinueJobWith(
      //    jobId2,
      //    () => Console.WriteLine($"{DateTime.Now}:Continuation!"));
    }

    public void ExecuteProcess() => Console.WriteLine("{ DateTime.Now }:do something......");

    public void SendStocksToSupplier() {
      var db = new StoreContext();
      var array = new string[] { "100", "103", "104", "105", "106" };
      var ignoredColumns = new string[] {
        "ID","ALTSKU","PALLET","PACKKEY","GROSSWGT","NETWGT",
        "CUBE","STORERKEY","LPN",
        "LOTTABLE1","EXTERNLINENO","LOTTABLE2",
        "LOTTABLE5","LOTTABLE6","LOTTABLE7","LOTTABLE8","LOTTABLE10",
        "LOTTABLE11","LOTTABLE12"
      };
      var stocks = db.STOCKS.Where(x => array.Contains(x.STATUS)).ToList().Select(n => new {LOT = n.LOT,
        WHSEID = n.WHSEID,
        EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
        POKEY = n.POKEY,
        STORERKEY = n.STORERKEY,
        RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
        LOC = n.LOC,
        LPN = n.LPN,
        SUPPLIERCODE = n.SUPPLIERCODE,
        SUPPLIERNAME = n.SUPPLIERNAME,
        SKU = n.SKU,
        SKUNAME = n.SKUNAME,
        QTY = n.QTY,
        UMO = n.UMO,
        STATUS = n.STATUS,
        CASECNT = n.CASECNT,
        PALLET = n.PALLET,
        PACKKEY = n.PACKKEY,
        GROSSWGT = n.GROSSWGT,
        NETWGT = n.NETWGT,
        CUBE = n.CUBE,
        RECEIPTKEY = n.RECEIPTKEY,
        ALTSKU = n.ALTSKU,
        INPUTDATETIME = n.INPUTDATETIME.ToString("yyyy/MM/dd HH:mm:ss"),
        EXTERNORDERKEY = n.EXTERNORDERKEY,
        PICKDETAILKEY = n.PICKDETAILKEY,
        ORDERKEY = n.ORDERKEY,
        NOTES = n.NOTES,
        LOTTABLE1 = n.LOTTABLE1,
        LOTTABLE2 = n.LOTTABLE2,
        LOTTABLE4 = n.LOTTABLE4,
        LOTTABLE5 = n.LOTTABLE5,
        LOTTABLE6 = n.LOTTABLE6,
        LOTTABLE7 = n.LOTTABLE7,
        LOTTABLE3 = n.LOTTABLE3,
        LOTTABLE9 = n.LOTTABLE9,
        LOTTABLE10 = n.LOTTABLE10,
        LOTTABLE11 = n.LOTTABLE11,
        LOTTABLE12 = n.LOTTABLE12})
        .OrderBy(x => x.SUPPLIERCODE).ThenBy(x => x.SKU).ThenBy(x => x.SKU).ToList();
      var suppliercodes = stocks.Select(x => new { x.SUPPLIERCODE, x.SUPPLIERNAME }).Distinct();
      foreach (var code in suppliercodes)
      {
        var email = db.SUPPLIERS.Where(x => x.SUPPLIERCODE == code.SUPPLIERCODE).FirstOrDefault()?.EMAIL;
        if (!string.IsNullOrEmpty(email))
        {
          var sp = stocks.Where(x => x.SUPPLIERCODE == code.SUPPLIERCODE).ToList();
          var stream = ExcelHelper.ExportExcel(typeof(STOCK), sp, ignoredColumns);
          sendMail(stream, email , code.SUPPLIERNAME);
        }

      }
    

    }

    private void sendMail(Stream stream, string to,string supplier)
    {
      var stmptserver = System.Configuration.ConfigurationManager.AppSettings["smtp"].Split(':')[0];
      var port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtp"].Split(':')[1]);
      var user = System.Configuration.ConfigurationManager.AppSettings["smtpuser"];
      var password = System.Configuration.ConfigurationManager.AppSettings["smtppassword"];
      using (var mailClient = new SmtpClient(stmptserver, port))
      using (var message = new MailMessage("28440117@qq.com", to, $"[{supplier}]-[{DateTime.Now.ToString("MM-dd")}]的库存报表", "库存报表请看附件..."))
      {
        stream.Position = 0;     // read from the start of what was written
        var attachment = new Attachment(stream, $"供应商库存报表{DateTime.Now.ToString("yyyy-MM-dd")}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
          ContentType = new ContentType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        };
        message.Attachments.Add(attachment);
        mailClient.EnableSsl = true;
        mailClient.UseDefaultCredentials = true;
        mailClient.Credentials = new NetworkCredential(user, password);
        mailClient.Send(message);
      }

    }
  }

  public class NoAuthorizationFilter : IDashboardAuthorizationFilter
  {

    public NoAuthorizationFilter()
    {

    }

    public bool Authorize(DashboardContext dashboardContext) => true;
  }
}