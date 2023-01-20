using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.DependencyResolver;
using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Models;

namespace PlacementManagement.Controllers
{
    public class PlacementRequestController : Controller
    {
        private readonly IPlacementRequestServices _placementRequestService;
        private readonly IMasterServices _masterService;

        public PlacementRequestController(IPlacementRequestServices placementRequestService, IMasterServices masterService)
        {
            _placementRequestService = placementRequestService;
            _masterService = masterService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var placementRequests = _placementRequestService.GetPlacementRequests();

                if (placementRequests != null)
                {
                    string departments = string.Empty;
                    string coreAreas = string.Empty;
                    var departmentMasterData = _masterService.GetDepartments();
                    var coreAreaMasterData = _masterService.GetCoreAreas();
                    foreach (var dept in placementRequests)
                    {
                        var departmentIds = dept.Departments?.Split(',');
                        departments = string.Join(", ", departmentMasterData.Where(c => departmentIds.Contains(c.Id.ToString()))
                                            .OrderBy(x => x.DepartmentName).Select(c => c.DepartmentName).ToList());
                        dept.Departments = departments;

                        var coreAreaIds = dept.CoreAreas?.Split(',');
                        coreAreas = string.Join(", ", coreAreaMasterData.Where(c => coreAreaIds.Contains(c.Id.ToString()))
                                            .OrderBy(x => x.CoreArea).Select(c => c.CoreArea).ToList());

                        dept.CoreAreas = coreAreas;
                    }
                }
                return View(placementRequests);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "PlacementRequest");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _masterService.GetDepartments();
            var coreAreas = _masterService.GetCoreAreas();

            List<long> departmentIds = new List<long>();
            List<long> coreAreaIds = new List<long>();

