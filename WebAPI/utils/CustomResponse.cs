using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cronicle.utils
{
    public class CustomResponse
    {
        private object data { get; set; }

        private string message { get; set; }

        private int status { get; set; }


        public static IActionResult OK(object _data) {

            return new OkObjectResult(new CustomResponse() {
                data = _data,
                message = "Data fetched Successfully",
                status = 200
            });
        }


        public static IActionResult OK()
        {

            return new OkObjectResult(new CustomResponse()
            {
                message = "Request Processed Successfully",
                status = 200
            });
        }


        public static IActionResult NotFound()
        {

            return new NotFoundObjectResult(new CustomResponse()
            {
                message = "Requested Resource Not Found",
                status = 404
            });
        }

        public static IActionResult BadRequest()
        {

            return new NotFoundObjectResult(new CustomResponse()
            {
                message = "Bad Request",
                status = 400
            });
        }


        public static IActionResult UnAuthorized()
        {

            return new UnauthorizedResult();
               
        }

    }
}
