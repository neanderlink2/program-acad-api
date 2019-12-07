using Microsoft.AspNetCore.Mvc;
using ProgramAcad.Common.Models;
using ProgramAcad.Common.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgramAcad.API.Presentation.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class ApiBaseController : Controller
    {
        protected readonly DomainNotificationManager _notifyManager;

        protected ApiBaseController(DomainNotificationManager notifyManager)
        {
            _notifyManager = notifyManager;
        }

        public bool IsValidResponse => !_notifyManager.HasNotifications();

        protected new IActionResult Response<T>(T response = null) where T : class
        {
            if (IsValidResponse)
                return Ok(GetOkResponse<T>(response));

            return BadRequest(GetBadRequestResponse());
        }

        protected virtual async Task Notify(string reason, string details)
        {
            await _notifyManager.Notify(reason, details);
        }

        protected Response<T, object> GetOkResponse<T>(T data) where T : class =>
            new Response<T, object>(true, data, null);

        protected Response<object, IEnumerable<ExpectedError>> GetBadRequestResponse()
        {
            var errors = _notifyManager.GetNotifications()
                .Select(notif => new ExpectedError(HttpStatusCode.BadRequest, notif.Reason, notif.Details));

            return new Response<object, IEnumerable<ExpectedError>>(false, null, errors);
        }
    }
}
