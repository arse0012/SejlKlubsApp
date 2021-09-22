using SejlKlubsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SejlKlubsApp.Services.Service
{
    public class LogInService
    {
        private Sailor _loggedInSailor;

        public void SailorLogIn(Sailor sailor)
        {
            _loggedInSailor = sailor;
        }
        public void SailorLogOut()
        {
            _loggedInSailor = null;
        }
        public Sailor GetLoggedSailor()
        {
            return _loggedInSailor;
        }
    }
}
