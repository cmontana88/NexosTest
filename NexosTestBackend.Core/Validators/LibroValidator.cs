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
    public class LibroValidator : AbstractValidator<LibroDto>
    {
        public LibroValidator(IHttpContextAccessor context)
        {
            if (context.HttpContext.Request.Method == HttpMethods.Put)
                RuleFor(e => e.Id).Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("El {PropertyName} es requerido.")
                    .NotEmpty().WithMessage("El {PropertyName} es requerido.")
                    .GreaterThan(0).WithMessage("El id debe ser mayor a 0");

            if (context.HttpContext.Request.Method == HttpMethods.Post)
                RuleFor(e => e.Id).Cascade(CascadeMode.Stop)
                    .Empty().WithMessage("No se debe enviar el id.");

            RuleFor(e => e.Titulo).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El {PropertyName} es requerido.")
                .NotEmpty().WithMessage("El {PropertyName} es requerido.")
                .Length(2, 100).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");

            RuleFor(e => e.Genero).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El {PropertyName} es requerido.")
                .NotEmpty().WithMessage("El {PropertyName} es requerido.")
                .Length(2, 50).WithMessage("El {PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");

            RuleFor(e => e.NumeroPaginas).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El número de páginas es requerido.")
                .NotEmpty().WithMessage("El número de páginas es requerido.")
                .GreaterThan(0).WithMessage("El número de páginas ser mayor a 0");

            RuleFor(e => e.IdEditorial).Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("La {PropertyName} es requerido.")
                    .NotEmpty().WithMessage("La {PropertyName} es requerido.")
                    .GreaterThan(0).WithMessage("La Editorial debe ser mayor a 0");

            RuleFor(e => e.IdAutor).Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("El {PropertyName} es requerido.")
                    .NotEmpty().WithMessage("El {PropertyName} es requerido.")
                    .GreaterThan(0).WithMessage("El Autor debe ser mayor a 0");
        }
    }
}
