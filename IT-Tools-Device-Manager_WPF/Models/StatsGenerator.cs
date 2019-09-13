using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITTools_DeviceManager_WPF.Models
{
    public static class StatsGenerator
    {

        public static Stats CountDeviceTypes(List<Device> devices)
        {
            Stats s = new Stats();

            if (devices.Count > 0)
            {

                foreach (Device d in devices)
                {
                    if (d.PcType.ToLower() == "notebook" || d.PcType.ToLower() == "laptop")
                    {
                        s.NumberOfNotebooks++;
                    }
                    else
                    {
                        s.NumberOfDesktops++;
                    }
                }
            }

            return s;

        }
    }
}
