using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Linq;
using Domain.Security;

namespace TeacherHiring
{
    public class SecurityServices
	{

        private string _apiUrl;
        private HttpClient _client;

		public SecurityServices(string apiUrl)
		{
            _apiUrl = apiUrl;
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = long.MaxValue;

        }
		public async Task<DtoUser> SignIn(DtoLogin person)
		{
            try
            {
                if (string.IsNullOrEmpty(person.Username))
                {
                    throw new ArgumentNullException("Contraseña invalida.");
                }
                else if (string.IsNullOrEmpty(person.Password))
                {
                    throw new ArgumentNullException("Contraseña invalida.");
                }

                var uri = new Uri(string.Format(_apiUrl + "Authenticate/Authenticate", "LogPerson"));
                var json = JsonConvert.SerializeObject(new { ClaveAcceso = person.Username, Contrasena = person.Password });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    //var dataResponse = await ApiResponse.Content.ReadAsStringAsync();

                    var token = response.Headers.GetValues("Token").First();

                    return await GetLogPerson(token);
                }
            }
            catch (Exception ex)
            {
                string msgException = ex.Message;
            }

			return null;
		}

        public async Task<DtoUser> GetLogPerson(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new Exception("Invalid token value.");

            var uri = new Uri(string.Format(_apiUrl + "Usuario/GetDataPerson?token={0}", token));
            
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Token", token);

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var person = JsonConvert.DeserializeObject<dynamic>(result);

                return new DtoUser {
                     UserID = person.Id,
                     Name = person.Nombre,
                     UserName = person.ClaveAcceso,
                     Token = token,
                     UserTypeID = person.IdTipoPerson
                };
            }

            return null;

        }

     
    }
}
