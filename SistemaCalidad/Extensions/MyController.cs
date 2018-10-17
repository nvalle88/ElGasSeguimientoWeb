using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCalidad.Extensions
{
    public static class MyController
    {
        /// <summary>
        /// Redirecciona a una Vista en el mismo Controlador.
        /// </summary>
        /// <param name="controlador">Controlador actual.</param>
        /// <param name="msg">Mensaje que saldrá en la parte superior derecha de la pantalla cuando cargue la Vista a la que se redirecciona.</param>
        /// <param name="nombreVista">Nombre de la Vista a la que se va a redireccionar, por defecto es a Index.</param>
        /// <returns></returns>
        public static IActionResult Redireccionar(this Controller controlador, string msg = null, string nombreVista = "Index")
        {
            if (!String.IsNullOrEmpty(msg))
                controlador.TempData["Mensaje"] = msg;

            return controlador.RedirectToAction(nombreVista);
        }


        /// <summary>
        /// Redirecciona a una Vista en el otro Controlador.
        /// </summary>
        /// <param name="controlador"></param>
        /// <param name="Controlador">Controlador al que se desa hacer la redirección</param>
        /// <param name="nombreVista">Acción a la que se desea redireccionar</param>
        /// <param name="msg">Mensaje que saldrá en la parte superior derecha de la pantalla cuando cargue la Vista a la que se redirecciona.</param>
        /// <returns></returns>
        public static IActionResult Redireccionar(this Controller controlador, string NombreControlador, string nombreVista, string msg = null)
        {
            if (!String.IsNullOrEmpty(msg))
                controlador.TempData["Mensaje"] = msg;

            return controlador.RedirectToAction(nombreVista, NombreControlador);
        }
        /// <summary>
        /// Redirecciona a una Vista en el otro Controlador con parámetros.
        /// </summary>
        /// <param name="controlador"></param>
        /// <param name="Controlador">Controlador al que se desa hacer la redirección</param>
        /// <param name="nombreVista">Acción a la que se desea redireccionar</param>
        /// <param name="parametros">Parametros que recive la acción que estamos invocando</param>
        /// <param name="msg">Mensaje que saldrá en la parte superior derecha de la pantalla cuando cargue la Vista a la que se redirecciona.</param>
        /// <returns></returns>
        public static IActionResult Redireccionar(this Controller controlador, string NombreControlador, string nombreVista, object parametros, string msg = null)
        {
            if (!String.IsNullOrEmpty(msg))
                controlador.TempData["Mensaje"] = msg;

            return controlador.RedirectToAction(nombreVista, NombreControlador, parametros);
        }

        /// <summary>
        /// Redirecciona a una Vista en el otro Controlador sin parámetros y permite controlar el tiempo que dura el mensaje
        /// </summary>
        /// <param name="controlador"></param>
        /// <param name="Controlador">Controlador al que se desa hacer la redirección</param>
        /// <param name="nombreVista">Acción a la que se desea redireccionar</param>
        /// <param name="msg">Mensaje que saldrá en la parte superior derecha de la pantalla cuando cargue la Vista a la que se redirecciona.</param>
        /// <returns></returns>
        public static IActionResult RedireccionarMensajeTime(this Controller controlador, string NombreControlador, string nombreVista, string msg = null)
        {
            if (!String.IsNullOrEmpty(msg))
                controlador.TempData["MensajeTimer"] = msg;

            return controlador.RedirectToAction(nombreVista, NombreControlador);
        }


        /// <summary>
        /// Redirecciona a una Vista en el otro Controlador con parámetros y permite controlar el tiempo que dura el mensaje
        /// </summary>
        /// <param name="controlador"></param>
        /// <param name="Controlador">Controlador al que se desa hacer la redirección</param>
        /// <param name="nombreVista">Acción a la que se desea redireccionar</param>
        /// <param name="parametros">Parametros que recive la acción que estamos invocando</param>
        /// <param name="msg">Mensaje que saldrá en la parte superior derecha de la pantalla cuando cargue la Vista a la que se redirecciona.</param>
        /// <returns></returns>
        public static IActionResult RedireccionarMensajeTime(this Controller controlador, string NombreControlador, string nombreVista, object parametros, string msg = null)
        {
            if (!String.IsNullOrEmpty(msg))
                controlador.TempData["MensajeTimer"] = msg;

            return controlador.RedirectToAction(nombreVista, NombreControlador, parametros);
        }



    }
}
