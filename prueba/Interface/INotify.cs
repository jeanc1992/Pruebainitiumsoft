namespace prueba.Interface
{
    public enum Tipo
    {
        success,
        alert,
        error

    }

    public interface INotify
    {
        string Notificacion(string Mensaje, Tipo tipo);


    }


}
