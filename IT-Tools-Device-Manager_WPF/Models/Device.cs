using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITTools_DeviceManager_WPF.Models
{
    [Table("deviceDB")]
    public partial class Device
    {
        [Key]
        [Column("user_ID")]
        public long UserId { get; set; }

        [Required]
        [Column("user_Username")]
        public string UserUsername { get; set; }

        [Required]
        [Column("user_Location")]
        public string UserLocation { get; set; }

        [Required]
        [Column("user_Department")]
        public string UserDepartment { get; set; }

        [Required]
        [Column("pc_ComputerName")]
        public string PcComputerName { get; set; }

        [Required]
        [Column("pc_SerialNumber")]
        public string PcSerialNumber { get; set; }

        [Required]
        [Column("pc_NameMatching", TypeName = "BOOLEAN")]
        public string PcNameMatching { get; set; }

        [Required]
        [Column("pc_Manufacturer")]
        public string PcManufacturer { get; set; }

        [Required]
        [Column("pc_Model")]
        public string PcModel { get; set; }

        [Required]
        [Column("pc_Type")]
        public string PcType { get; set; }


        [Required]
        [Column("pc_OSversion")]
        public string PcOsversion { get; set; }

        [Required]
        [Column("pcSpecs_CPU")]
        public string PcSpecsCpu { get; set; }

        [Required]
        [Column("pcSpecs_RAMmoduleDetails")]
        public string PcSpecsRammoduleDetails { get; set; }

        [Required]
        [Column("pcSpecs_RAMinstalled")]
        public string PcSpecsRaminstalled { get; set; }

        [Required]
        [Column("pcSpecs_RAMcapabilities")]
        public string PcSpecsRamcapabilities { get; set; }

        [Required]
        [Column("pcSpecs_HDDmodel")]
        public string PcSpecsHddmodel { get; set; }

        [Required]
        [Column("pcSpecs_HDDcapacity")]
        public string PcSpecsHddcapacity { get; set; }

        [Required]
        [Column("periph_MonitorsConnected")]
        public string PeriphMonitorsConnected { get; set; }

        [Required]
        [Column("periph_InputDevice")]
        public string PeriphInputDevice { get; set; }

        [Required]
        [Column("periph_DockingStation", TypeName = "BOOLEAN")]
        public string PeriphDockingStation { get; set; }

        [Required]
        [Column("periph_TPM_BitLocker_ON", TypeName = "BOOLEAN")]
        public string PeriphTpmBitLockerOn { get; set; }
    }
}
