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
using System.Linq;

namespace SolforbTest.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IBaseService<OrderItemDTO> _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _appEnvironment;

        public OrderItemsController(ILogger<OrderItemsController> logger, IOrderItemService service, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderItemModel, OrderItemDTO>();
                cfg.CreateMap<OrderItemDTO, OrderItemModel>();
            });

            _mapper = config.CreateMapper();

            _service = service;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public ActionResult Index(int id)
        {
            ViewData["OrderId"] = id;
            var options = new OrderItemFilterOption()
            {
                OrderIds = new List<int>()
            };

            options.OrderIds.Add(id);
            return View(GetAll(options));
        }

        [HttpGet]
        public IEnumerable<OrderItemModel> GetAll(OrderItemFilterOption options)
        {
            var result = _mapper.Map<IEnumerable<OrderItemDTO>, IEnumerable<OrderItemModel>>(_service.GetAll());

            if (options != null)
                result = result
                    .Where(x => options.OrderIds == null || options.OrderIds.Contains(x.OrderId))
                    .ToList();

            return result;
        }

        [HttpGet]
        public IActionResult Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["OrderId"] = id;

            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, decimal quantity, string unit, int orderId)
        {
            _service.Add(new OrderItemDTO()
            {
                Name = name,
                Quantity = quantity,
                Unit = unit,
                OrderId = orderId
            });

            return RedirectToAction("Index", new { id = orderId });
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
        public IActionResult Edit(int id, string name, decimal quantity, string unit, int orderId)
        {
            var orderItem = _service.Find((int)id);

            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.Name = name;
            orderItem.Quantity = quantity;
            orderItem.Unit = unit;

            _service.Update(orderItem);

            return RedirectToAction("Index", new { id = orderId });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = _service.Find((int)id);
            var orderId = orderItem.OrderId;

            if (orderItem == null)
            {
                return NotFound();
            }

            _service.Delete(orderItem);

            return RedirectToAction("Index", new { id = orderId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class OrderItemFilterOption
    {
        public List<int> Ids;
        public List<int> OrderIds;
    }
}
