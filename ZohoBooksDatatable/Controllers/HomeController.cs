using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZohoBooksDatatable.Models;
using ZohoBooksDatatable.Models.Datatables;
using ZohoBooksProxy.Services;
using ZohoBooksProxy.Models;

namespace ZohoBooksDatatable.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Invoices()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ListInvoices([FromBody]DTParameters param)
        {
            var service = new BooksInvoiceService("995ffd119b5a0d6a4690d4e98bdc2c5e", "653437843");
            var requestParams = new Dictionary<string, string>()
                {
                    //{ "customer_id", bookUser.ContactId},
                    //{ "status", "unpaid" },
                    { "page", param.Start.ToString() },
                    { "per_page", param.Length.ToString() }
                };


            var invoice = await service.ListAsync(requestParams);

            DTResult<Invoice> result = new DTResult<Invoice>
            {
                draw = param.Draw,
                data = invoice.Invoices,
                recordsFiltered = 300,
                recordsTotal = 300
            };

            return Json(result);
            
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
