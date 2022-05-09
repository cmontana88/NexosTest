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
    public class AutorValidator : AbstractValidator<AutorDto>
    {
        public AutorValidator(IHttpContextAccessor context)
        {
            if (context.HttpContext.Request.Method == HttpMethods.Put)
                RuleFor(a => a.Id).Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("El {PropertyName} es requerido.")
                .NotEmpty().WithMessage("El {PropertyName} es requerido.")
                    .GreaterThan(0).WithMessage("El id debe ser mayor a 0");

            if (context.HttpContext.Request.Method == HttpMethods.Post)
                RuleFor(a => a.Id).Cascade(CascadeMode.Stop)
                    .Empty().WithMessage("No se debe enviar el id.");

            RuleFor(a => a.Nombre).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El {PropertyName} es requerido.")
                .NotEmpty().WithMessage("El {PropertyName} es requerido.")
                .Length(2, 200).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");

            RuleFor(a => a.CiudadNacimiento).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("La {PropertyName} es requerida.")
                .NotEmpty().WithMessage("La {PropertyName} es requerida.")
                .Length(2, 100).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");

            RuleFor(a => a.Email).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El {PropertyName} es requerido.")
                .NotEmpty().WithMessage("El {PropertyName} es requerido.")
                .Length(2, 200).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.")
                .EmailAddress().WithMessage("Se requiere un {PropertyName} válido");

            RuleFor(u => u.FechaNacimiento).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("La fecha de nacimiento no puede ser vacío.")
                .NotEmpty().WithMessage("La fecha de nacimiento no puede ser vacío.")
                .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento no puede ser mayor a la fecha actual.")
                .Must(IsOver18).WithMessage("El autor debe ser mayor de edad."); 
        }

        private bool IsOver18(DateTime birthDate)
        {
            return DateTime.Now.AddYears(-18) >= birthDate;
        }
    }
}
