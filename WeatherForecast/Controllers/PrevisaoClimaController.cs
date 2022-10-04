using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeatherForecast.ApplicationService;
using WeatherForecast.Repository;

namespace WeatherForecast.Controllers
{
    public class PrevisaoClimaController : Controller
    {
        private PrevisaoTempoAplicationService appService;
        private UnitOfWork uow;
        public PrevisaoClimaController()
        {
            uow = new UnitOfWork();
            appService = new PrevisaoTempoAplicationService(uow);
        }

        public async Task<ActionResult> Index(CancellationToken cancellationToken = default)
        {
            await appService.VerificaBancoByCreate(cancellationToken);
            return View(await appService.GetPrevisaoCidadede(cancellationToken));
        }

        public async Task<JsonResult> GetPrevisao(int id, CancellationToken cancellationToken)
        {
            return Json(await appService.GetPrevisaoDia(id, cancellationToken), JsonRequestBehavior.AllowGet);
        }
    }
}