namespace FacturacionCLN.Services
{
    public class TasaCambioService
    {
        public (bool IsValid, string ErrorMessage) ValidarTasaCambio(decimal tasaCambio)
        {
            if (tasaCambio <= 0)
            {
                return (false, "La tasa de cambio debe ser un número positivo.");
            }

            return (true, null);
        }
    }
}
