using System.Net.Http.Json;
using BlazorHttp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorHttp.Pages;

public partial class BookPage : ComponentBase
{

    /* The [Inject] attribute is a directive in Blazor used for dependency injection. When applied to a property or a field within a Blazor component,
    * it signals to the Blazor framework that the specified service should be injected into that property or field.
    */
    [Inject]
    private HttpClient _httpClient { get; set; }

    // Our base URL for the API we are calling. We can append strings to this to utilize endpoints as needed.
    private static string BASE_URL = "https://livre.azurewebsites.net";

    private bool IsLoading = true;
    private bool ErrorOcurred = false;
    private string ErrorMessage = "";

    // List of books to hold the books returned from the HTTP Request.
    private List<BookResponse> Books { get; set; } = [];

    /// <summary>
    /// lifecycle method in Blazor components that is called when the component has been initialized and is ready for interaction. 
    /// It is invoked automatically after the component has been rendered for the first time and all of its parameters have been set.
    /// </summary>
    /// <returns>The Task to return</returns>
    protected override async Task OnInitializedAsync()
    {
        await this.RetrieveBooks();
        this.IsLoading = false;
    }

    /// <summary>
    /// Retrieves books from the Livre API and sets our list of books.
    /// </summary>
    /// <returns>The Task to return.</returns>
    private async Task RetrieveBooks()
    {
        // Reset our error variables before each call.
        this.ErrorOcurred = false;
        this.ErrorMessage = "";
        try
        {
            // Get our response back from the API as an HttpResponseMessage so we can disect the response better.
            HttpResponseMessage response = await _httpClient.GetAsync($"{BASE_URL}/books");

            // If response is successful, (Any status code between 200-299), return our list of books.
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the JSON response to a list of BookResponse
                this.Books = await response.Content.ReadFromJsonAsync<List<BookResponse>>();
            }
            else
            {
                // If the response is not successful, handle the error by displaying a message for the user.
                // This code will run if the API we are calling sent us back a response in the 400 or 500 range.
                this.ErrorOcurred = true;
                this.ErrorMessage = $"An unexpected error occurred: {response.StatusCode}: {response.ReasonPhrase}";
            }

        }
        catch (HttpRequestException ex)
        {
            // If an exception occurs, handle the error by displaying a message for the user.
            // This code will run if an exception occurs while calling the server, such as no response or a timeout.
            this.ErrorOcurred = true;
            this.ErrorMessage = $"An unexpected error occurred. Please try again later.";
            Console.WriteLine(ex.ToString());
        }
    }

}