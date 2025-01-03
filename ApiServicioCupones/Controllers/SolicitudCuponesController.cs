﻿using ApiServicioCupones.Data;
using ApiServicioCupones.Interfaces;
using ApiServicioCupones.Models;
using ApiServicioCupones.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace ApiServicioCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCuponesController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly ICuponesService _cuponesService;
        private readonly ISendEmailService _sendEmailService; 

        public SolicitudCuponesController (DataBaseContext context, ICuponesService cuponesService, ISendEmailService sendEmailService)
        {

            _context = context;
            _cuponesService = cuponesService;
            _sendEmailService = sendEmailService;
        }

        [HttpPost("SolicitarCupon")]
        public async Task<IActionResult> SolicitarCupon(ClienteDto clienteDto)
        {
            try
            {
                if (string.IsNullOrEmpty(clienteDto.CodCliente))
                {
                    Log.Error("DNI del cliente vacío.");
                    throw new Exception("El DNI del cliente no puede estar vacío");
                }

                Log.Information($"Generando número de cupón para el cliente con DNI: {clienteDto.CodCliente}");
                string nroCupon = await _cuponesService.GenerarNroCupon();

                Cupon_ClienteModel cupon_Cliente = new Cupon_ClienteModel()
                {
                    Id_Cupon = clienteDto.id_Cupon,
                    CodCliente = clienteDto.CodCliente,
                    FechaAsignado = DateTime.Now,
                    NroCupon = nroCupon
                };

                _context.Cupones_Clientes.Add(cupon_Cliente);
                await _context.SaveChangesAsync();
                Log.Information($"Cupón generado y guardado para el cliente con DNI: {clienteDto.CodCliente}, Número de cupón: {nroCupon}");

                await _sendEmailService.EnviarEmailCliente(clienteDto.Email, nroCupon);
                Log.Information($"Correo electrónico enviado a {clienteDto.Email} con el número de cupón.");

                return Ok(new
                {
                    Msj = "Se dio de alta el registro correctamente.",
                    NroCupon = nroCupon
                });
            }
            catch (Exception ex)
            {
                Log.Error($"Error al solicitar el cupón para el cliente con DNI: {clienteDto.CodCliente}, error: {ex.Message}");
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /*
        [HttpPost("QuemadoCupon")]

        public async Task<IActionResult> QuemadoCupon( string nroCupon)
        {
            /* (POST): Este endpoint debe manejar el quemado del cupón
            de un cliente tras haber sido usado por el mismo. 
            El endpoint recibirá como parámetro el número de cupón(111 - 111 - 111) que
            va a ser utilizado.
            El flujo es:
                        i.Recibir número de cupón.
                        ii.Insertar registro en Cupones_Historial.
                        iii.Eliminar registro en Cupones_Clientes.
                        iv.Devolver mensaje indicando que el cupón fue utilizado correctamente.
          
        }*/

        /*
        [HttpPost("QuemarCupon")] //QuemarCupon tiene muchisimos logs/debug porque era propenso a tirar problemas.
        public async Task<IActionResult> QuemarCupon(string nroCupon)
        {
            try
            {
                Console.WriteLine($"Iniciando quemado de cupón con número: {nroCupon}");

                var cupon = await _context.Cupones_Clientes
                                           .FirstOrDefaultAsync(c => c.NroCupon == nroCupon);

                if (cupon == null)
                {
                    Console.WriteLine("El cupón no existe en Cupones_Clientes.");
                    return BadRequest("El cupón no existe.");
                }

                Console.WriteLine("Cupón encontrado en Cupones_Clientes. Verificando historial...");
                var historial = await _context.Cupones_Historial
                                               .FirstOrDefaultAsync(h => h.NroCupon == nroCupon);

                if (historial != null)
                {
                    Console.WriteLine("El cupón ya ha sido utilizado según el historial.");
                    return BadRequest("El cupón ya ha sido utilizado.");
                }

                Console.WriteLine("Registrando el uso del cupón en Cupones_Historial...");
                Cupon_HistorialModel nuevoHistorial = new Cupon_HistorialModel()
                {
                    Id_Cupon = cupon.Id_Cupon,
                    NroCupon = cupon.NroCupon,
                    FechaUso = DateTime.Now,
                    CodCliente = cupon.CodCliente
                };

                _context.Cupones_Historial.Add(nuevoHistorial);

                Console.WriteLine("Eliminando cupón de Cupones_Clientes...");
                _context.Cupones_Clientes.Remove(cupon);

                Console.WriteLine("Guardando cambios en la base de datos...");
                await _context.SaveChangesAsync();

                Console.WriteLine("Operación de quemado de cupón completada con éxito.");
                return Ok(new { mensaje = "El cupón fue utilizado correctamente." });
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"DbUpdateException: {dbEx.Message}");
                if (dbEx.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {dbEx.InnerException.Message}");
                    if (dbEx.InnerException.InnerException != null)
                    {
                        Console.WriteLine($"Inner Inner Exception: {dbEx.InnerException.InnerException.Message}");
                    }
                }
                return BadRequest($"Database Error: {dbEx.Message} | Inner: {dbEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return BadRequest($"Error: {ex.Message} | Inner: {ex.InnerException?.Message}");
            }
        }
        */

        [HttpPost("QuemarCupon")]
        public async Task<IActionResult> QuemarCupon(string nroCupon)
        {
            try
            {
                var historial = await _context.Cupones_Historial
                                               .FirstOrDefaultAsync(h => h.NroCupon == nroCupon);

                if (historial != null)
                {
                    Log.Information($"El cupón {nroCupon} ya ha sido utilizado.");
                    return BadRequest("El cupón ya ha sido utilizado.");
                }

                var cupon = await _context.Cupones_Clientes
                                           .FirstOrDefaultAsync(c => c.NroCupon == nroCupon);

                if (cupon == null)
                {
                    Log.Information($"El cupón {nroCupon} no existe o ya fue utilizado.");
                    return BadRequest("El cupón no existe o ya fue utilizado.");
                }

                Cupon_HistorialModel nuevoHistorial = new Cupon_HistorialModel()
                {
                    Id_Cupon = cupon.Id_Cupon,
                    NroCupon = cupon.NroCupon,
                    CodCliente = cupon.CodCliente,
                    FechaUso = DateTime.Now
                };

                _context.Cupones_Historial.Add(nuevoHistorial);
                _context.Cupones_Clientes.Remove(cupon);

                await _context.SaveChangesAsync();
                Log.Information($"El cupón {nroCupon} fue utilizado correctamente.");

                return Ok(new { mensaje = "El cupón fue utilizado correctamente." });
            }
            catch (Exception ex)
            {
                Log.Error($"Error al quemar el cupón {nroCupon}: {ex.Message}");
                return BadRequest($"Error: {ex.Message}");
            }
        }



        [HttpGet("CuponesActivos")]
        public async Task<IActionResult> ObtenerCuponesActivos(string codCliente)
        {
            if (string.IsNullOrEmpty(codCliente))
            {
                Log.Information("El código de cliente es obligatorio y no fue proporcionado.");
                return BadRequest("El código de cliente es obligatorio.");
            }

            try
            {
                Log.Information($"Buscando cupones activos para el cliente con código: {codCliente}");

                var cuponesActivos = await (from c in _context.Cupones
                                            join ch in _context.Cupones_Historial
                                            on c.Id_Cupon equals ch.Id_Cupon
                                            where c.Activo == true && ch.CodCliente == codCliente
                                            select new
                                            {
                                                c,   // tabla Cupones
                                                ch   // tabla Cupones_Historial
                                            }).ToListAsync();

                if (cuponesActivos == null || !cuponesActivos.Any())
                {
                    Log.Information($"No se encontraron cupones activos para el cliente con código {codCliente}.");
                    return NotFound($"No se encontraron cupones activos para el cliente con código {codCliente}.");
                }

                Log.Information($"Se encontraron {cuponesActivos.Count} cupones activos para el cliente con código {codCliente}.");
                return Ok(cuponesActivos);
            }
            catch (Exception ex)
            {
                Log.Error($"Error al obtener cupones activos para el cliente con código {codCliente}: {ex.Message}");
                return BadRequest($"Error: {ex.Message}");
            }
        }




    }
}
