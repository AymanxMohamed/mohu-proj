// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.OData.Query;
// using Microsoft.AspNetCore.OData.Routing.Controllers;
// using Microsoft.Identity.Client;
// using System.Net.Http.Headers;
// using System.Text.Json;
//
// namespace POCVirtualEntity.Controllers
// {
//
//     public class ContactsController : ODataController
//     {
//         private readonly HttpClient _httpClient;
//
//         public ContactsController()
//         {
//             _httpClient = new HttpClient();
//             _httpClient.BaseAddress = new Uri("https://mohuqc.crm4.dynamics.com/api/data/v9.0/");
//             SetAuthenticationHeader().Wait();
//         }
//
//         private async Task SetAuthenticationHeader()
//         {
//             var app = ConfidentialClientApplicationBuilder.Create("0f1beb50-1c1f-4f0e-a86d-db0418edf4a9")
//                 .WithClientSecret("py78Q~kR6OVXlgxisR3K79T2KHf5gLyUuGD5aa2~")
//                 .WithAuthority(new Uri("https://login.microsoftonline.com/c9b1687e-c304-4684-9573-dcb972d1f81e"))
//                 .Build();
//
//             var result = await app.AcquireTokenForClient(new[] { "https://mohuqc.crm4.dynamics.com/.default" }).ExecuteAsync();
//
//             _httpClient.DefaultRequestHeaders.Authorization =
//                 new AuthenticationHeaderValue("Bearer", result.AccessToken);
//         }
//
//         [EnableQuery]
//         public async Task<IActionResult> Get()
//         {
//             var response = await _httpClient.GetAsync("contacts?$select=contactid,fullname,emailaddress1,telephone1");
//             
//             
//             if (response.IsSuccessStatusCode)
//             {
//                 var json = await response.Content.ReadAsStringAsync();
//                 var contacts = ParseDynamicsContacts(json);
//                 return Ok(contacts);
//             }
//             return StatusCode((int)response.StatusCode);
//         }
//
//         private IEnumerable<Contact> ParseDynamicsContacts(string json)
//         {
//             var contacts = new List<Contact>();
//             using (JsonDocument document = JsonDocument.Parse(json))
//             {
//                 JsonElement root = document.RootElement;
//
//                 if (root.TryGetProperty("value", out JsonElement valueArray) && valueArray.ValueKind == JsonValueKind.Array)
//                 {
//                     foreach (JsonElement item in valueArray.EnumerateArray())
//                     {
//                         contacts.Add(new Contact
//                         {
//                             ContactId = item.GetProperty("contactid").GetString(),
//                             FullName = item.GetProperty("fullname").GetString(),
//                             Email = item.GetProperty("emailaddress1").GetString(),
//                             PhoneNumber = item.GetProperty("telephone1").GetString()
//                         });
//                     }
//                 }
//             }
//
//             return contacts;
//         }
//     }
// }
