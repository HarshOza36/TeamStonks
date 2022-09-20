using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using System.Net.Http;

using System.IO;

using System;

using Newtonsoft.Json;

using Newtonsoft.Json.Linq;

using System.Threading.Tasks;

public class analytics : MonoBehaviour
{
    async Task Awake()
    {        
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

        var get_response = await client.GetAsync("https://csci526analytics-default-rtdb.firebaseio.com/analytics.json");

        if (get_response.IsSuccessStatusCode)
        {
            var content = await get_response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(content);


            var l_start_cnt= (int)jsonObject["level_start"]["level_0"] + 1;

            jsonObject["level_start"]["level_0"] = l_start_cnt.ToString();

            var put_content = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            var put_response = await client.PutAsync("https://csci526analytics-default-rtdb.firebaseio.com/analytics.json",  new StringContent(put_content));
            
            if (put_response.IsSuccessStatusCode)
            {
                Debug.Log("Successfully updated: ");
            }
            else{
                Debug.Log("Error");
            }
        }
        else
        {
            Debug.Log("Error::: ");
            Debug.Log((int)get_response.StatusCode);
            Debug.Log(get_response.ReasonPhrase);
        }
    }
}