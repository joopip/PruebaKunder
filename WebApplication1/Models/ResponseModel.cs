using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /// <summary>
    /// Objeto que devuelve los parámetros necesarios para la función Time.
    /// </summary>
    public class TimeResponseModel
    {
        /// <summary>
        /// Cadena de texto por defecto.
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// Cadena de texto por defecto.
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// DateTime convertida en formato UTC ISODATE.
        /// </summary>
        public DateTime data { get; set; }
    }

    /// <summary>
    /// Objeto que devuelve los parámetros necesarios para la función Word.
    /// </summary>
    public class WordResponseModel
    {
        /// <summary>
        /// Cadena de texto por defecto.
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// Cadena de texto por defecto.
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Cadena de texto convertida a Mayúsculas.
        /// </summary>
        public string data { get; set; }
    }
}