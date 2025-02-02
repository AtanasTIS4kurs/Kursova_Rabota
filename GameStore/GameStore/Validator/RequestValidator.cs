using FluentValidation;
using GameStore.DL.Interface;
using GameStore.Models.Requests;

namespace GameStore.Validator
{
    internal class RequestValidator : AbstractValidator<AddGameRequest> 
    {
        private readonly ICompanyRepository _companyRepository;
        public RequestValidator(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            RuleFor(AddGameRequest => AddGameRequest.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(20)
                .WithMessage("Name must be less than 20 characters");
            RuleFor(AddGameRequest => AddGameRequest.Price)
                .NotEmpty()
                .WithMessage("Price is required")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
            RuleFor(RuleFor => RuleFor.Company)
                .NotEmpty()
                .WithMessage("Company is required")
                .Must(CompanyExists).WithMessage("Company does not exist.");
        }
        private bool CompanyExists(string companyName)
        {
            var existingCompany = _companyRepository.GetByName(companyName);
            return existingCompany != null;
        }
    }
}
