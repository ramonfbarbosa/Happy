using FluentValidation;
using OrfanatoAPI.Models;

namespace OrfanatoAPI.Validators;

public class OrfanatoValidator : AbstractValidator<Orphanage>
{
    public OrfanatoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O campo nome não pode ficar vazio!")
            .NotNull()
            .WithMessage("O campo nome não pode ser nulo!");
        RuleFor(x => x.Whatsapp)
            .NotEmpty()
            .WithMessage("O campo whatsapp não pode ficar vazio!")
            .NotNull()
            .WithMessage("O campo whatsapp não pode ser nulo!");
        RuleFor(x => x.Latitude)
            .NotEqual(0)
            .WithMessage("O campo latitude não pode ficar vazio!");
        RuleFor(x => x.Longitude)
            .NotEqual(0)
            .WithMessage("O campo longitude não pode ficar vazio!");
        RuleFor(x => x.About)
            .NotEmpty()
            .WithMessage("O campo sobre não pode ficar vazio!")
            .NotNull()
            .WithMessage("O campo sobre não pode ser nulo!");
        RuleFor(x => x.Instructions)
            .NotEmpty()
            .WithMessage("O campo instrucoes não pode ficar vazio!")
            .NotNull()
            .WithMessage("O campo instrucoes não pode ser nulo!");
        RuleFor(x => x.AbreAs)
            .NotEmpty()
            .WithMessage("O campo hora de abertura não pode ficar vazio!")
            .NotNull()
            .WithMessage("O campo hora de abertura não pode ser nulo!");
        RuleFor(x => x.FechaAs)
            .NotEmpty()
            .WithMessage("O campo hora de abertura não pode ficar vazio!")
            .NotNull()
            .WithMessage("O campo hora de abertura não pode ser nulo!");
    }
}
