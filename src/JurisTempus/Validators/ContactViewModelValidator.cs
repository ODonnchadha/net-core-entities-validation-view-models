using FluentValidation;
using JurisTempus.ViewModels;

namespace JurisTempus.Validators
{
  public class ContactViewModelValidator : AbstractValidator<ContactViewModel>
  {
    public ContactViewModelValidator()
    {
      RuleFor(vm => vm.Email).NotEmpty().EmailAddress();
      RuleFor(vm => vm.Subject).NotEmpty().MaximumLength(100);
      RuleFor(vm => vm.Message).NotEmpty().MaximumLength(4000);
    }
  }
}
