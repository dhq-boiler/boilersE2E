using Xunit;

namespace boilersE2E.xUnit
{
    /// <summary>
    /// https://stackoverflow.com/questions/48262699/multiple-different-fact-attributes-for-xunit-test-or-alternative-to-multiple-fac
    /// </summary>
    public class MultiFact : FactAttribute
    {
        public MultiFact(params Type[] types)
        {
            var result = types.Select(Activator.CreateInstance).Cast<FactAttribute>().ToList();

            if (result.Any(it => !string.IsNullOrEmpty(it.Skip)))
            {
                Skip = string.Join(", ", result.Where(it => !string.IsNullOrEmpty(it.Skip)).Select(it => it.Skip));
            }
        }
    }
}
