using FluentValidation;
using JurisTempus.ViewModels;

namespace JurisTempus.Validators
{
  public class TimeBillViewModelValidator : AbstractValidator<TimeBillViewModel>
  {
    public TimeBillViewModelValidator()
    {
      RuleFor(vm => vm.Rate).InclusiveBetween(0m, 500m);
      RuleFor(vm => vm.TimeSegments).GreaterThan(0);
      RuleFor(vm => vm.WorkDescription).MinimumLength(25);
      RuleFor(vm => vm.CaseId).NotEmpty();
      RuleFor(vm => vm.EmployeeId).NotEmpty();
    }
  }
}
