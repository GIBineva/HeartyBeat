using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeartyBeatApp.Data
{
    public enum VehicleType
    {
        [Display(Name = "Car")]
        CAR = 0,
        [Display(Name = "Van")]
        VAN,
        [Display(Name = "Train")]
        TRAIN,
        [Display(Name = "Ship")]
        SHIP,
        [Display(Name = "Bus")]
        BUS
    }
}
