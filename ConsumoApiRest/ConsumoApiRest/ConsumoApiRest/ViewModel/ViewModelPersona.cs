using ConsumoApiRest.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using Xamarin.Forms;

namespace ConsumoApiRest.ViewModel
{
    public class ViewModelPersona : INotifyPropertyChanged
    {

        public ViewModelPersona() {



            crearPersonas = new Command( async () => {


                using (var httpClient = new HttpClient()) {

                    persona body1 = new persona() { 
                        nombre_persona = this.nombre, 
                        apellido_persona = this.apellido , 
                        fecha_nacimiento = this.fechaNacimiento
                    };

                    var myContent = JsonConvert.SerializeObject(body1);
                    var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

                    var respuesta = await httpClient.PostAsync(url, stringContent);

                    if (respuesta.IsSuccessStatusCode) {
                        
                        getPersonas();
                    
                    }

                }

            });


            actualizarPersona = new Command(async() => {

                using (var httpClient = new HttpClient())
                {

                    persona body1 = new persona()
                    {
                        nombre_persona = this.nombre,
                        apellido_persona = this.apellido,
                        fecha_nacimiento = this.fechaNacimiento
                    };

                    var myContent = JsonConvert.SerializeObject(body1);
                    var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

                    var respuesta = await httpClient.PutAsync(url+"/"+personaSeleccionada.id_persona, stringContent);

                    if (respuesta.IsSuccessStatusCode)
                    {

                        getPersonas();

                    }

                }

            });

            borrarPersona = new Command(async () => {

                using (var httpClient = new HttpClient())
                {

                    var respuesta = await httpClient.DeleteAsync(url + "/" + personaSeleccionada.id_persona);

                    if (respuesta.IsSuccessStatusCode)
                    {

                        getPersonas();

                    }

                }

            } );


            actualizarFormulario = new Command(() => {

                Nombre = personaSeleccionada.nombre_persona;
                Apellido = personaSeleccionada.apellido_persona;
                FechaNacimiento = personaSeleccionada.fecha_nacimiento;

            } );



            getPersonas();

        }

        private async void getPersonas() {

            ListPersonas = new ObservableCollection<persona>();

            HttpClient httpClient = new HttpClient();

            var respuesta = await httpClient.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {

                var contenido = await respuesta.Content.ReadAsStringAsync();
                JsonSerializerOptions opciones = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var listado = System.Text.Json.JsonSerializer.Deserialize<List<persona>>(contenido, opciones);


                foreach (var item in listado)
                {

                    ListPersonas.Add(item);


                }

            }
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


        persona personaSeleccionada = new persona();

        public persona PersonaSeleccionada {

            get => personaSeleccionada;
            set {

                personaSeleccionada = value;
                var args = new PropertyChangedEventArgs(nameof(PersonaSeleccionada));
                PropertyChanged?.Invoke(this, args);

            }

        }


        public DateTime FechaMin { get; set; } = new DateTime(1980, 01, 01);


        public Command crearPersonas { get; }
        public Command actualizarFormulario { get; }
        public Command actualizarPersona { get; set; }
        public Command borrarPersona { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
