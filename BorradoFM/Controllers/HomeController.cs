using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BorradoFM.Models;
using RestSharp;
using Newtonsoft.Json;

namespace BorradoFM.Controllers
{
    public class HomeController : Controller
    {
        string privateToken = "";
        bool flag = true;
   
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task Borrar([FromBody] Entidad entidad)
        {
            flag = true;

            privateToken = await tokenReq(entidad.Payload);

            int id = Convert.ToInt32(entidad.Id ?? "0");

            while (flag)
            {
                await borrado(entidad,privateToken,id);

                id++;
            }
        }

        [HttpPost]
        public async Task Detener()
        {
            await Task.Delay(100);

            flag = false;

            Debug.WriteLine("Servicio detenido");

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        async Task<string> borrado(Entidad entidad,string ptoken,int id)
        {
            string result = "";

            string tabla = entidad.EntidadString;
       
            await Task.Run(()=> {
                try
                {
                    var client =
                     new RestClient($"https://api.forcemanager.net/api/v4/{tabla}/{id}");

                    var request = new RestRequest(Method.DELETE);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "*/*");
                    request.AddHeader("X-Session-Key", $"{ptoken}");
                    var body = @"";
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    result = response.Content;

                    Debug.WriteLine(result+"--"+id);

                }
                catch (Exception ex)
                {
                    result = ex.ToString();

                    Debug.WriteLine(result);
                }
            });


            return result;
        }


         async Task<string> tokenReq(Payload payload)
        {
            string result = "";

            await Task.Run(()=> {
                try
                {
                    var client = new RestClient("https://api.forcemanager.net/api/v4/login");

                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "*/*");
                    var body = JsonConvert.SerializeObject(payload, Formatting.Indented);
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    if (response.Content.Contains("token"))
                    {
                        Token token = JsonConvert.DeserializeObject<Token>(response.Content);

                        result = token.token;
                    }

                }
                catch (Exception ex)
                {
                }
            });
           

            return result;
        }
    }
}
