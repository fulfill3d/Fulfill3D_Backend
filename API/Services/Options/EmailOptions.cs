namespace Fulfill3D.API.API.Services.Options
{
    public class EmailOptions(string email, string subject, string message)
    {
        public readonly string Message = $"Dear admin,\n\nThe following address:\n\n{email}\n\nhas sent you the following:\n\nSubject:\n{subject}\n\nMessage:\n{message}";
    }
}