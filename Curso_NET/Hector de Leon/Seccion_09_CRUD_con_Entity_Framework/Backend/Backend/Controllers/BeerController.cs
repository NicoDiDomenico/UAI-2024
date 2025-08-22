using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Modelos;
using Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System;
using FluentValidation; // Para usar FluentValidation, debes instalar el paquete NuGet "FluentValidation.AspNetCore" en tu proyecto. Esta librería te permite definir reglas de validación para tus modelos de forma fluida y sencilla.

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _context;
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;

        public BeerController(StoreContext context, 
            IValidator<BeerInsertDto> beerInsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator)
        {
            _context = context;
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get()
        {
            return await _context.Beers.Select(b => new BeerDto
            {
                Id = b.BeerID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = b.BrandID
            }).ToListAsync();
        }

        // Indica que este método responde a peticiones HTTP GET 
        // en la ruta "api/Beer/{id}" (ej: api/Beer/5)
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            // Busca en la tabla "Beers" el registro cuya clave primaria (BeerID) sea igual a "id"
            // - Primero revisa si ya está en memoria del DbContext
            // - Si no está, hace un SELECT en la base de datos
            // Devuelve "null" si no existe
            var beer = await _context.Beers.FindAsync(id);

            // Si no se encontró ninguna cerveza con ese id, devuelve un 404 Not Found
            if (beer == null)
            {
                return NotFound();
            }

            // Si se encontró la cerveza, la convertimos en un DTO para devolver solo lo que interesa al frontend
            // Acá estás mapeando (Beer -> BeerDto)
            var beerDto = new BeerDto
            {
                Id = beer.BeerID,     // Asignás la PK de la entidad Beer a la propiedad Id del DTO
                Name = beer.Name,     // Asignás el nombre
                Alcohol = beer.Alcohol, // Asignás el grado de alcohol
                BrandID = beer.BrandID // Asignás la clave foránea de la marca
            };

            // Devuelve un resultado 200 OK con el objeto beerDto en el body de la respuesta
            return Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            // Validamos el DTO de entrada usando FluentValidation
            if (!validationResult.IsValid)
            {
                // Si la validación falla, devolvemos un 400 Bad Request con los errores
                return BadRequest(validationResult.Errors);
            }

            // 1) Creás una nueva entidad Beer a partir del DTO de entrada
            var beer = new Beer
            {
                Name = beerInsertDto.Name,
                Alcohol = beerInsertDto.Alcohol,
                BrandID = beerInsertDto.BrandID
            };

            // 2) Le decís a EF Core que agregue la entidad a la base (aún no se guarda)
            await _context.Beers.AddAsync(beer); // Acá le agrega una PK a la cerveza nueva, que es un entero autoincremental (Identity) en la base de datos

            // 3) Guardás los cambios -> acá se ejecuta el INSERT en la DB
            // y automáticamente se genera el BeerID (PK autoincremental)
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos, sin esta linea no se hacen los cambio

            // 4) Armás el DTO de salida para devolverlo al frontend
            var beerDto = new BeerDto
            {
                Id = beer.BeerID, // Asignás la PK generada por la base de datos gracias a AddAsync
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            // 5) Respondés con 201 Created - CreatedAtAction es otro helper (si no llevaba param la ruta entonces no hacia falta el new {} en la firma)
            return CreatedAtAction(
                nameof(GetById), // Nombre del método que recupera un recurso por id --> api/Beer/{id}
                new { id = beer.BeerID }, // Valores de ruta necesarios para armar la URL de ese recurso --> api/Beer/4   
                beerDto // El objeto que devolvés en el body
            );
            // Diferencias:
            // Ok(...) → devuelve 200 OK, usado para respuestas normales(consultas, listas, etc).
            // CreatedAtAction(...) → devuelve 201 Created + la URL del recurso recién creado, ideal para POST.
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!validationResult.IsValid)
            {
                // Si la validación falla, devolvemos un 400 Bad Request con los errores
                return BadRequest(validationResult.Errors);
            }

            // Busca la cerveza por su ID
            var beer = await _context.Beers.FindAsync(id);

            // Si no se encuentra, devuelve un 404 Not Found
            if (beer == null)
            {
                return NotFound();
            }

            // Actualiza los campos de la cerveza con los datos del DTO
            beer.Name = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;
            beer.BrandID = beerUpdateDto.BrandID;

            // Marca la entidad como modificada (opcional, EF lo hace automáticamente al cambiar propiedades)
            await _context.SaveChangesAsync();

            // Crea un DTO para devolver la cerveza actualizada. Recordar que DTO es Data Transfer Object, un objeto que se usa para transferir datos entre procesos.
            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            // Devuelve un 200 OK con el DTO actualizado
            return Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            // Elimina la cerveza del contexto
            _context.Beers.Remove(beer);

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
