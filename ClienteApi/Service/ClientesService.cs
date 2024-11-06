using ClienteApi.Interface;
using ClienteApi.Models.DTO;
using Newtonsoft.Json;
using System.Text;

namespace ClienteApi.Service
{
    public class ClientesService : IClienteService
    {

        public async Task<string> SolicitarCupon (ClienteDTO clienteDTO)
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
    }
}
