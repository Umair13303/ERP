//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using OrganisationSetup.Areas.Inventory.Services;
//using OrganisationSetup.Services;
//using SharedUI.Models.Contexts;
//using SharedUI.Models.Enums;
//using SharedUI.Models.SQLParameters;
//using System.Collections.Generic;

//namespace OrganisationSetup.Areas.Inventory.Controllers
//{
//    [Authorize]
//    [Area(nameof(SetupRoute.Area.Inventory))]
//    public class POSManagementController : Controller
//    {
//        private readonly ICommon _commonsServices;
//        private readonly TempUser _currentUser;
//        private readonly IInventoryRetriever _irService;
//        private readonly IPOSService _posService;

//        public POSManagementController(ICommon commonsServices, TempUser currentUser, IInventoryRetriever irService, IPOSService posService)
//        {
//            _commonsServices = commonsServices;
//            _currentUser = currentUser;
//            _irService = irService;
//            _posService = posService;
//        }

//        #region RENDER VIEWS
//        [HttpGet]
//        public IActionResult PointOfSaleWindow()
//        {
//            ViewBag.LocationId = _currentUser.BranchId;
//            return View();
//        }
//        #endregion

//        #region POS DATA ENDPOINTS
//        [HttpGet]
//        public async Task<IActionResult> getProductsForPOS()
//        {
//            var products = await _posService.GetProductsWithStockAndPricing(_currentUser.BranchId, _currentUser.CompanyId);
//            return Json(products);
//        }

//        [HttpPost]
//        public async Task<IActionResult> recordPOSSale([FromBody] POSSaleRequest posSaleRequest)
//        {
//            if (!ModelState.IsValid)
//                return Json(new { IsSuccess = false, message = "Invalid request data", statusCode = 400 });

//            var result = await _posService.RecordPOSSale(posSaleRequest, _currentUser);
//            return Json(new { result.IsSuccess, message = result.Message, statusCode = result.StatusCode, data = result.DocumentNumber });
//        }

//        [HttpGet]
//        public async Task<IActionResult> getProductsBySearch(string searchTerm)
//        {
//            var products = await _posService.SearchProducts(searchTerm, _currentUser.BranchId, _currentUser.CompanyId);
//            return Json(products);
//        }

//        [HttpGet]
//        public async Task<IActionResult> getProductDetails(int productId)
//        {
//            var product = await _posService.GetProductDetails(productId, _currentUser.BranchId);
//            return Json(product);
//        }
//        #endregion

//        #region INVENTORY ADJUSTMENT
//        [HttpPost]
//        public async Task<IActionResult> createUpdateInventoryAdjustment([FromBody] PostedData postedData)
//        {
//            if (!ModelState.IsValid)
//                return Json(new { IsSuccess = false, message = "Invalid data", statusCode = 400 });

//            var result = await _posService.CreateInventoryAdjustment(postedData, _currentUser);
//            return Json(new { result.IsSuccess, message = result.Message, statusCode = result.StatusCode });
//        }
//        #endregion
//    }
//}