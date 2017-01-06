using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// <p>Funciones para Prueba KUNDER.</p>
    /// </summary>
    public class PruebaKUNDERController : ApiController
    {
        // POST api/word
        /// <summary>
        /// <p>Recibe una palábra de largo 4 (String) y devuelve su valor en mayúsculas.</p>
        /// <p><b>CONSIDERACIONES:</b></p>
        /// <ul>
        /// <li>El objeto JSON enviado puede tener más atributos, pero solo se considerará el atributo 'data'.</li>
        /// <li>La cadena de texto 'data' puede contener letras Mayúsculas y Minúsculas además de ñ y acentos (á,é,í,ó,ú).</li>
        /// </ul>
        /// </summary>
        /// <param name="jsonbody">El objeto JSON recibido.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [Route("word")]
        public WordResponseModel Word([FromBody]JToken jsonbody)
        {
            try
            {
                //Deserializa el objeto JSON recibido en el Body.
                dynamic j = JsonConvert.DeserializeObject(jsonbody.ToString());
                string data = j.data;

                //Comprueba que el objeto JSON recibido contenga datos en la variable 'data'.
                if (!string.IsNullOrEmpty(data))
                {
                    //Comprueba que la cadena recibida contenga sólo 4 carácteres y que sólo sean letras. 
                    if (data.Length == 4 && Regex.IsMatch(data, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$"))
                    {
                        return new WordResponseModel()
                        {
                            code = "00",
                            description = "OK",
                            data = data.ToUpper()
                        };
                    }
                    else
                    {
                        throw new HttpException();
                    }
                }
                else
                {
                    throw new HttpException();
                }
            }
            catch(HttpException ex)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        // GET api/time/{DateTime}
        /// <summary>
        /// <p>Toma la hora de value y la devuelve en formato UTC ISODATE.</p>
        /// <p><b>CONSIDERACIONES:</b></p>
        /// <ul>
        /// <li>La cadena de texto DateTime debe contener parámetros de hora.</li>
        /// <li>Cadenas de texto que no denoten un uso horario válido serán convertidos a UTC según uso horario por defecto del sistema.</li>
        /// <li>La fecha en el resultado siempre será 0001-01-01, ya que sólo se trabaja con la hora.</li>
        /// </ul>
        /// </summary>
        /// <param name="value">Cadena de texto válida para un DateTime.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [Route("time")]
        public TimeResponseModel Time(string value)
        {
            try
            {
                //Comprueba si la cadena de texto DateTime contiene parámetros de hora.
                if (value.Contains(":") || value.ToLower().Contains("am") || value.ToLower().Contains("pm"))
                {
                    //Cadenas de texto que no denoten un uso horario válido serán convertidos a UTC según uso horario por defecto del sistema.
                    DateTime result = new DateTime(DateTime.Parse(value, CultureInfo.InvariantCulture).ToUniversalTime().TimeOfDay.Ticks,DateTimeKind.Utc);
                    return new TimeResponseModel()
                    {
                        code = "00",
                        description = "OK",
                        data = result
                    };
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
