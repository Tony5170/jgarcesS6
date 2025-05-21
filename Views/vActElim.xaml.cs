using System.Text;
using jgarcesS6.Models;
using Newtonsoft.Json;

namespace jgarcesS6.Views;

public partial class vActElim : ContentPage
{
	public vActElim(Estudiante datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.Codigo.ToString();
        txtNombre.Text = datos.Nombre;
        txtApellido.Text = datos.Apellido;
        txtEdad.Text = datos.Edad.ToString();
    }
	public async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var estudiante = new Estudiante
            {
                Codigo = int.Parse(txtCodigo.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Edad = int.Parse(txtEdad.Text)
            };

            var json = JsonConvert.SerializeObject(estudiante);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var url = $"https://localhost:7040/api/Estudiante/{estudiante.Codigo}";
                var response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Estudiante actualizado correctamente", "OK");
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    var mensaje = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Error al actualizar: {mensaje}", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo actualizar: " + ex.Message, "OK");
        }
    }
    public async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            int codigo = int.Parse(txtCodigo.Text);

            using (HttpClient client = new HttpClient())
            {
                var url = $"https://localhost:7040/api/Estudiante/{codigo}";
                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Estudiante eliminado correctamente", "OK");
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    var mensaje = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Error al eliminar: {mensaje}", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo eliminar: " + ex.Message, "OK");
        }
    }
}