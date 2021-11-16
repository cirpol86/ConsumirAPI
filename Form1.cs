using ConsumirAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;


namespace ConsumirAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

               private async void button1_Click(object sender, EventArgs e)
        {
            string  idfactura = textBox1.Text;            
            if(!string.IsNullOrWhiteSpace(idfactura))
            {
              string token =  ObtenerToken();
               Facturas factur =ObtenrDatosConsultaAPI(token,idfactura);
                if(factur != null)
                {
                    textBox2.Text =factur.TipoPago.ToString();
                    textBox3.Text = factur.DocumentoCliente.ToString();
                    textBox4.Text = factur.NombreCliente.ToString();
                    textBox5.Text = factur.SubTotal.ToString();
                    textBox6.Text = factur.Descuento.ToString();
                    textBox7.Text = factur.IVA.ToString();
                    textBox8.Text = factur.TotalDescuento.ToString();
                    textBox9.Text = factur.TotalImpuesto.ToString();
                    textBox10.Text = factur.Total.ToString();
                }
            }
        }

       Facturas ObtenrDatosConsultaAPI(string token, string id) {

            string respuesta = "";
            var url = $"https://localhost:5001/api/Facturas/{id}";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                             respuesta = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(respuesta);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }

            Facturas facturaRespuesta = JsonConvert.DeserializeObject<Facturas>(respuesta);
            if (facturaRespuesta != null) { 
                return facturaRespuesta;
            }else
                return null;
      

        }
        public string ObtenerToken()
        {
            string token = "";
            string nombre = "cpolo";
            string password = "123456789";
            string respuesta = "";
            var url = $"https://localhost:5001/api/login";
            var request = (HttpWebRequest)WebRequest.Create(url);
            string json = $"{{\"usuario\":\"{nombre}\",\"password\":\"{password}\"}}";
            request.Method = "POST";
           
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            respuesta = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(respuesta);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
           
            JObject o = JObject.Parse(respuesta);
            string primerAtributo = (string)o.Properties().First().Value;

            return primerAtributo;
        }
    }
}
