using FluentValidation;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator() // fluent validation'da validation kodları parametresiz constr içerisinde yazılıyor
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2); //boş olamaz ve en az 2 karakter uzunluğuna sahip olmalı şartını ekledik
    }
}
