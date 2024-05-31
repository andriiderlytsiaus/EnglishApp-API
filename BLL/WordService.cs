using BLL.WordModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL
{
    public class WordService
    {
        public static async Task<Definition> GetWord(string wordString)
        {
            var word = new Definition();
            HttpClient client = new HttpClient();
            try
            {
                string apiUrl = $"https://api.dictionaryapi.dev/api/v2/entries/en/{wordString}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                string responseContent = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var definitions = System.Text.Json.JsonSerializer.Deserialize<Definition[]>(responseContent, options);
                var myword = new Definition();
                myword.Word = definitions[0].Word;
                myword.Phonetic = definitions[0].Phonetic;
                myword.Meanings = definitions[0].Meanings;

                return myword;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                client.Dispose();
            }
        }
        public static async Task<string> GetBestDefinition(string wordString,
            string sentence, ICollection<string> definitions)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string apiUrl = "http://127.0.0.1:5000/find_definition";
                var data = new
                {
                    word = wordString,
                    sentence = sentence,
                    definitions = definitions
                };
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
    }
}
