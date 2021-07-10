using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cronicle.DBContexts;
using Cronicle.Models;
using Cronicle.Services;
using Cronicle.utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cronicle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IEmailService mailService;
        private ProjectDbContext DBContext;
        public ForgotPasswordController(ProjectDbContext context,IEmailService mailService)
        {
            this.mailService = mailService;
            DBContext = context;
        }


        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendMail([FromBody] EmailInfo emailInfo)
        {
            try
            {
                var user = DBContext.Users.Where(x => x.Email == emailInfo.EmailTo).FirstOrDefault();

                if (user != null) {
                    Random generator = new Random();
                    string random = generator.Next(0, 1000000).ToString("D6");
                    user.resetCode = random;

                    DBContext.Attach(user);
                    DBContext.Entry(user).Property(p => p.resetCode).IsModified = true;
                    DBContext.SaveChanges();

                    emailInfo.Subject = "Reset password";
                    emailInfo.Body = $"Your password reset code is {random}";
                    await mailService.SendEmailAsync(emailInfo);
                    return CustomResponse.OK();
                }
                else {

                    return CustomResponse.BadRequest();
                }
               
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("resetPassword")]
        public IActionResult changePass([FromBody] ResetPassword payload) {
            try
            {
                var user = DBContext.Users.Where(x => x.resetCode == payload.code).FirstOrDefault();

                if (user != null)
                {
                    user.Password = Helpers.GetHash(payload.newPassword + user.Salt);
                    user.resetCode = "";
                    DBContext.Attach(user);
                    DBContext.Entry(user).Property(p => p.Password).IsModified = true;
                    DBContext.Entry(user).Property(p => p.resetCode).IsModified = true;
                    DBContext.SaveChanges();
                    return CustomResponse.OK();
                }
                else
                {

                    return CustomResponse.BadRequest();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }


}