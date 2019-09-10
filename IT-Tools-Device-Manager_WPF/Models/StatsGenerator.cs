using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITTools_DeviceManager_WPF.Models
{
    public static class StatsGenerator
    {

        public static (int desktopsCount, int notebooksCount) CountDeviceTypes(List<Device> devices)
        {
            var desktopsCounter = 0;
            var notebooksCounter = 0;
            
            foreach(Device d in devices)
            {
                if(d.PcType=="Notebook" || d.PcType == "Laptop")
                {
                    notebooksCounter++;
                }
                else
                {
                    desktopsCounter++;
                }
            }

            return (desktopsCounter, notebooksCounter);
        }

    }
}
