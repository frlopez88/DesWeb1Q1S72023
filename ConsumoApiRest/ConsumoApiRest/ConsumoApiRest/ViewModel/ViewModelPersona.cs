using ConsumoApiRest.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using Xamarin.Forms;

namespace ConsumoApiRest.ViewModel
{
    public class ViewModelPersona : INotifyPropertyChanged
    {

        public ViewModelPersona() {

            

            listarPersonas = new Command( async () => {

                ListPersonas = new ObservableCollection<persona>();

                HttpClient httpClient = new HttpClient();

                var respuesta = await httpClient.GetAsync(url);

                if (respuesta.IsSuccessStatusCode) {
                
                    var contenido = await respuesta.Content.ReadAsStringAsync();
                    JsonSerializerOptions opciones = new JsonSerializerOptions() { 
                        PropertyNameCaseInsensitive = true
                    };
                    var listado = System.Text.Json.JsonSerializer.Deserialize<List<persona>>(contenido, opciones);


                    foreach (var item in listado) {

                        ListPersonas.Add(item);


                    }
                
                }


            });
        
        }
        

        ObservableCollection<persona> listPersonas = new ObservableCollection<persona>();

        public ObservableCollection<persona> ListPersonas {
            get => listPersonas;
            set { 
            
                listPersonas = value;
                var args = new PropertyChangedEventArgs(nameof(ListPersonas));
                PropertyChanged?.Invoke(this, args);

            }


        }

        string url = "https://desfrlopez.me/ejemplo/api/persona";

        string nombre;
        public string Nombre {

            get => nombre;
            set {

                nombre = value;
                var args = new PropertyChangedEventArgs(nameof(Nombre));
                PropertyChanged?.Invoke(this, args);

            }
            
        }

        string apellido;
        public string Apellido
        {

            get => apellido;
            set
            {

                apellido = value;
                var args = new PropertyChangedEventArgs(nameof(Apellido));
                PropertyChanged?.Invoke(this, args);

            }

        }

        DateTime fechaNacimiento;
        public DateTime FechaNacimiento
        {

            get => fechaNacimiento;
            set
            {

                fechaNacimiento = value;
                var args = new PropertyChangedEventArgs(nameof(FechaNacimiento));
                PropertyChanged?.Invoke(this, args);

            }

        }


        public DateTime FechaMin { get; set; } = new DateTime(1980, 01, 01);


        public Command listarPersonas { get; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
