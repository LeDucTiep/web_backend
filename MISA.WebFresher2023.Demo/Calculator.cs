namespace MISA.WebFresher2023.Demo
{
    public class Calculator
    {
        /// <summary>
        /// Hàm cộng hai số nguyên 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>số nguyên</returns>
        public long Add(int a, int b)
        {
            return a + (long)b;
        }
        /// <summary>
        /// Hàm trừ hai số nguyên 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public long Sub(int a, int b)
        {
            return a - (long)b;
        }
        /// <summary>
        /// Hàm nhân 2 số nguyên
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public long Mul(int a, int b)
        {
            return (long)a * b;
        }
        /// <summary>
        /// Hàm chia hai số nguyên 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException"></exception>
        public double Div(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException("Không được chia cho 0");
            return a / (double)b;
        }
    }
}
