using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace jgarcesS6.Views;

public partial class AñadirEstudiante : ContentPage
{
	public AñadirEstudiante()
	{
		InitializeComponent();
	}

    private async void btnAñadir_Clicked(object sender, EventArgs e)
    {
        try
        {
            var estudiante = new
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Edad = int.Parse(txtEdad.Text)
            };

            var json = JsonConvert.SerializeObject(estudiante);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync("https://localhost:7040/api/Estudiante", content);

                if (response.IsSuccessStatusCode)
                {
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    var mensaje = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"No se pudo añadir el estudiante: {response.StatusCode} - {mensaje}", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo añadir el estudiante: " + ex.Message, "OK");
        }
    }
}