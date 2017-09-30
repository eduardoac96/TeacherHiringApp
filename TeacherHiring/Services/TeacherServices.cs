using Acr.UserDialogs;
using Domain.Student;
using Domain.Teacher;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TeacherHiring.Services
{
    public class TeacherServices
    {
        private string _apiUrl;
        private HttpClient _client;

        public TeacherServices(string apiUrl)
        {
            _apiUrl = apiUrl;
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = long.MaxValue;
        }

        public async Task<List<DtoClassAvailable>> GetAvailableClasses(string token)
        {
            
            var uri = new Uri(string.Format(_apiUrl + "Materia/GetListMateriaApps"));

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Add("Token", token);

            HttpResponseMessage response = await _client.SendAsync(message);
            var result = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

            var materiasCollection = new List<DtoClassAvailable>();
            foreach (var m in result)
            {
                materiasCollection.Add(new DtoClassAvailable
                {
                    ClassID = m.MateriaId,
                    Name = m.Descripcion,
                    Quantity =  m.Disponibles
                });
            }

            return materiasCollection;

        }

        public async Task SaveClass(DtoNewClass item)
        {
            var uri = new Uri(string.Format(_apiUrl + "ProfesorMateria/InsProfesorMateriaApp", "ProfesorMateriaApp"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);

            var json = JsonConvert.SerializeObject(
                    new {
                            IdProfesorMateria = new Random().Next(),
                            IdMateria = item.ClassID,
                            IdProfesor = item.TeacherID,
                            FechaHora = item.AvailableDate + item.Time,
                            Latitud = item.CurrentLatitude,
                            Longitud = item.CurrentLongitude,
                            NombreMateria = item.Name,
                            NombreProfesor = item.TeacherName
                        });

            request.Headers.Add("Token", item.Token);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("No se pudor registrar la materia, intente de nuevo.");
            }

        }

        public async Task ConfirmClass(DtoTeacherSchedule schedule)
        {
            var uri = new Uri(string.Format(_apiUrl + "AlumnoMateria/InsProfesorMateriaApp?idAlumno={0}&idProfesorMateria={1}", schedule.StudentID, schedule.ScheduleID));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);

            request.Headers.Add("Token", schedule.Token);

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("No se pudo registrar la materia, intente de nuevo.");
            }
        }
        public async Task<List<DtoTeacherSchedule>> GetScheduleClass(DtoNewClass schedule)
        {

            var uri = new Uri(string.Format(_apiUrl + "AlumnoMateria/GetListProfesorMateriaApp?idMateria={0}", schedule.ClassID));

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Add("Token", schedule.Token);

            HttpResponseMessage response = await _client.SendAsync(message);
            var result = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

            var materiasCollection = new List<DtoTeacherSchedule>();
            foreach (var m in result)
            {
                materiasCollection.Add(new DtoTeacherSchedule
                {
                    ScheduleID = m.IdProfesorMateria,
                    ClassID = m.IdMateria,
                    TeacherID = m.IdProfesor,
                    AvailableDate = m.FechaHora,
                    Latitude = m.Latitud,
                    Longitude = m.Longitud,
                    ClassName = m.NombreMateria,
                    TeacherName = m.NombreProfesor,
                    Token = schedule.Token,

                });
            }

            return materiasCollection;

        }

        public async Task<List<DtoRequestStatus>> GetPendingRequests(int teacherID)
        {

            var uri = new Uri(string.Format(_apiUrl + "ProfesorMateria/GetListProfesorMateriaApps?idProfesor={0}&aceptada={1}", teacherID, false));

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Add("Token", App.LoggedUser.Token);

            HttpResponseMessage response = await _client.SendAsync(message);
            var result = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

            var materiasCollection = new List<DtoRequestStatus>();
            foreach (var m in result)
            {
                materiasCollection.Add(new DtoRequestStatus
                {
                    TeacherScheduleID = m.IdProfesoMateria,
                    ClassID = m.IdMateria,
                    TeacherID = m.IdProfesor,
                    AvailableDate = m.FechaHora,
                    Latitude = m.Latitud,
                    Longitude = m.Longitud,
                    ClassName = m.NombreMateria,
                    TeacherName = m.NombreProfesor,
                    IsAccepted = m.Aceptada,
                    StudentID = m.IdAlumno,
                    StudentName = m.NombreAlumno,
                    StudentScheduleID = m.IdAlumnoMateria
                });
            }

            return materiasCollection;
        }

        public async Task<List<DtoRequestStatus>> GetConfirmedRequests(int teacherID)
        {

            var uri = new Uri(string.Format(_apiUrl + "ProfesorMateria/GetListProfesorMateriaApps?idProfesor={0}&aceptada={1}", teacherID, true));

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Add("Token", App.LoggedUser.Token);

            HttpResponseMessage response = await _client.SendAsync(message);
            var result = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

            var materiasCollection = new List<DtoRequestStatus>();
            foreach (var m in result)
            {
                materiasCollection.Add(new DtoRequestStatus
                {
                    TeacherScheduleID = m.IdProfesoMateria,
                    ClassID = m.IdMateria,
                    TeacherID = m.IdProfesor,
                    AvailableDate = m.FechaHora,
                    Latitude = m.Latitud,
                    Longitude = m.Longitud,
                    ClassName = m.NombreMateria,
                    TeacherName = m.NombreProfesor,
                    IsAccepted = m.Aceptada,
                    StudentID = m.IdAlumno,
                    StudentName = m.NombreAlumno,
                    StudentScheduleID = m.IdAlumnoMateria
                });
            }

            return materiasCollection;
        }

        public async Task ConfirmRequest(DtoRequestStatus schedule)
        {
            var uri = new Uri(string.Format(_apiUrl + "ProfesorMateria/SolicitarProfesorMateriaApp?idProfesor={0}&idProfesorMateria={1}", schedule.TeacherID, schedule.TeacherScheduleID));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);

            request.Headers.Add("Token", App.LoggedUser.Token);

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("No se pudo confirmar la solicitud, intente de nuevo.");
            }
        }
    }
}
