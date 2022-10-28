using Application.Security;
using Eshop.Common;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("Admin")]
  [CheckPermission(Permissions.Admin)]
    public class AdminBaseController : Controller
    {
        
    }
}
