using AutoMapper;
using FluentValidation.Results;
using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Notifications;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Common
{
    public abstract class AppService
    {
        protected readonly IMapper _mapper;
        protected readonly DomainNotificationManager _notifyManager;

        protected AppService(IMapper mapper, DomainNotificationManager notifyManager)
        {
            _mapper = mapper;
            _notifyManager = notifyManager;
        }

        protected virtual Task NotifyAsync(string reason, string details)
        {
            return NotifyAsync(new DomainNotification(reason, details));
        }

        protected virtual Task NotifyAsync(DomainNotification notification)
        {
            return _notifyManager.Notify(notification);
        }

        protected virtual async Task NotifyValidationErrorsAsync(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                await NotifyAsync(NotifyReasons.VALIDATION_ERROR, error.ErrorMessage);
            }
        }
    }
}
