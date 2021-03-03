using Newtonsoft.Json;
using System;

namespace prueba.Interface
{
    public class Notify : INotify
    {
        public string Notificacion(string Mensaje, Tipo tipo)
        {
            var tp = Enum.GetName(typeof(Tipo), tipo);
            return JsonConvert.SerializeObject(new { Tipo = tp, Message = Mensaje });

        }
    }
}
