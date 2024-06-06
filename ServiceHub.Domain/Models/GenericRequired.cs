using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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