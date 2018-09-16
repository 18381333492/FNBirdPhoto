using Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FNBirdPhoto.Web.Controllers
{
    public class DefaultController : BaseController<IUser>
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}