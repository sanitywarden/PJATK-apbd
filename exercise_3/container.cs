
using System.Linq.Expressions;

public class OverfillException(string message) : Exception(message) {} 

public enum ContainerType {
    CONTAINER_FLUID = (int)'L',
    CONTAINER_GAS = (int)'G',
    CONTAINER_COOLING = (int)'C'
}

public abstract class Container (
    double total_mass_kg,
    double height_cm,
    double container_mass_kg,
    double depth_cm,
    double max_mass_kg,
    ContainerType container_type
) {
    private static int CONTAINER_COUNT = 0;
    private readonly static int END_NUMBER_LENGTH = 3;

    public double MassKG { get; set; } = total_mass_kg;
    public double HeightCM { get; set; } = height_cm;
    public double ContainerMassKG { get; set; } = container_mass_kg;
    public double DepthCM { get; set; } = depth_cm;
    public string SerialNumber { get; set; } = create_serial(container_type);
    public double MaxMassKG { get; set; } = max_mass_kg;

    public abstract void get_info();

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

interface IHazardNotifier {
    public abstract void send_notification(string message);
}

public enum FluidCargoType {
    FLUID_DANGEROUS,
    FLUID_SAFE
}

public class FluidContainer (
    double total_mass_kg,
    double height_cm,
    double container_mass_kg,
    double depth_cm,
    double max_mass_kg,
    FluidCargoType cargo_type
) : Container(total_mass_kg, height_cm, container_mass_kg, depth_cm, max_mass_kg, ContainerType.CONTAINER_FLUID), IHazardNotifier {
    private FluidCargoType CargoType { get; set; } = cargo_type;

    public override void get_info() {
        Console.WriteLine(String.Format("Fluid container | total_mass={0} fluid_cargo_type={1} ", this.MassKG, this.CargoType ));
    }

    public void send_notification(string message) {
        Console.WriteLine(message);
    }

    public new void load_container_with(double cargo_mass) {
        base.load_container_with(cargo_mass);

        if(this.CargoType == FluidCargoType.FLUID_DANGEROUS && this.MassKG >= this.MaxMassKG / 2)
            this.send_notification(string.Format("Hazardous operation with container {0} with unsafe fluid", this.SerialNumber));
        else if(this.CargoType == FluidCargoType.FLUID_SAFE && this.MassKG >= 0.9 * this.MaxMassKG)
            this.send_notification(string.Format("Hazardous operation with container {0} with safe fluid", this.SerialNumber));
    }
}

public class GasContainer (
    double total_mass_kg,
    double height_cm,
    double container_mass_kg,
    double depth_cm,
    double max_mass_kg,
    double pressure_atmosphere
) : Container(total_mass_kg, height_cm, container_mass_kg, depth_cm, max_mass_kg, ContainerType.CONTAINER_GAS), IHazardNotifier {
    
    public double PressureAtmoshperes { get; set; } = pressure_atmosphere;

    public override void get_info() {
        Console.WriteLine(String.Format("Gas container | total_mass={0} pressure={1}", this.MassKG, this.PressureAtmoshperes));
    }

    public void send_notification(string message) {
        Console.WriteLine(message);
    }

    public new void empty_container() {
        this.MassKG *= 0.05;
    }   

    // "Jeśli masa ładunku przekroczy dopuszczalną ładowność - chcemy zwrócić błąd"
    // load_container_with(double) juz ma funkcjonalnosc zwracania bledu w przypadku przeladowania
};

public enum ProductType {
    PRODUCT_MEAT,
    PRODUCT_FISH,
    PRODUCT_ICE_CREAM,
    PRODUCT_BANANAS
};

public class CoolingContainer (
    double total_mass_kg,
    double height_cm,
    double container_mass_kg,
    double depth_cm,
    double max_mass_kg,
    ProductType product_type,
    double cooling_temperature_celsius
) : Container(total_mass_kg, height_cm, container_mass_kg, depth_cm, max_mass_kg, ContainerType.CONTAINER_COOLING) {
    public ProductType ProductType { get; set; } = product_type;
    public double CoolingTemperatureCelsius { get; set; } = cooling_temperature_celsius;

    private Dictionary<ProductType, double> MAX_PRODUCT_COOLING_TEMPERATURES = new Dictionary<ProductType, double> {
        [ProductType.PRODUCT_BANANAS] = 13.3,
        [ProductType.PRODUCT_FISH] = 2.0,
        [ProductType.PRODUCT_ICE_CREAM] = -18.0, 
        [ProductType.PRODUCT_MEAT] = -15.0
    };
    public override void get_info() {
        Console.WriteLine(String.Format("Cooling container | total_mass={0} product_type={1} temperature={2}", this.MassKG, this.ProductType, this.CoolingTemperatureCelsius));
    }

    public new void load_container_with(double cargo_mass) {
        base.load_container_with(cargo_mass);

        // "Temperatura kontenera nie może być niższa niż temperatura wymagana przez dany rodzaj produktu" 
        if(MAX_PRODUCT_COOLING_TEMPERATURES[this.ProductType] > this.CoolingTemperatureCelsius) 
            this.CoolingTemperatureCelsius = MAX_PRODUCT_COOLING_TEMPERATURES[this.ProductType];
    }
}