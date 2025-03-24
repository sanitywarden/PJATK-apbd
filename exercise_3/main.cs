List<Container> containers1 = new List<Container>();
List<Container> containers2 = new List<Container>();

for(int i = 0; i < 20; i++) {
    var random = new Random();

    // we dont care to handle overfill exceptions here so
    // just give a small mass to cargo 
    var mass = random.NextDouble() % 500 + 10;
    var container_mass = random.NextDouble() % 10;
    FluidContainer c1 = new FluidContainer(mass, random.NextDouble() % 100 + 500, container_mass, random.NextDouble() % 100, 900, FluidCargoType.FLUID_DANGEROUS);
    FluidContainer c2 = new FluidContainer(mass, random.NextDouble() % 100 + 500, container_mass, random.NextDouble() % 100, 900, FluidCargoType.FLUID_SAFE);

    containers1.Add(c1);
    containers2.Add(c2);

    GasContainer c3 = new GasContainer(mass, random.NextDouble() % 100 + 500, container_mass, random.NextDouble() % 100, 900, 1);
    GasContainer c4 = new GasContainer(mass, random.NextDouble() % 100 + 500, container_mass, random.NextDouble() % 100, 900, 1);

    containers1.Add(c3);
    containers2.Add(c4);

    CoolingContainer c5 = new CoolingContainer(mass, random.NextDouble() % 100 + 500, container_mass, random.NextDouble() % 100, 900, ProductType.PRODUCT_MEAT, 0.0);
    CoolingContainer c6 = new CoolingContainer(mass, random.NextDouble() % 100 + 500, container_mass, random.NextDouble() % 100, 900, ProductType.PRODUCT_FISH, -10.0);

    containers1.Add(c5);
    containers2.Add(c6);
}

Ship s1 = new Ship(containers1, 10, 1000, 100000000);
Ship s2 = new Ship(containers2, 10, 1000, 100000000);

// The serial has to exist obviously for the below to succeed
// The two chosen ones exist
// If instead of the first one we would choose "KON-G-1" it would fail
string first_ship_serial1 = "KON-L-1";
string first_ship_serial2 = "KON-C-119";
string second_ship_serial1 = "KON-G-4";

Console.WriteLine("=====");
// this works fine if serials exist
if(s1.swap_containers(first_ship_serial1, first_ship_serial2))
    Console.WriteLine(string.Format("Swapped containers {0} and {1}", first_ship_serial1, first_ship_serial2));
else Console.WriteLine("Failed swapping containers");

Console.WriteLine("=====");
// this works fine if serials exist
if(s1.swap_containers_between_ships(first_ship_serial1, second_ship_serial1, s2))
    Console.WriteLine(string.Format("Swapped containers {0} and {1} between ships", first_ship_serial1, second_ship_serial1));
else Console.WriteLine("Failed swapping containers between ships");

Console.WriteLine("=====");
// first does not work because we swapped the containers between ships so now it's on ship 2
if(s1.remove_container(first_ship_serial1))
    Console.WriteLine("Removed container");
else Console.WriteLine("Failed to remove container");

// this works fine if serial exists
if(s1.remove_container(first_ship_serial2))
    Console.WriteLine("Removed container");
else Console.WriteLine("Failed to remove container");

Console.WriteLine("=====");
// displays one less because we deleted it
s1.get_info();
s2.get_info();