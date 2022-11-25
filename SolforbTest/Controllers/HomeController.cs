using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolforbTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SolforbTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBaseService<OrderDTO> _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeController(ILogger<HomeController> logger, IOrderService service, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderModel, OrderDTO>();
                cfg.CreateMap<OrderDTO, OrderModel>();
            });

            _mapper = config.CreateMapper();

            _service = service;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public ActionResult Index(DateTime? startDate, DateTime? endDate, string number)
        {
            var options = new OrderFilterOption();

            if (startDate != null)
            {
                options.StartDates = new List<DateTime>
                {
                    (DateTime)startDate
                };
            }

            if (endDate != null)
            {
                options.EndDates = new List<DateTime>
                {
                    (DateTime)endDate
                };
            }

            if (number != null)
            {
                options.Numbers = new List<string>
                {
                    number
                };
            }

            return View(GetAll(options));
        }

        [HttpGet]
        public IEnumerable<OrderModel> GetAll(OrderFilterOption options)
        {
            var result = _mapper.Map<IEnumerable<OrderDTO>, IEnumerable<OrderModel>>(_service.GetAll());

            if (options != null)
                result = result
                    .Where(x => options.StartDates == null || options.StartDates.Any(y => y <= x.Date))
                    .Where(x => options.EndDates == null || options.EndDates.Any(y => x.Date >= y))
                    .Where(x => options.Numbers == null || options.Numbers.Contains(x.Number))
                    .ToList();

            return result;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string number, DateTime date, int providerId)
        {
            _service.Add(new OrderDTO()
            {
                Number = number,
                Date = date,
                ProviderId = providerId,
            });

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var orderItem = _service.Find((int)id);

            if (orderItem == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        [HttpPost]
        public IActionResult Edit(int id, string number, DateTime date, int providerId)
        {
            var orderItem = _service.Find((int)id);

            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.Number = number;
            orderItem.Date = date;
            orderItem.ProviderId = providerId;

            _service.Update(orderItem);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = _service.Find((int)id);

            if (orderItem == null)
            {
                return NotFound();
            }

            _service.Delete(orderItem);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class OrderFilterOption
    {
        public List<int> Ids;
        public List<DateTime> StartDates;
        public List<DateTime> EndDates;
        public List<string> Numbers;
        public List<int> ProviderIds;
    }
}
