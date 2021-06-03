using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _config;

        public HomeController(ILogger<HomeController> logger,
                              IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            string connectionstring = _config.GetConnectionString("TestDB");

            ViewBag.Data = getdata();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public string getdata()
        {

            string tmp = string.Empty;
            try
            {
                var strCon = _config.GetConnectionString("TestDB");
                using (SqlConnection mConnection = new SqlConnection(strCon))
                {
                    using (SqlCommand command = new SqlCommand("RST_Test", mConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@UserId", UserId);
                        //command.Parameters.AddWithValue("@FromDate", FromDate);
                        //command.Parameters.AddWithValue("@ToDate", Todate);
                        mConnection.Open();
                        object scalarVal = command.ExecuteScalar();
                        tmp = (scalarVal == null) ? "" : Convert.ToString(scalarVal);
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
            return tmp;
        }
    }
}
