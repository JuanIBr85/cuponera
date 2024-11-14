using ClienteApi.Interface;
using ClienteApi.Models.DTO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ClienteApi.Service
{
    public class ClientesService : IClienteService
    {
        public async Task<string> SolicitarCupon(ClienteDTO clienteDTO)
        {
            try
            {
                var jsonCliente = JsonConvert.SerializeObject(clienteDTO);
                var contenido = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var respuesta = await client.PostAsync("https://localhost:7003/api/SolicitudCupones/SolicitarCupon", contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    var msg = await respuesta.Content.ReadAsStringAsync();
                    return msg;
                }
                else
                {
                    var error = await respuesta.Content.ReadAsStringAsync();
                    throw new Exception($"{error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<string> QuemarCuponAsync(string nroCupon)
        {
            try
            {
                var client = new HttpClient();

                var response = await client.PostAsync( // https://www.youtube.com/watch?v=kvuJIqGGIG4 y por qué en inglés?, por las dudas mejor usar dos variables que reciclar "respuesta"
                    $"https://localhost:7003/api/SolicitudCupones/QuemarCupon?nroCupon={nroCupon}",
                    null
                );

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al intentar quemar el cupón: {ex.Message}");
            }
        }
    }
}
