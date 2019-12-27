using FluentValidation.Results;
using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Workers;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Common
{
    public abstract class Command<TCommandModel>
    {
        protected readonly DomainNotificationManager _notifyManager;
        protected readonly IUnitOfWork _unitOfWork;

        protected Command(DomainNotificationManager notifyManager, IUnitOfWork unitOfWork)
        {
            _notifyManager = notifyManager;
            _unitOfWork = unitOfWork;
        }

        protected virtual Task NotifyAsync(string reason, string details)
        {
            return _notifyManager.Notify(new DomainNotification(reason, details));
        }

        protected virtual async Task NotifyValidationErrorsAsync(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                await NotifyAsync(NotifyReasons.VALIDATION_ERROR, error.ErrorMessage);
            }
        }

        protected virtual Task<bool> CommitChangesAsync()
        {
            return _unitOfWork.Commit();
        }

        public abstract Task<bool> ExecuteAsync(TCommandModel commandModel);
    }
}
