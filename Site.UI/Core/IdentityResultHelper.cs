using Microsoft.AspNet.Identity;
using System;
using System.Linq;

namespace Site.UI.Core
{
    public static class IdentityResultHelper
    {
        public static bool Validate(this IdentityResult identityResult, ref Exception exception)
        {
            if (object.Equals(identityResult, null))
            {
                exception = new ArgumentNullException("IdentityResult");
                return false;
            }

            if (!object.Equals(identityResult.Errors, null))
            {
                var error = identityResult.Errors.FirstOrDefault();
                if (!string.IsNullOrEmpty(error))
                    exception = new Exception(error);
            }

            return identityResult.Succeeded;
        }
    }
}