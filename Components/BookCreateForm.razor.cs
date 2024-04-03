using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorHttp.Components;

public partial class BookCreateForm : ComponentBase
{

    /* The [Inject] attribute is a directive in Blazor used for dependency injection. When applied to a property or a field within a Blazor component,
    * it signals to the Blazor framework that the specified service should be injected into that property or field.
    */
    [Inject]
    private HttpClient _httpClient { get; set; }

    // Our base URL for the API we are calling. We can append strings to this to utilize endpoints as needed.
    private static string BASE_URL = "https://livre.azurewebsites.net";

    // Stores our data needed to create a new book.
    private BookSaveRequest BookSaveRequest = new BookSaveRequest();

    private int BookToUpdateId {get; set;}

    // Is true when our book POST request is in process.
    private bool IsLoading {get; set;} = false;
    
    // These two variables hold the status/message for our errors that might occur.
    private bool ErrorOccurred {get; set;} = false;
    private string ErrorMessage {get; set;} = "";

    /// <summary>
    /// Submits our form to create a new book.
    /// </summary>
    /// <returns>The Task to return.</returns>
    private async Task SubmitForm()
    {
        this.IsLoading = true;
        this.ErrorOccurred = false;
        this.ErrorMessage = "";
        try {
        // Make the HTTP Request and wait for the response.
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{BASE_URL}/books", BookSaveRequest);

        // If successful, just say so in the console for now.
        if (response.IsSuccessStatusCode) {
            Console.WriteLine("Book Created Successfully!");
            this.BookSaveRequest = new BookSaveRequest();
        } else {
            // Otherwise, if it's a HTTP 400 (Bad Request), we know the user entered invalid data.
            // If it's not HTTP 400, then let's just display a generic error message.
            this.ErrorOccurred = true;
            if (HttpStatusCode.BadRequest == response.StatusCode) {
                // The API is actually sending back the invalid fields that need to be corrected, but for 
                // simplicity we will just display this error message here.
                this.ErrorMessage = "Book is invalid, please enter a valid book";
            } else {
                this.ErrorMessage = "An error occurred saving the book. Please check your input and try again.";
            }
        }
        } catch (HttpRequestException ex) {
            // If this occurred, we don't necessarily know what went wrong, just that the request could not be completed.
            // Display something generic.
            this.ErrorOccurred = true;
            this.ErrorMessage = $"An unexpected error occurred, please try again.";
        }
        
        this.IsLoading = false;

    }

    /// <summary>
    /// Updates an existing Book.
    /// </summary>
    /// <returns>The Task to return.</returns>
    private async Task UpdateBook()
    {
        this.IsLoading = true;
        this.ErrorOccurred = false;
        this.ErrorMessage = "";
        try {
        // Make the HTTP Request and wait for the response.
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{BASE_URL}/books/{BookToUpdateId}", BookSaveRequest);

        // If successful, just say so in the console for now.
        if (response.IsSuccessStatusCode) {
            Console.WriteLine("Book Updated Successfully!");
            this.BookSaveRequest = new BookSaveRequest();
        } else {
            // Otherwise, if it's a HTTP 400 (Bad Request), we know the user entered invalid data.
            // If it's not HTTP 400, then let's just display a generic error message.
            this.ErrorOccurred = true;
            if (HttpStatusCode.BadRequest == response.StatusCode) {
                // The API is actually sending back the invalid fields that need to be corrected, but for 
                // simplicity we will just display this error message here.
                this.ErrorMessage = "Book is invalid, please enter a valid book";
            } else {
                this.ErrorMessage = "An error occurred saving the book. Please check your input and try again.";
            }
        }
        } catch (HttpRequestException ex) {
            // If this occurred, we don't necessarily know what went wrong, just that the request could not be completed.
            // Display something generic.
            this.ErrorOccurred = true;
            this.ErrorMessage = $"An unexpected error occurred, please try again.";
        }
        
        this.IsLoading = false;

    }

}