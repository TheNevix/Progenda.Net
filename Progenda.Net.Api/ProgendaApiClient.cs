using Newtonsoft.Json;
using Progenda.Net.Api.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Progenda.Net.Api
{
    public class ProgendaApiClient
    {
        private readonly string _email;
        private readonly string _token;
        private readonly string _baseUrl;
        private readonly string _base64Token;
        private readonly HttpClient _httpClient;

        public ProgendaApiClient(string email, string token, string applicationName)
        {
            _email = email;
            _token = token;
            _baseUrl = "https://progenda.be/api/v2/";
            _base64Token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_email}:{_token}"));
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _base64Token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(applicationName);
        }

        /// <summary>
        /// Gets all the centers of a Progenda account.
        /// </summary>
        /// <returns>A list of <see cref="Center"/> or null if an error occured.</returns>
        public async Task<List<Center>> GetCenters()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "centers");
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                CenterResponse centerResponse = JsonConvert.DeserializeObject<CenterResponse>(responseBody);

                List<Center> centers = new List<Center>();

                if (centerResponse != null && centerResponse.CenterDetails != null)
                {
                    foreach (var center in centerResponse.CenterDetails)
                    {
                        centers.Add(center.Center);
                    }
                }

                return centers;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets all the calendars of a Progenda account.
        /// </summary>
        /// <returns>A list of <see cref="Calendar"/> or null if an error occured.</returns>
        public async Task<List<Calendar>> GetCalendars()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "calendars");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                CalendarResponse calendarResponse = JsonConvert.DeserializeObject<CalendarResponse>(responseBody);

                List<Calendar> calendarList = new List<Calendar>();

                if (calendarResponse != null && calendarResponse.Calendars != null)
                {
                    foreach (var calendarWrapper in calendarResponse.Calendars)
                    {
                        calendarList.Add(calendarWrapper.Calendar);
                    }
                }

                return calendarList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all the patients with paging of a center.
        /// </summary>
        /// <param name="centerId">The ID of a center.</param>
        /// <param name="page">The page (default is 1).</param>
        /// <returns>A list of <see cref="Patient"/> or null if an error occured.</returns>
        public async Task<List<Patient>> GetPatients(int centerId, int? page = 1, int? since = 100000)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/centers/{centerId}/patients?page={page}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                PatientResponse patientResponse = JsonConvert.DeserializeObject<PatientResponse>(responseBody);

                List<Patient> patientList = new List<Patient>();

                if (patientResponse != null && patientResponse.PatientDetails != null)
                {
                    foreach (var patientWrapper in patientResponse.PatientDetails)
                    {
                        patientList.Add(patientWrapper.Patient);
                    }
                }

                return patientList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a specefic patient.
        /// </summary>
        /// <param name="centerId">The ID of a center.</param>
        /// <param name="remoteId">The remote ID of the patient.</param>
        /// <returns>A <see cref="Patient"/> or null if an error occured.</returns>
        public async Task<Patient> GetPatient(int centerId, string remoteId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/centers/{centerId}/patients/remote_id:{remoteId}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetPatientResponse patientResponse = JsonConvert.DeserializeObject<GetPatientResponse>(responseBody);

                return patientResponse?.Patient;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Updates the remote ID of a patient.
        /// </summary>
        /// <param name="centerId">The ID of a center.</param>
        /// <param name="patientId">The ID of the patient (ID from Progenda).</param>
        /// <param name="remoteId">The remote ID that you want to assign.</param>
        /// <returns>A <see cref="Patient"/> or null if an error occured.</returns>
        public async Task<Patient> UpdatePatientRemoteId(int centerId, int patientId, string remoteId)
        {
            try
            {
                var body = new
                {
                    patient = new
                    {
                        remote_id = remoteId
                    }
                };

                var jsonBody = JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_baseUrl}/centers/{centerId}/patients/{patientId}", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetPatientResponse patientResponse = JsonConvert.DeserializeObject<GetPatientResponse>(responseBody);

                return patientResponse?.Patient;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Updates a patient.
        /// </summary>
        /// <param name="centerId">The ID of a center.</param>
        /// <param name="remoteId">The remote ID of the patient.</param>
        /// <param name="request">A <see cref="UpdatePatientRequest"/> object with the properties you want to update.</param>
        /// <returns>A <see cref="Patient"/> or null if an error occured.</returns>
        public async Task<Patient> UpdatePatient(int centerId, string remoteId, UpdatePatientRequest request)
        {
            try
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                var jsonBody = JsonConvert.SerializeObject(request, jsonSettings);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_baseUrl}/centers/{centerId}/patients/remote_id:{remoteId}", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetPatientResponse patientResponse = JsonConvert.DeserializeObject<GetPatientResponse>(responseBody);

                return patientResponse?.Patient;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a patient.
        /// </summary>
        /// <param name="centerId">The ID of a center.</param>
        /// <param name="request">An <see cref="UpdatePatientRequest"/> object.</param>
        /// <returns>A <see cref="Patient"/> or null if an error occured.</returns>
        public async Task<Patient> CreatePatient(int centerId, CreatePatientRequest request)
        {
            try
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                var jsonBody = JsonConvert.SerializeObject(request, jsonSettings);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/centers/{centerId}/patients", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetPatientResponse patientResponse = JsonConvert.DeserializeObject<GetPatientResponse>(responseBody);

                return patientResponse?.Patient;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes a patient.
        /// </summary>
        /// <param name="centerId">The ID of a center.</param>
        /// <param name="remoteId">The remote ID of the patient.</param>
        /// <returns>True or false if an error occured.</returns>
        public async Task<bool> DeletePatient(int centerId, string remoteId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseUrl}/centers/{centerId}/patients/remote_id:{remoteId}");
                response.EnsureSuccessStatusCode();
                
                if(response.StatusCode == HttpStatusCode.NoContent)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Updates/creates multiple patients.
        /// </summary>
        /// <param name="centerId">The ID of a center.</param>
        /// <param name="patients">A list of patients to update or create.</param>
        /// <returns>A list of <see cref="Patient"/> or null if an error occured.</returns>
        public async Task<List<Patient>> BulkUpdatePatients(int centerId, List<BulkUpdatePatient> patients)
        {
            try
            {
                var body = new
                {
                    patients_collection = new
                    {
                        patients = patients.Select(p => new
                        {
                            patient = p
                        }).ToList()
                    }
                };

                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                var jsonBody = JsonConvert.SerializeObject(body, jsonSettings);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/centers/{centerId}/patients_collections", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                PatientResponse patientResponse = JsonConvert.DeserializeObject<PatientResponse>(responseBody);

                List<Patient> patientList = new List<Patient>();

                if (patientResponse != null && patientResponse.PatientDetails != null)
                {
                    foreach (var patientWrapper in patientResponse.PatientDetails)
                    {
                        patientList.Add(patientWrapper.Patient);
                    }
                }

                return patientList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all appointments of a calendar.
        /// </summary>
        /// <param name="calendarId">The ID of a calendar.</param>
        /// <returns>A <see cref="Appointment"/> or null if an error occured.</returns>
        public async Task<List<Appointment>> GetAppointments(int calendarId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/calendars/{calendarId}/appointments");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetAppointmentsResponse appointmentResponse = JsonConvert.DeserializeObject<GetAppointmentsResponse>(responseBody);

                List<Appointment> appointmentList = new List<Appointment>();

                if (appointmentResponse != null && appointmentResponse.Appointments != null)
                {
                    foreach (var appointmentWrapper in appointmentResponse.Appointments)
                    {
                        appointmentList.Add(appointmentWrapper.Appointment);
                    }
                }

                return appointmentList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Updates the remote ID of an appointment.
        /// </summary>
        /// <param name="calendarId">The ID of a calendar.</param>
        /// <param name = "appointmentId" > The ID of an appointment.</param>
        /// <param name = "remoteId" > The remote ID to assign.</param>
        /// <returns>A <see cref="Appointment"/> or null if an error occured.</returns>
        public async Task<Appointment> UpdateAppointmentRemoteId(int calendarId, int appointmentId, string remoteId)
        {
            try
            {
                var body = new
                {
                    appointment = new
                    {
                        remote_id = remoteId
                    }
                };

                var jsonBody = JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_baseUrl}/calendars/{calendarId}/appointments/{appointmentId}", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetAppointmentResponse appointmentResponse = JsonConvert.DeserializeObject<GetAppointmentResponse>(responseBody);

                return appointmentResponse?.Appointment;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets an appointment by remote ID.
        /// </summary>
        /// <param name="calendarId">The ID of a calendar.</param>
        /// <param name = "remoteId" > The remote ID of an appointment.</param>
        /// <returns>A <see cref="Appointment"/> or null if an error occured.</returns>
        public async Task<Appointment> GetAppointment(int calendarId, string remoteId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/calendars/{calendarId}/appointments/remote_id:{remoteId}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetAppointmentResponse appointmentResponse = JsonConvert.DeserializeObject<GetAppointmentResponse>(responseBody);

                return appointmentResponse?.Appointment;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes an appointment.
        /// </summary>
        /// <param name="calendarId">The ID of a calendar.</param>
        /// <param name = "remoteId" > The remote ID to assign.</param>
        /// <returns>True or false if an error occured.</returns>
        public async Task<bool> DeleteAppointment(int calendarId, string remoteId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseUrl}/calendars/{calendarId}/appointments/remote_id:{remoteId}");
                response.EnsureSuccessStatusCode();

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Updates an appointment.
        /// </summary>
        /// <param name="calendarId">The ID of a calendar.</param>
        /// <param name = "remoteId" > The remote ID to assign.</param>
        /// <param name="request">A <see cref="UpdateAppointmentRequest"/> object with the properties you want to update.</param>
        /// <returns>A <see cref="Appointment"/> or null if an error occured.</returns>
        public async Task<Appointment> UpdateAppointment(int calendarId, string remoteId, UpdateAppointmentRequest request)
        {
            try
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                var jsonBody = JsonConvert.SerializeObject(request, jsonSettings);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_baseUrl}/calendars/{calendarId}/appointments/remote_id:{remoteId}", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetAppointmentResponse appointmentResponse = JsonConvert.DeserializeObject<GetAppointmentResponse>(responseBody);

                return appointmentResponse?.Appointment;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates an appointment.
        /// </summary>
        /// <param name="calendarId">The ID of a calendar.</param>
        /// <param name="request">An <see cref="CreateAppointmentRequest"/> object.</param>
        /// <returns>A <see cref="Appointment"/> or null if an error occured.</returns>
        public async Task<Appointment> CreateAppointment(int calendarId, CreateAppointmentRequest request)
        {
            try
            {
                var body = new
                {
                    appointment = request
                };
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                var jsonBody = JsonConvert.SerializeObject(body, jsonSettings);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/calendars/{calendarId}/appointments", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetAppointmentResponse appointmentResponse = JsonConvert.DeserializeObject<GetAppointmentResponse>(responseBody);

                return appointmentResponse?.Appointment;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Updates/creates multiple appointments.
        /// </summary>
        /// <param name="calendarId">The ID of a calendar.</param>
        /// <param name="appointments">A list of appointments to update or create.</param>
        /// <returns>A list of <see cref="Appointment"/> or null if an error occured.</returns>
        public async Task<List<Appointment>> BulkUpdateAppointments(int calendarId, List<BulkUpdatePatient> appointments)
        {
            try
            {
                var body = new
                {
                    appointments_collection = new
                    {
                        appointments = appointments.Select(a => new
                        {
                            appointment = a
                        }).ToList()
                    }
                };

                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                var jsonBody = JsonConvert.SerializeObject(body, jsonSettings);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/calendars/{calendarId}/appointments_collections", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                GetAppointmentsResponse appointmentResponse = JsonConvert.DeserializeObject<GetAppointmentsResponse>(responseBody);

                List<Appointment> appointmentList = new List<Appointment>();

                if (appointmentResponse != null && appointmentResponse.Appointments != null)
                {
                    foreach (var appointmentWrapper in appointmentResponse.Appointments)
                    {
                        appointmentList.Add(appointmentWrapper.Appointment);
                    }
                }

                return appointmentList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all services.
        /// </summary>
        /// <param name="calendarId">The ID of a calendar.</param>
        /// <returns>A <see cref="Service"/> or null if an error occured.</returns>
        public async Task<List<Service>> GetServices(int calendarId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/calendars/{calendarId}/services");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetServicesResponse serviceResponse = JsonConvert.DeserializeObject<GetServicesResponse>(responseBody);

                List<Service> serviceList = new List<Service>();

                if (serviceResponse != null && serviceResponse.Services != null)
                {
                    foreach (var serviceWrapper in serviceResponse.Services)
                    {
                        serviceList.Add(serviceWrapper.Serivce);
                    }
                }

                return serviceList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Updates/creates multiple services.
        /// </summary>
        /// <param name="calendarId">The ID of a calendar.</param>
        /// <param name="services">A list of services to update or create.</param>
        /// <returns>A list of <see cref="Service"/> or null if an error occured.</returns>
        public async Task<List<Service>> BulkUpdateServices(int calendarId, List<BulkUpdateService> services)
        {
            try
            {
                var body = new
                {
                    services_collection = new
                    {
                        services = services.Select(s => new
                        {
                            service = s
                        }).ToList()
                    }
                };

                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                var jsonBody = JsonConvert.SerializeObject(body, jsonSettings);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/calendars/{calendarId}/services_collections", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                GetServicesResponse serviceResponse = JsonConvert.DeserializeObject<GetServicesResponse>(responseBody);

                List<Service> serviceList = new List<Service>();

                if (serviceResponse != null && serviceResponse.Services != null)
                {
                    foreach (var serviceWrapper in serviceResponse.Services)
                    {
                        serviceList.Add(serviceWrapper.Serivce);
                    }
                }

                return serviceList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
