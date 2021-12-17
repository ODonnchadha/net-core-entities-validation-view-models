using FluentValidation;
using JurisTempus.Data;
using JurisTempus.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JurisTempus.Validators
{
  public class ClientViewModelValidator : AbstractValidator<ClientViewModel>
  {
    public ClientViewModelValidator(BillingContext context)
    {
      RuleFor(c => c.Name).MinimumLength(5).MaximumLength(100).MustAsync(
        async (model, value, token) =>
        {
          return !(await context.Clients.AnyAsync(c => c.Name == value && c.Id != model.Id));
        }).WithMessage("Name must be unique.");

      RuleFor(c => c.ContactName).MaximumLength(100);

      When(c => !string.IsNullOrEmpty(c.Phone) || !string.IsNullOrEmpty(c.ContactName), () => {
        RuleFor(c => c.Phone).NotEmpty().WithMessage("Phone cannot be empty if contact is specified.");
        RuleFor(c => c.ContactName).NotEmpty().WithMessage("Contact cannot be empty if phone is specified.");
      });
    }
  }
}
