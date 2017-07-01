using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BlissXamarinApp.Interfaces;
using BlissXamarinApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(BlissXamarinApp.Services.BlissApiService))]
namespace BlissXamarinApp.Services
{
    public class BlissApiService : IBlissApiService
    {
        private const string BaseUrl = "https://private-anon-5e603a36df-blissrecruitmentapi.apiary-mock.com/";
        private const int Limit = 10;

        public async Task<List<Question>> GetQuestionsAsync(int offset)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync($"{BaseUrl}questions?{Limit}&{offset}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<List<Question>>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync($"{BaseUrl}questions/{questionId}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<Question>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Question>> GetQuestionsByFilterAsync(int offset, string filter)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync($"{BaseUrl}questions?{Limit}&{offset}&{Uri.EscapeUriString(filter)}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<List<Question>>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<StatusApi> GetStatusApiAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync($"{BaseUrl}health").ConfigureAwait(false);

                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<StatusApi>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> PostUrlAsync(ShareScreen content)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync($"{BaseUrl}share?{content.DestinationEmail}&{content.ContentUrl}", null)
                    .ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
