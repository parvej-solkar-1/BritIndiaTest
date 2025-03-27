using FluentValidation;
using ProductServices.DTOs.Product;

namespace ProductServices.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator(IHttpContextAccessor httpContextAccessor)
        {
            var method = httpContextAccessor.HttpContext.Request.Method.ToUpper();

            //RuleFor(x => x.Id)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .Must(x => !x.HasValue)
            //    .When(x => method == "POST")
            //    .WithMessage(x => $"'{nameof(x.Id)}' is not allowed.")
            //    .Must(x => x.HasValue && x.Value > 0)
            //    .When(x => method == "PATCH" || method == "DELETE", ApplyConditionTo.CurrentValidator)
            //    .WithMessage(x => $"'{nameof(x.Id)}' is required.");

            When(x => method == "POST" || method == "PATCH" || method == "DELETE", () =>
            {
                RuleFor(x => x.Id)
                    .Must(x => !x.HasValue)
                    .WithMessage(x => $"'{nameof(x.Id)}' is not allowed.");

                RuleFor(x => x.CreatedBy)
                .Must(x => x == null)
                .WithMessage(x => $"'{nameof(x.CreatedBy)}' is not allowed.");

                RuleFor(x => x.CreatedOn)
                .Must(x => !x.HasValue)
                .WithMessage(x => $"'{nameof(x.CreatedOn)}' is not allowed.");

                RuleFor(x => x.ModifiedBy)
                .Must(x => x == null)
                .WithMessage(x => $"'{nameof(x.ModifiedBy)}' is not allowed.");

                RuleFor(x => x.ModifiedOn)
                .Must(x => x == null)
                .WithMessage(x => $"'{nameof(x.ModifiedOn)}' is not allowed.");
            });
        }
    }
}
