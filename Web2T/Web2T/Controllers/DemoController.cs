using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Net.Http;
using Web2T.Models;
using Web2T.ModelViews;
using Web2T.Extension;
using Web2T.Helpper;
using PagedList;
using Microsoft.EntityFrameworkCore;
using System.Data;

//namespace Web2T.Controllers
//{
    //public class DemoController : Controller
    //{
       // private readonly DbMarketsContext _context;
        //public INotyfService _notyfService { get; }
        //private readonly IHttpClientFactory _httpClientFactory;
        //public DemoController(IHttpClientFactory httpClientFactory, DbMarketsContext context, INotyfService notyfService)
        //{
            //_context = context;
            //_httpClientFactory = httpClientFactory;
            //_notyfService = notyfService;
        //}

        //public IActionResult Index()
        //{
            //var cities = _context.Locations.AsNoTracking().Where(x => x.Levels == 1).ToList();
                //foreach (var city in cities)
            //{
                //var client = _httpClientFactory.CreateClient();
                //client.BaseAddress = new Uri("https://provinces.open-api.vn/api/?depth=2");
                //var response = client.GetAsync($"https://provinces.open-api.vn/api/?depth=2").Result;
                //string jsonData = response.Content.ReadAsStringAsync().Result;
                //var collection = JsonConvert.DeserializeObject<List<DemoVM>>(jsonData);

                //foreach (var item in collection)
                //{
                    //Location location = new Location();
                    //location.Code = item.code;
                    // tên Hà nội -- > TP.HÀ NỘI
                    // đồng nai --- > TỈNH đỒNG NAI
                    // bÌNH tHẠNH -- > qUẬN BÌNH THẠNH
                   // location.Levels = 2;
                    //location.ParentCode = city.LocationId; // tỈNH /THÀNH
                    //location.Name = item.name;
                    //location.Slug = Helpper.Utilities.SEOUrl(location.Name);
                    //_context.Locations.Add(location);
                    //_context.SaveChanges();
               // }

            //}

            
            
                

               
            
                
                
            

            

            // Đi lây quan hueyn
            //var cities = _context.Locations.AsNoTracking().Where(x=>x.Levels== 1).ToList();

            //foreach (var city in cities)
            //{
            // ds quan huyen Goi api de lay quan huyen
            // ds quanhuye
            //foreach (var item in collection)
            //{
            //Location location = new Location();
            //location.ParentCode = city.locationid;
            //location.Levels = 2;
            //location.name = city.locationid;
            //_context.Locations.Add(location);
            //_context.SaveChanges();
            //}
            //}


            // Đi lây phuong xa
            //var cities = _context.Locations.AsNoTracking().Where(x => x.Levels == 1).ToList();

            //foreach (var city in cities)
            //{
            // ds quan huyen Goi api de lay quan huyen
            // ds quanhuye
            //foreach (var item in collection)
            //{
            //Location location = new Location();
            //location.ParentCode = city.locationid;
            //location.Levels = 3;
            //location.name = city.locationid;
            //_context.Locations.Add(location);
            //_context.SaveChanges();
            //}
            //}
            // qUAN HUYỆN
            /*
                location.Levels = 2;
               location.ParentCode = 0; // Thuộc tỉnh  LocationID Tỉnh

             */

            // phường xã
            /*
                location.Levels = 3;
               location.ParentCode = 0; // // Thuộc tỉnh  LocationID HUYỆN

             */
            //return View();
        //}

        // 
    //}
//}

