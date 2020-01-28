using Microsoft.AspNetCore.Mvc;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Common.Models;
using ProgramAcad.Common.Models.PagedList;
using ProgramAcad.Common.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tango.Types;

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

        private IActionResult MethodWhenSome() => IsValidResponse ? NoContent() : ResponseBadRequest();
        private IActionResult MethodWhenSome(object obj) => IsValidResponse ? Ok(obj) : ResponseBadRequest();
        private IActionResult MethodWhenNone() { return IsValidResponse ? NotFound() : ResponseBadRequest(); }
        private IActionResult MethodWhenSome(string uri, object obj) => IsValidResponse ? Created(uri, obj) : ResponseBadRequest();

        protected bool IsValidResponse => !_notifyManager.HasNotifications();

        protected virtual async Task Notify(string reason, string details)
        {
            await _notifyManager.Notify(reason, details);
        }

        protected Response<T, object> GetOkResponse<T>(T data) where T : class =>
            new Response<T, object>(true, data, null);

        protected IEnumerable<ExpectedError> GetBadRequestResponse()
        {
            var errors = _notifyManager.GetNotifications()
                .Select(notif => new ExpectedError(notif.Reason, notif.Details));

            return errors;
        }

        protected IActionResult ResponseBadRequest()
        {
            return BadRequest(GetBadRequestResponse());
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected new IActionResult Response(Option<object> result)
        {
            return result.Match(MethodWhenSome, MethodWhenNone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult ResponseCreated(string uri, Option<object> result)
        {
            return result.Match(value => MethodWhenSome(uri, value), MethodWhenNone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult ResponseCreated<T>(string uri, IEnumerable<T> result)
        {
            return result.Match(value => MethodWhenSome(uri, value), MethodWhenNone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult ResponseNoContent()
        {
            return MethodWhenSome();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagedList"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected new IActionResult Response<T>(IPagedList<T> pagedList)
        {
            return pagedList.Match(MethodWhenSome, MethodWhenNone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected new IActionResult Response<T>(IEnumerable<T> enumerable)
        {
            return enumerable.Match(MethodWhenSome, MethodWhenNone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected new IActionResult Response<T>(IEnumerator<T> enumerable)
        {
            return enumerable.Match(MethodWhenSome, MethodWhenNone);
        }
    }
}
