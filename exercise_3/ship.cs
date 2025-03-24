public class Ship(
    List<Container> containers,
    double speed_km,
    int max_container_load,
    int max_weight_kg
) {
    public List<Container> Containers { get; set; } = containers;
    public double SpeedKM { get; set; } = speed_km;
    public int MaxContainerLoad { get; set; } = max_container_load;
    public int MaxWeightKG { get; set; } = max_weight_kg;

    public void add_container(Container container) {
        this.Containers.Add(container);
    }

    public void add_container(Container[] containers) {
        foreach(var container in containers)
            this.add_container(container);
    }

    public bool remove_container(string serial_number) {
        try {
            Container? container = this.Containers.Find((Container c) => { return c.SerialNumber == serial_number; });
            if(container != null) {
                this.Containers.Remove(container);
                return true;
            }
            return false;
        }
        catch(Exception e) {
            return false;
        }
    }

    public bool swap_containers(string first_serial, string second_serial) {
        int index_first = this.Containers.FindIndex(0, this.Containers.Count, (Container c) => { return c.SerialNumber == first_serial; });
        int index_second = this.Containers.FindIndex(0, this.Containers.Count, (Container c) => { return c.SerialNumber == second_serial; });
        
        if(index_first == -1 || index_second == -1)
            return false;
        
        Container temp = this.Containers[index_first];
        this.Containers[index_first] = this.Containers[index_second];
        this.Containers[index_second] = temp;
    
        return true;
    }

    public bool swap_containers_between_ships(string serial_first_ship, string serial_second_ship, Ship other_ship) {
        int index_on_first = this.Containers.FindIndex(0, this.Containers.Count, (Container c) => { return c.SerialNumber == serial_first_ship; });
        int index_on_second = other_ship.Containers.FindIndex(0, other_ship.Containers.Count, (Container c) => { return c.SerialNumber == serial_second_ship; });

        if(index_on_first == -1 || index_on_second == -1)
            return false;

        Container temp = this.Containers[index_on_first];
        this.Containers[index_on_first] = other_ship.Containers[index_on_second];
        other_ship.Containers[index_on_second] = temp;
    
        return true;
    }

    public void get_info() {
        Console.WriteLine(String.Format("Cargo ship | containers={0} speed={1}", this.Containers.Count, this.SpeedKM));
    }
}