            PlacementRequestViewModel placementRequestView = new PlacementRequestViewModel();
            placementRequestView.PlacementDate = DateTime.Now.Date.AddDays(1);
            placementRequestView.DepartmentDetails = departments.Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.Id.ToString() }).ToList();
            placementRequestView.CoreAreaDetails = coreAreas.Select(x => new SelectListItem { Text = x.CoreArea, Value = x.Id.ToString() }).ToList();
            placementRequestView.DepartmentIds = departmentIds.ToArray();
            placementRequestView.CoreAreaIds = coreAreaIds.ToArray();
            return View(placementRequestView);
        }

        [HttpPost]
        public IActionResult Create(PlacementRequestViewModel model)
        {
            PlacementRequestViewModel placementRequestView = new PlacementRequestViewModel();

            try
            {
                var departments = _masterService.GetDepartments();
                var coreAreas = _masterService.GetCoreAreas();

                List<long> departmentIds = new List<long>();
                List<long> coreAreaIds = new List<long>();

                placementRequestView.PlacementDate = DateTime.Now.Date.AddDays(1);
                placementRequestView.DepartmentDetails = departments.Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.Id.ToString() }).ToList();
                placementRequestView.CoreAreaDetails = coreAreas.Select(x => new SelectListItem { Text = x.CoreArea, Value = x.Id.ToString() }).ToList();
                placementRequestView.DepartmentIds = departmentIds.ToArray();
                placementRequestView.CoreAreaIds = coreAreaIds.ToArray();

                if (model.PlacementDate < DateTime.Now.Date.AddDays(1))
                {
                    TempData["ErrorMessage"] = "Placement date should be greater than current date.";
                    return View(placementRequestView);
                }

                if (model.DepartmentIds?.Length > 0)
                {
                    model.Departments = String.Join(",", model.DepartmentIds);
                }
                else
                {
                    TempData["ErrorMessage"] = "Minimum one Department is required.";
                    return View(placementRequestView);
                }
                if (model.CoreAreaIds?.Length > 0)
                {
                    model.CoreAreas = String.Join(",", model.CoreAreaIds);
                }
                else
                {
                    TempData["ErrorMessage"] = "Minimum one Core Area is required.";
                    return View(placementRequestView);
                }

                ModelState.Remove("Departments");
                ModelState.Remove("CoreAreas");

                model.CollegeId = 1;

                if (ModelState.IsValid)
                {
                    _placementRequestService.AddorEditPlacementRequest(model);
                    TempData["SuccessMessage"] = "Placement request created.";
                    return RedirectToAction("Index");
                }
                return View(placementRequestView);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(placementRequestView);
            }
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                var placementRequest = _placementRequestService.GetPlacementRequestById(Id);

                if (placementRequest != null)
                {
                    var departments = _masterService.GetDepartments();
                    var coreAreas = _masterService.GetCoreAreas();

                    List<long> departmentIds = new List<long>();
                    List<long> coreAreaIds = new List<long>();

                    placementRequest.DepartmentDetails = departments.Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.Id.ToString() }).ToList();
                    placementRequest.CoreAreaDetails = coreAreas.Select(x => new SelectListItem { Text = x.CoreArea, Value = x.Id.ToString() }).ToList();
                    placementRequest.DepartmentIds = placementRequest.Departments?.Split(',').Select(long.Parse).ToArray();
                    placementRequest.CoreAreaIds = placementRequest.CoreAreas?.Split(',').Select(long.Parse).ToArray();
                    return View(placementRequest);
                }
                else
                {
                    TempData["ErrorMessage"] = $"Placement request not found with ID: {Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(PlacementRequestViewModel model)
        {
            PlacementRequestViewModel placementRequestView = new PlacementRequestViewModel();

            try
            {
                var departments = _masterService.GetDepartments();
                var coreAreas = _masterService.GetCoreAreas();

                List<long> departmentIds = new List<long>();
                List<long> coreAreaIds = new List<long>();

                placementRequestView.PlacementDate = DateTime.Now.Date.AddDays(1);
                placementRequestView.DepartmentDetails = departments.Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.Id.ToString() }).ToList();
                placementRequestView.CoreAreaDetails = coreAreas.Select(x => new SelectListItem { Text = x.CoreArea, Value = x.Id.ToString() }).ToList();
                placementRequestView.DepartmentIds = departmentIds.ToArray();
                placementRequestView.CoreAreaIds = coreAreaIds.ToArray();

                if (model.PlacementDate < DateTime.Now.Date.AddDays(1))
                {
                    TempData["ErrorMessage"] = "Placement date should be greater than current date.";
                    return View(placementRequestView);
                }

                if (model.DepartmentIds?.Length > 0)
                {
                    model.Departments = String.Join(",", model.DepartmentIds);
                }
                else
                {
                    TempData["ErrorMessage"] = "Minimum one Department is required.";
                    return View(placementRequestView);
                }
                if (model.CoreAreaIds?.Length > 0)
                {
                    model.CoreAreas = String.Join(",", model.CoreAreaIds);
                }
                else
                {
                    TempData["ErrorMessage"] = "Minimum one Core Area is required.";
                    return View(placementRequestView);
                }

                ModelState.Remove("Departments");
                ModelState.Remove("CoreAreas");

                model.CollegeId = 1;
                if (ModelState.IsValid)
                {
                    _placementRequestService.AddorEditPlacementRequest(model);
                    TempData["SuccessMessage"] = "Placement request updated.";
                    return RedirectToAction("Index");
                }
                return View(placementRequestView);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(placementRequestView);
            }
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var placementRequest = _placementRequestService.GetPlacementRequestById(Id);
                if (placementRequest != null)
                {
                    var departments = _masterService.GetDepartments();
                    var coreAreas = _masterService.GetCoreAreas();

                    List<long> departmentIds = new List<long>();
                    List<long> coreAreaIds = new List<long>();

                    placementRequest.DepartmentDetails = departments.Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.Id.ToString() }).ToList();
                    placementRequest.CoreAreaDetails = coreAreas.Select(x => new SelectListItem { Text = x.CoreArea, Value = x.Id.ToString() }).ToList();
                    placementRequest.DepartmentIds = placementRequest.Departments?.Split(',').Select(long.Parse).ToArray();
                    placementRequest.CoreAreaIds = placementRequest.CoreAreas?.Split(',').Select(long.Parse).ToArray();
                    return View(placementRequest);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            var placementRequest = _placementRequestService.GetPlacementRequestById(Id);

            try
            {
                if (placementRequest != null)
                {
                    var departments = _masterService.GetDepartments();
                    var coreAreas = _masterService.GetCoreAreas();

                    List<long> departmentIds = new List<long>();
                    List<long> coreAreaIds = new List<long>();

                    placementRequest.DepartmentDetails = departments.Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.Id.ToString() }).ToList();
                    placementRequest.CoreAreaDetails = coreAreas.Select(x => new SelectListItem { Text = x.CoreArea, Value = x.Id.ToString() }).ToList();
                    placementRequest.DepartmentIds = placementRequest.Departments?.Split(',').Select(long.Parse).ToArray();
                    placementRequest.CoreAreaIds = placementRequest.CoreAreas?.Split(',').Select(long.Parse).ToArray();
                }

                bool isDeleted = _placementRequestService.DeletePlacementRequest(Id);
                if (isDeleted)
                {
                    TempData["SuccessMessage"] = "Placement request deleted.";
                    return RedirectToAction("Index");
                }
                return View(placementRequest);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(placementRequest);
            }
        }
    }
}
