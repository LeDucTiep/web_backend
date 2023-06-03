namespace MISA.WebFresher2023.Demo.Common.MyException
{
    public class InternalException : Exception
    {
        #region Field
        /// <summary>
        /// Mã lỗi nội bộ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public int ErrorCode { get; set; }
        #endregion

        #region Contructor
        public InternalException() { }
        public InternalException(string? message) : base(message)
        {
        } 
        #endregion
    }
}
