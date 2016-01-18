using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OwnerPasswordCredentialsGrant.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            using (var proxy = new HttpClient())
            {
                proxy.BaseAddress = new Uri("http://localhost:13168/");
                var clientId = "1234";
                var clientSecret = "5678";
                proxy.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

                var parameters = new Dictionary<string, string>();
                parameters.Add("grant_type", "client_credentials");

                var result = proxy.PostAsync("/token", new FormUrlEncodedContent(parameters)).Result;
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ViewBag.Ticket = result.Content.ReadAsStringAsync().Result;
                }
            }

            return View();
        }
    }
}