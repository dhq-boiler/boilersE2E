using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace boilersE2E.MsTest
{
    public class ConditionalEnvironmentVariableAttribute : Attribute
    {
        public ConditionalEnvironmentVariableAttribute(string environmentVariable) 
        {
            var value = Environment.GetEnvironmentVariable(environmentVariable);
            if (value is null)
            {
                Assert.Inconclusive($"{environmentVariable} が定義されていません。");
            }
            Console.WriteLine($"Defined Environment Variable {environmentVariable} = {value}");
        }
    }
}
