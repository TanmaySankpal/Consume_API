using Consume_API;
using System.Net;
using System.Net.Http.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var client=new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7152/api/");
            PostStudents(client).Wait();
            GetAllStudents(client).Wait();
        }

       
    }


    private static async Task GetAllStudents(HttpClient client)
    {
        HttpResponseMessage httpResponseMessage = await client.GetAsync("Student");

        if (httpResponseMessage.IsSuccessStatusCode) 
        {
            List<Student>students=await httpResponseMessage.Content.ReadFromJsonAsync<List<Student>>();

            foreach(var item in students)
            {
                Console.WriteLine(item.Name);
            }
        }
        else
        {
            Console.WriteLine("Fail");
        }
    }

    private static async Task PostStudents(HttpClient client)
    {
        HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync<Student>("Student",new Student { Id=1,Name="xyz"});

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            string errormessge = await httpResponseMessage.Content.ReadAsStringAsync();
            
            Console.WriteLine(errormessge);
            
        }
    }
}