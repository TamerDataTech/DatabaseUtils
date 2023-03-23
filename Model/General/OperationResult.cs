namespace DatabaseUtils.Model.General
{
    public class OperationResult<T>
    {
        

        public bool Result { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public T Response { get; set; }
    }
}
