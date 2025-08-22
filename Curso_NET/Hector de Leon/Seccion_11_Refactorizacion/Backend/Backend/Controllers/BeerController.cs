using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Modelos;
using Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System;
using FluentValidation;
using Backend.Services; // Para usar FluentValidation, debes instalar el paquete NuGet "FluentValidation.AspNetCore" en tu proyecto. Esta librería te permite definir reglas de validación para tus modelos de forma fluida y sencilla.

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private IBeerService _beerService;

        public BeerController(
            IValidator<BeerInsertDto> beerInsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator,
            IBeerService beerService)
        {
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _beerService.Get();

        // Indica que este método responde a peticiones HTTP GET 
        // en la ruta "api/Beer/{id}" (ej: api/Beer/5)
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _beerService.GetById(id);

            return beerDto == null 
                ? NotFound() // Si no se encuentra la cerveza, devuelve 404 Not Found
                : Ok(beerDto); // Si se encuentra, devuelve 200 OK con el DTO de la cerveza
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            // Las validaciones de formato/campo se quedan en el controlador
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var beerDto = await _beerService.Add(beerInsertDto);

            return CreatedAtAction(
                nameof(GetById), 
                new { id = beerDto.Id },
                beerDto
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!validationResult.IsValid) 
                return BadRequest(validationResult.Errors);

            var beerDto = await _beerService.Update(id, beerUpdateDto);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }
            
        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _beerService.Delete(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }
    }
}
