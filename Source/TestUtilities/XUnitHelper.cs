using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

using Xunit;

namespace Syndic.ImageComparer.TestUtilities
{

    /// <summary>
    /// Defines methods for extending xUnit.
    /// </summary>
    public class XUnitHelper
    {

        /// <summary>
        /// Invokes an action and checks to see that a specified constraint was fired.
        /// </summary>
        /// <param name="action">The operation to perform.</param>
        /// <param name="constraintText">The text of the constraint message.</param>
        [DebuggerStepThrough]
        public static void ViolatesConstraint(Action action, string constraintText)
        {
            bool conditionFailed = false;
            EventHandler<ContractFailedEventArgs> handler = (o, e) => conditionFailed = e.Condition == constraintText;
            Contract.ContractFailed += handler;

            try
            {
                action.Invoke();
            }
            catch (Exception)
            {
            }

            Contract.ContractFailed -= handler;
            Assert.True(conditionFailed, "Expected constraint violation was not received.");

        }

    }

}
