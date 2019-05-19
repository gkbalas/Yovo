using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yovo.Models;
using Newtonsoft.Json;

namespace Yovo.Viewmodel
{
    public class ReservationCheckVM
    {
        public Reservation NewReservation { get; set; }
        public IList<Reservation> OldReservations;
    }
}