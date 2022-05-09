using FluentValidation;
using Microsoft.AspNetCore.Http;
using NexosTestBackend.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Core.Validators
{
    public class EditorialValidator : AbstractValidator<EditorialDto>
    {
        public EditorialValidator(IHttpContextAccessor context)
        {
            if (context.HttpContext.Request.Method == HttpMethods.Put)
                RuleFor(e => e.Id).Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("El {PropertyName} es requerido.")
                    .NotEmpty().WithMessage("El {PropertyName} es requerido.")
                    .GreaterThan(0).WithMessage("El id debe ser mayor a 0");

            if (context.HttpContext.Request.Method == HttpMethods.Post)
                RuleFor(e => e.Id).Cascade(CascadeMode.Stop)
                    .Empty().WithMessage("No se debe enviar el id.");

            RuleFor(e => e.Nombre).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El {PropertyName} es requerido.")
                .NotEmpty().WithMessage("El {PropertyName} es requerido.")
                .Length(2, 200).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");

            RuleFor(e => e.DireccionCorrespondencia).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("La dirección de correspondencia es requerida.")
                .NotEmpty().WithMessage("La dirección de correspondencia es requerida.")
                .Length(2, 100).WithMessage("La dirección de correspondencia tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");

            RuleFor(a => a.Email).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El {PropertyName} es requerido.")
                .NotEmpty().WithMessage("El {PropertyName} es requerido.")
                .Length(2, 200).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.")
                .EmailAddress().WithMessage("Se requiere un {PropertyName} válido");

            RuleFor(e => e.Telefono).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El teléfono es requerido.")
                .NotEmpty().WithMessage("El teléfono es requerido.")
                .Must(IsNumeric).WithMessage("Numero de teléfono invalido.")
                .Length(10).WithMessage("El teléfono tiene {TotalLength} números. Debe tener una longitud de 10 números."); 
            
        }

        private bool IsNumeric(string telefono)
        {
            return long.TryParse(telefono, out _);
        }
    }
}
