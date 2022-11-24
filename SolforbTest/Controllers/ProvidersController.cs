using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolforbTest.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace SolforbTest.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly ILogger<ProvidersController> _logger;
        private readonly IBaseReadonlyService<ProviderDTO> _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProvidersController(ILogger<ProvidersController> logger, IProviderService service, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProviderModel, ProviderDTO>();
                cfg.CreateMap<ProviderDTO, ProviderModel>();
            });

            _mapper = config.CreateMapper();

            _service = service;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(GetAll());
        }

        [HttpGet]
        public IEnumerable<ProviderModel> GetAll()
        {
            var result = _mapper.Map<IEnumerable<ProviderDTO>, IEnumerable<ProviderModel>>(_service.GetAll());

            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
