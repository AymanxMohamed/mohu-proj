using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Identity.Client;
using VirtualEntity.Poc.Contacts.Models;

namespace VirtualEntity.Poc.Contacts.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ContactsController : ODataController
{
    private readonly HttpClient _httpClient;

    public ContactsController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://mohuqc.crm4.dynamics.com/api/data/v9.0/");
        SetAuthenticationHeader().Wait();
    }

    private async Task SetAuthenticationHeader()
    {
        var app = ConfidentialClientApplicationBuilder.Create("0f1beb50-1c1f-4f0e-a86d-db0418edf4a9")
            .WithClientSecret("py78Q~kR6OVXlgxisR3K79T2KHf5gLyUuGD5aa2~")
            .WithAuthority(new Uri("https://login.microsoftonline.com/c9b1687e-c304-4684-9573-dcb972d1f81e"))
            .Build();

        var result = await app.AcquireTokenForClient(["https://mohuqc.crm4.dynamics.com/.default"]).ExecuteAsync();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", result.AccessToken);
    }

    [EnableQuery]
    public async Task<IActionResult> Get()
    {
        var response = await _httpClient.GetAsync("contacts?$select=contactid,fullname,emailaddress1,mobilephone");


        if (!response.IsSuccessStatusCode) return StatusCode((int)response.StatusCode);
        
        var json = await response.Content.ReadAsStringAsync();
        
        var contacts = ParseDynamicsContacts(json);

        return Ok(contacts);
    }

    [EnableQuery]
    public async Task<IActionResult> Get([FromODataUri] Guid key)
    {
        // Construct the Dynamics API URL with the GUID key
        var response = await _httpClient.GetAsync($"contacts({key})?$select=contactid,fullname,emailaddress1,mobilephone");

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();

        // Parse the response into a Contact object
        var contact = ParseSingleDynamicsContact(json);

        return Ok(contact);
    }



    private static Contact ParseSingleDynamicsContact(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        return Contact.Create(
            contactId: root.GetProperty("contactid").GetString(),
            fullName: root.GetProperty("fullname").GetString(),
            email: root.GetProperty("emailaddress1").GetString(),
            phoneNumber: root.GetProperty("mobilephone").GetString()
        );
    }

    private static IEnumerable<Contact> ParseDynamicsContacts(string json)
    {
        var contacts = new List<Contact>();
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("value", out JsonElement valueArray) ||
            valueArray.ValueKind != JsonValueKind.Array) return contacts;
            
        foreach (var item in valueArray.EnumerateArray())
        {
            contacts.Add(Contact.Create(
                contactId: item.GetProperty("contactid").GetString(),
                fullName: item.GetProperty("fullname").GetString(),
                email: item.GetProperty("emailaddress1").GetString(),
                phoneNumber: item.GetProperty("mobilephone").GetString()));
        }

        return contacts;
    }
}