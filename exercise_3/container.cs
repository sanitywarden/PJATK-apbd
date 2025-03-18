
using System.ComponentModel.DataAnnotations;

public class OverfillException(string message) : Exception(message) {} 

public enum ContainerType {
    CONTAINER_FLUID = (int)'L',
    CONTAINER_GAS = (int)'G',
    CONTAINER_COOLING = (int)'C'
}

public class Container (
    double total_mass_kg,
    double height_cm,
    double container_mass_kg,
    double depth_cm,
    double max_mass_kg,
    ContainerType container_type
) {
    private static int CONTAINER_COUNT = 0;
    readonly static int END_NUMBER_LENGTH = 3;

    public double MassKG { get; set; } = total_mass_kg;
    public double HeightCM { get; set; } = height_cm;
    public double ContainerMassKG { get; set; } = container_mass_kg;
    public double DepthCM { get; set; } = depth_cm;
    public string SerialNumber { get; set; } = create_serial(container_type);
    public double MaxMassKG { get; set; } = max_mass_kg;

    private static string create_serial(ContainerType container_type) {
        string generated_number = string.Format("{0}", ++CONTAINER_COUNT);
        int digits = generated_number.Length;
        for(int i = 0; i < END_NUMBER_LENGTH - digits; i++)
            generated_number = '0' + generated_number;
        return string.Format("KON-{0}-{1}", (char)container_type, generated_number);
    }

    public void empty_container() {
        this.MassKG = this.ContainerMassKG;
    }

    public void load_container_with(double cargo_mass) {
        if(this.MassKG + cargo_mass > this.MaxMassKG)
            throw new OverfillException(string.Format("Container is overfilled by {0}", this.MassKG + cargo_mass % this.MaxMassKG));
        this.MassKG += cargo_mass;
    }
}

public class FluidContainer (
    double total_mass_kg,
    double height_cm,
    double container_mass_kg,
    double depth_cm,
    double max_mass_kg
) : Container(total_mass_kg, height_cm, container_mass_kg, depth_cm, max_mass_kg, ContainerType.CONTAINER_FLUID) {}

public class GasContainer (
    double total_mass_kg,
    double height_cm,
    double container_mass_kg,
    double depth_cm,
    double max_mass_kg
) : Container(total_mass_kg, height_cm, container_mass_kg, depth_cm, max_mass_kg, ContainerType.CONTAINER_GAS) {}

public class CoolingContainer (
    double total_mass_kg,
    double height_cm,
    double container_mass_kg,
    double depth_cm,
    double max_mass_kg
) : Container(total_mass_kg, height_cm, container_mass_kg, depth_cm, max_mass_kg, ContainerType.CONTAINER_COOLING) {}