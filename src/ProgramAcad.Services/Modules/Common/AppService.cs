using AutoMapper;
using FluentValidation.Results;
using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Workers;
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

        protected virtual Task Notify(string key, string value)
        {
            return Notify(new DomainNotification(key, value));
        }

        protected virtual Task Notify(DomainNotification notification)
        {
            return _notifyManager.Notify(notification);
        }

        protected virtual async Task NotifyValidationErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                await Notify(NotifyReasons.VALIDATION_ERROR, error.ErrorMessage);
            }
        }
    }
}
