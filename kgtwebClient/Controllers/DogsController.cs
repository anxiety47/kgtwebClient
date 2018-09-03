﻿using kgtwebClient.Models;
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
        HttpClient client;
        //The URL of the WEB API Service
        string url = "http://kgt.azurewebsites.net/api/employee/";


        // GET: Employees
        public async Task<ActionResult> Index()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<List<EmployeeInfo>>(responseData);

                //return View(Employees);
                //ViewBag.Persons = data;

                var employeesList = new ListOfEmployeeInfo
                {
                    listOfEmployee = Employees
                };

                ViewBag.RawData = responseData;
                //ViewBag.Employees = Employees;


                return View(employeesList);
            }
            return View();
        }
    }
}