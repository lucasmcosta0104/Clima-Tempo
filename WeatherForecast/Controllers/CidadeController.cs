﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeatherForecast.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        public ActionResult Index()
        {
            return View();
        }
    }
}