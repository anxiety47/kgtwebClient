using kgtwebClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace kgtwebClient.Controllers
{
    public class DogsController : Controller
    {

        //The URL of the WEB API Service
        static string url = "http://kgt.azurewebsites.net/api/";
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri(url) };


        // GET: Employees
        public async Task<ActionResult> Index()
        {
            //client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = await client.GetAsync("dogs/");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var dogs = JsonConvert.DeserializeObject<List<Dog>>(responseData);

                //return View(Employees);
                //ViewBag.Persons = data;

                var dogsList = new DogsList
                {
                    ListOfDogs = dogs
                };

                ViewBag.RawData = responseData;
                //ViewBag.Employees = Employees;


                return View(dogsList);
            }
            return View();
        }

        public bool DeleteDog(int? id)
        {
            //client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            /* dla put i post:
            httpmethod.put i httpmethod.post
            message.Content = new StringContent(***object-json-serialized***, 
                                                System.Text.Encoding.UTF8, "application/json");
             */
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, client.BaseAddress +"dogs/" + id.ToString());
            message.Content = new StringContent(id.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.SendAsync(message).Result;
            if (responseMessage.IsSuccessStatusCode)    //200 OK
            {
                //wyswietlić informację
                message.Dispose();
                return true;
            }
            else    // wiadomosc czego się nie udałos
            {
                message.Dispose();
                return false;
            }

        }

        public bool UpdateDog(int? id)
        {
            //client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            /* dla put(update) i post(add):
            httpmethod.put i httpmethod.post
            message.Content = new StringContent(***object-json-serialized***, 
                                                System.Text.Encoding.UTF8, "application/json");
             */
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, client.BaseAddress + "dogs/?id=" + id.ToString());
            var dog = new Dog
            {
                DogID = 9,
                Name = "Łapczor",
                DateOfBirth = new DateTime(2010, 1, 1),
                Level = 1,
                Workmode = new List<Object>(),
                Notes = "Notatki",
                Guide = 1
            };

            var dogSerialized = JsonConvert.SerializeObject(dog);
            

            message.Content = new StringContent(dogSerialized /*id.ToString()*/, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.SendAsync(message).Result;
            if (responseMessage.IsSuccessStatusCode)    //200 OK
            {
                //wyswietlić informację
                message.Dispose();
                return true;
            }
            else    // wiadomosc czego się nie udałos
            {
                message.Dispose();
                return false;
            }

        }
    }
}