using CM.Data.ViewModels.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.ServiceInterfaces
{
    public interface IEmailService
    {
        bool SendAppointmentEMail(AppointmentViewModel appointee);
    }
}
