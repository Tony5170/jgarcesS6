using System.Collections.ObjectModel;
using System.Text.Json;
using jgarcesS6.Models;
using Newtonsoft.Json;

namespace jgarcesS6.Views;

public partial class vCrud : ContentPage
{

	private const string URL = "https://localhost:7040/api/Estudiante";
	private HttpClient cliente = new HttpClient();
	private ObservableCollection<Estudiante> _estudianteTem;
	public vCrud()
	{
		InitializeComponent();
		mostrarEstudiantes();
    }

	public async void mostrarEstudiantes()
	{
		var content = await cliente.GetStringAsync(URL);
		List<Estudiante> estudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(content);
        _estudianteTem = new ObservableCollection<Estudiante>(estudiantes);
		lvEstudiantes.ItemsSource = _estudianteTem;
    }
}
