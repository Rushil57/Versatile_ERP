using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DAL.Abstract;
using VTPL_ERP.Models;
using VTPL_ERP.Util;
using System.Net.Mail;
using System.Net;

namespace VTPL_ERP.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userData;
        private readonly IRolesAndRightsMasterService _rolesandrightsMasterData;
        public AccountController(IUserService userData, IRolesAndRightsMasterService rolesandrightsMasterData)
        {
            _userData = userData;
            _rolesandrightsMasterData = rolesandrightsMasterData;
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
        public IActionResult Login()
        {
            ViewData["Message"] = "Your Login page.";
            return View();
        }
        public async Task<JsonResult> LoginUser(UserViewModel model)
        {
            JsonResponseData retObj = new JsonResponseData();
            ERP_Utitlity erpUtil = new ERP_Utitlity();
            var encryptPswd = erpUtil.Encrypt(model.Password);
            // var encryptPswd = Encrypt(model.Password);
            var employee = await _userData.GetUserByEmailPswd(model.Email, encryptPswd);
            if (employee != null)
            {
                HttpContext.Session.SetObject(AppConstants.SessionKey.CURRENT_USER, employee);
                var listRolesAndRights = await _rolesandrightsMasterData.GetRolesAndRightsByRoleId(employee.RoleId);
                HttpContext.Session.SetObject(AppConstants.SessionKey.CURRENT_USER_RIGHTS, listRolesAndRights);
                retObj.IsError = false;
            }
            else
            {
                retObj.IsError = true;
            }
            return Json(retObj);
        }
        public async Task<JsonResult> SendEmail(String EmailId)
        {
            JsonResponseData retObj = new JsonResponseData();
            try
            {
                var userObj = await _userData.GetUserByEmail(EmailId);
                if (userObj != null)
                {
                    ERP_Utitlity erpUtil = new ERP_Utitlity();
                    var decryptPswd = erpUtil.Decrypt(userObj.Password);
                    var senderEmail = new MailAddress("kenygoswami30@gmail.com", "Versatile Technology");
                    var receiverEmail = new MailAddress(EmailId, "Receiver");
                    var password = "9974135248";
                    var sub = "Password Detail";
                    var body = "your Password is:" + decryptPswd + "";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    retObj.IsError = false;
                    retObj.SuccessMessage = "Password had been send to your Email";
                }
                else
                {
                    retObj.IsError = true;
                    retObj.ErrorMessage = "Email data is not available.";
                }
                return Json(retObj);
            }
            catch (Exception ex)
            {
                retObj.IsError = true;
                retObj.ErrorMessage = ex.Message;
                return Json(retObj);
            }
            
        }
    }
}