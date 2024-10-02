using System.ComponentModel.DataAnnotations;

namespace ServiceHub.Domain.Models
{
    public class GenericRequired : RequiredAttribute
    {
        public GenericRequired()
        {
            ErrorMessage = "Field required";
        }
    }
}