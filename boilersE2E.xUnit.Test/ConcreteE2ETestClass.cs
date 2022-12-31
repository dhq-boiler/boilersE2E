using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boilersE2E.xUnit.Test
{
    internal class ConcreteE2ETestClass : E2ETestClass
    {
        static ConcreteE2ETestClass()
        {
            E2ETestFixture.EnvironmentVariableNameWhereWinAppDriverRunAutomatically = "BOILERS_E2ETEST_IS_VALID";
            E2ETestFixture.EnvironmentVariableNameWhereSetWindowSizeManually = "BOILERS_E2ETEST_SET_WINDOWSIZE_MANUAL";
        }
    }
}
