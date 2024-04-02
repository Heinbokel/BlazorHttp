using System.Net.Http.Json;
using BlazorHttp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorHttp.Pages;

public partial class BookPage: ComponentBase {

    /* The [Inject] attribute is a directive in Blazor used for dependency injection. When applied to a property or a field within a Blazor component,
    * it signals to the Blazor framework that the specified service should be injected into that property or field.
    */
    [Inject]
    private HttpClient _httpClient {get; set;}

    // Our base URL for the API we are calling. We can append strings to this to utilize endpoints as needed.
    private static string BASE_URL = "https://livre.azurewebsites.net";

    private bool IsLoading = true;
    
    // List of books to hold the books returned from the HTTP Request.
    private List<BookResponse> Books {get; set;} = [];

    /// <summary>
    /// lifecycle method in Blazor components that is called when the component has been initialized and is ready for interaction. 
    /// It is invoked automatically after the component has been rendered for the first time and all of its parameters have been set.
    /// </summary>
    /// <returns>The Task to return</returns>
    protected override async Task OnInitializedAsync()
    {
        this.Books = await _httpClient.GetFromJsonAsync<List<BookResponse>>($"{BASE_URL}/books");
        this.IsLoading = false;
    }

}