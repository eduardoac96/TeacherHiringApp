using Domain.Student;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TeacherHiring.Services
{
    public class StudentServices
    {
        private string _apiUrl;
        private HttpClient _client;

        public StudentServices(string apiUrl)
        {
            _apiUrl = apiUrl;
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = long.MaxValue;
        }

        public async Task<List<DtoRequestStatus>> GetRequests(int studentID)
        {
            var uri = new Uri(string.Format(_apiUrl + "AlumnoMateria/GetListAlumnoMateriaApps?idAlumno={0}", studentID));

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Add("Token", App.LoggedUser.Token);

            HttpResponseMessage response = await _client.SendAsync(message);
            var result = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

            var requests = new List<DtoRequestStatus>();
            foreach (var m in result)
            {
                requests.Add(new DtoRequestStatus
                {
                   StudentScheduleID = m.IdAlumnoMateria,
                   TeacherScheduleID = m.IdProfesoMateria,
                   StudentID = m.IdAlumno,
                   TeacherID = m.IdProfesor,
                   ClassID = m.IdMateria,
                   StudentName = m.NombreAlumno,
                   TeacherName = m.NombreProfesor,
                   ClassName = m.NombreMateria,
                   AvailableDate = m.FechaHora,
                   Latitude = m.Latitud,
                   Longitude = m.Longitud
                });
            }

            return requests;
        }
    }
}
