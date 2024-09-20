using Consume.Models;
using Consume.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Consume.Controllers
{
    /// <summary>
    /// ProductController, kullanıcıların ürün verileriyle etkileşimde bulunmasını sağlar. Kimlik doğrulama ve yetkilendirme mekanizmaları sayesinde, sadece yetkili kullanıcıların ürün ekleme, güncelleme ve silme işlemleri yapması sağlanır. 
    /// </summary>
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
		/// Tüm Ürünleri Getirir
		/// </summary>
		/// <returns></returns>
		///
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                var products = await _productService.GetProductsAsync(token);
                return View(products);
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, "Ürünleri alırken bir hata oluştu: " + ex.Message);
                return View(new List<Product>());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Bilinmeyen bir hata oluştu: " + ex.Message);
                return View(new List<Product>());
            }

        }

        /// <summary>
        /// Yeni bir ürün ekler
        /// </summary>
        /// <param name="productDto">Yeni ürün bilgilerini içeren DTO.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Yeni bir ürün ekler
        /// </summary>
        /// <param name="productDto">Yeni ürün bilgilerini içeren DTO.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var token = HttpContext.Session.GetString("Token");
            await _productService.AddProductAsync(product, token);
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Ürün güncelleme yazdırma sayfası
        /// </summary>
        /// <param name="id">Güncellenecek ürün ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("Token");

            if (!User.IsInRole("Admin"))
                return RedirectToAction("Unauthorized", "Auth");

            var products = await _productService.GetProductsAsync(token);
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// Var olan bir ürünü günceller.
        /// </summary>

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            var token = HttpContext.Session.GetString("Token");

            if (!User.IsInRole("Admin"))
                return RedirectToAction("Unauthorized", "Auth");

            await _productService.UpdateProductAsync(product, token);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Belirtilen bir ürünü siler.
        /// </summary>
        /// <param name="id">Silinecek ürünün ID'si.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("Token");

            if (!User.IsInRole("Admin"))
                return RedirectToAction("Unauthorized", "Auth");

            await _productService.DeleteProductAsync(id, token);
            return RedirectToAction("Index");
        }
    }
}
