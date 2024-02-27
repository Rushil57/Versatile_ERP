using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.DAL.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using VTPL_ERP.Util;
using static VTPL_ERP.Util.AppConstants;

namespace VTPL_ERP.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionEmployee = HttpContext.Session.GetObject<EmployeeMasterDto>(AppConstants.SessionKey.CURRENT_USER);
            Controller controller = filterContext.Controller as Controller;
            if (controller != null)
            {
                if (sessionEmployee == null)
                {
                    filterContext.Result = controller.RedirectToAction("Login", "Account");
                }
                if (sessionEmployee != null)
                {
                    //var routeUrl = Request.Path.Value;
                    //var controllername = RouteData.Values["controller"].ToString();
                    var actionname = RouteData.Values["action"].ToString().ToUpper();
                    if(actionname == "VIEWPURCHASEQUOTATION" || actionname == "PURCHASEQUOTATIONDETAILS")
                    {
                        actionname = "PURCHASEQUOTATION";
                    }
                    if (actionname == "PRINTPURCHASEENTRY" || actionname == "PURCHASEENTRYDETAILS")
                    {
                        actionname = "PURCHASEENTRY";
                    }
                    if (actionname == "PRINTPURCHASERETURNENTRY" || actionname == "PURCHASERETURNENTRYDETAILS")
                    {
                        actionname = "PURCHASERETURNENTRY";
                    }
                    if (actionname == "PRINTSALESORDER" || actionname == "SALESORDERDETAILS")
                    {
                        actionname = "SALESORDER";
                    }
                    if (actionname == "PRINTSALESENTRY" || actionname == "SALESENTRYDETAILS")
                    {
                        actionname = "SALESENTRY";
                    }
                    if (actionname == "PRINTSALESRETURNENTRY" || actionname == "SALESRETURNENTRYDETAILS")
                    {
                        actionname = "SALESRETURNENTRY";
                    }
                    if (Enum.GetNames(typeof(AppPages)).Any(x => x.ToUpper() == actionname))
                    {
                        var page = Enum.GetNames(typeof(AppPages)).Where(x => x.ToUpper() == actionname).FirstOrDefault();
                        AppPages pagename = (AppPages)Enum.Parse(typeof(AppPages), page);
                        var hasright = IsHasRight(pagename, RoleActions.IsPageVisible);
                        if(hasright == false)
                        {
                            //filterContext.Result = controller.RedirectToAction("UnauthorizedUser", "Master");
                        }
                    }
                    ViewData["username"] = sessionEmployee.FirstName + " " + sessionEmployee.LastName;
                    ViewData["PhotoUrl"] = sessionEmployee.PhotoUrl;

                }
            }

            base.OnActionExecuting(filterContext);
        }
        public bool IsHasRight(AppPages pageId, RoleActions actionId)
        {
            var pgId = (int)pageId;
            var actId = (int)actionId;

            var sessionRolesAndRights = HttpContext.Session.GetObject<List<RolesAndRightsMasterDto>>(AppConstants.SessionKey.CURRENT_USER_RIGHTS);
            var sessiondata = sessionRolesAndRights.Where(x => x.ActionId.Equals(actId) && x.PageId.Equals(pgId)).FirstOrDefault();
            if (sessiondata != null)
            {
                return sessiondata.IsRight;
            }
            else {
                return false;
            }
        }
    }
}