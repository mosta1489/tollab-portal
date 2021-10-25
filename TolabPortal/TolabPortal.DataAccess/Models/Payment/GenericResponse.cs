using System.Collections.Generic;

namespace TolabPortal.DataAccess.Models.Payment
{
    public class GenericResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
        public T Data { get; set; }
    }
    public class ValidationError
    {
        public string Name { get; set; }
        public string Error { get; set; }
    }
}
