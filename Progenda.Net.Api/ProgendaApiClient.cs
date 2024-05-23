﻿using Newtonsoft.Json;
using Progenda.Net.Api.Models;
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

        public async Task<List<Patient>> GetPatients(int centerId, int? page = 1, int? since = 100000)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/centers/{centerId}/patients?page={page}&since={since}");
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
    }
}
