using System.ComponentModel;

namespace RealEstate.Constants
{
    public enum PropertyType
    {
        Flats,
        Plots,
        Resale,
        Farmlands,
        [Description("Commercial Properties")]
        CommercialProperties,
        Investments
    }
}
