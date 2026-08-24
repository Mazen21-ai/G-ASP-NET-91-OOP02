/*
Question1
A)
Struct → Value Type.
Class → Reference Type.
Class supports inheritance, struct doesn't.
 
B)Classes are better for large applications because they support inheritance, encapsulation, and code reuse.
*/

/*
Question 2
A) Parent: Shipment
B) Child: ExpressShipment
C) Inherited members: TrackingCode, Description, Weight, DeliveryFee, Destination, EstimatedCost, and methods مثل UpdateDeliveryFee() وPrintShipment().
D) Inheritance reduces code duplication and makes the program easier to maintain.
*/

/*
using System;

public struct DeliveryAddress
{
    public string City;
    public string Street;
    public int BuildingNumber;

    public DeliveryAddress(string city, string street, int buildingNumber)
    {
        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
    }

    public string GetFullAddress()
    {
        return $"{Street}, Building {BuildingNumber}, {City}";
    }

    public override string ToString()
    {
        return GetFullAddress();
    }
}

public class Shipment
{
    private string trackingCode;
    private string description;
    private decimal weight;
    private decimal deliveryFee;

    public DeliveryAddress Destination { get; set; }

    public string TrackingCode
    {
        get
        {
            return trackingCode;
        }
        private set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                trackingCode = value;
            }
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                description = value;
            }
        }
    }

    public decimal Weight
    {
        get
        {
            return weight;
        }
        set
        {
            if (value > 0)
            {
                weight = value;
            }
        }
    }

    public decimal DeliveryFee
    {
        get
        {
            return deliveryFee;
        }
        private set
        {
            if (value > 0)
            {
                deliveryFee = value;
            }
        }
    }

    public virtual decimal EstimatedCost
    {
        get
        {
            return DeliveryFee + (Weight * 5);
        }
    }

    public Shipment(string trackingCode)
    {
        TrackingCode = trackingCode;
        Description = "Unknown";
        Weight = 1;
        DeliveryFee = 50;
        Destination = new DeliveryAddress();
    }

    public Shipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination)
    {
        TrackingCode = trackingCode;
        Description = description;
        Weight = weight;
        DeliveryFee = deliveryFee;
        Destination = destination;
    }

    public void UpdateDeliveryFee(decimal newFee)
    {
        if (newFee > 0)
        {
            DeliveryFee = newFee;
        }
    }

    public virtual void PrintShipment()
    {
        Console.WriteLine($"Tracking Code: {TrackingCode}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Weight: {Weight}");
        Console.WriteLine($"Delivery Fee: {DeliveryFee}");
        Console.WriteLine($"Destination: {Destination.GetFullAddress()}");
        Console.WriteLine($"Estimated Cost: {EstimatedCost}");
    }
}

public class StandardShipment : Shipment
{
    public StandardShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination)
        : base(
            trackingCode,
            description,
            weight,
            deliveryFee,
            destination)
    {
    }
}

public class ExpressShipment : Shipment
{
    private decimal extraFee;

    public decimal ExtraFee
    {
        get
        {
            return extraFee;
        }
        set
        {
            if (value >= 0)
            {
                extraFee = value;
            }
        }
    }

    public override decimal EstimatedCost
    {
        get
        {
            return DeliveryFee + (Weight * 5) + ExtraFee;
        }
    }

    public ExpressShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination,
        decimal extraFee)
        : base(
            trackingCode,
            description,
            weight,
            deliveryFee,
            destination)
    {
        ExtraFee = extraFee;
    }

    public override void PrintShipment()
    {
        base.PrintShipment();
        Console.WriteLine($"Extra Fee: {ExtraFee}");
    }
}

public class InternationalShipment : Shipment
{
    private string destinationCountry;
    private decimal customsFee;

    public string DestinationCountry
    {
        get
        {
            return destinationCountry;
        }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                destinationCountry = value;
            }
        }
    }

    public decimal CustomsFee
    {
        get
        {
            return customsFee;
        }
        set
        {
            if (value >= 0)
            {
                customsFee = value;
            }
        }
    }

    public override decimal EstimatedCost
    {
        get
        {
            return DeliveryFee + (Weight * 5) + CustomsFee;
        }
    }

    public InternationalShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination,
        string destinationCountry,
        decimal customsFee)
        : base(
            trackingCode,
            description,
            weight,
            deliveryFee,
            destination)
    {
        DestinationCountry = destinationCountry;
        CustomsFee = customsFee;
    }

    public override void PrintShipment()
    {
        base.PrintShipment();
        Console.WriteLine($"Destination Country: {DestinationCountry}");
        Console.WriteLine($"Customs Fee: {CustomsFee}");
    }
}

public class DeliveryCenter
{
    public string CenterName { get; set; }

    private Shipment[] shipments;
    private int shipmentCount;

    public DeliveryCenter(string centerName)
    {
        CenterName = centerName;
        shipments = new Shipment[20];
        shipmentCount = 0;
    }

    public Shipment this[int index]
    {
        get
        {
            if (index >= 0 && index < shipmentCount)
            {
                return shipments[index];
            }

            return default;
        }
        set
        {
            if (index >= 0 && index < shipmentCount)
            {
                shipments[index] = value;
            }
        }
    }

    public Shipment this[string trackingCode]
    {
        get
        {
            for (int i = 0; i < shipmentCount; i++)
            {
                if (shipments[i].TrackingCode == trackingCode)
                {
                    return shipments[i];
                }
            }

            return default;
        }
    }

    public bool AddShipment(Shipment shipment)
    {
        if (shipmentCount >= 20)
        {
            return false;
        }

        shipments[shipmentCount] = shipment;
        shipmentCount++;

        return true;
    }

    public bool RemoveShipment(string trackingCode)
    {
        for (int i = 0; i < shipmentCount; i++)
        {
            if (shipments[i].TrackingCode == trackingCode)
            {
                for (int j = i; j < shipmentCount - 1; j++)
                {
                    shipments[j] = shipments[j + 1];
                }

                shipments[shipmentCount - 1] = null;
                shipmentCount--;

                return true;
            }
        }

        return false;
    }

    public void PrintAllShipments()
    {
        Console.WriteLine();
        Console.WriteLine($"Center Name: {CenterName}");
        Console.WriteLine("==============================");

        for (int i = 0; i < shipmentCount; i++)
        {
            Console.WriteLine();
            Console.WriteLine($"Shipment #{i + 1}");
            Console.WriteLine("------------------------------");
            shipments[i].PrintShipment();
        }
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter Center Name: ");
        string centerName = Console.ReadLine();

        DeliveryCenter center = new DeliveryCenter(centerName);

        Console.WriteLine();
        Console.WriteLine("===== Standard Shipment =====");

        Console.Write("Tracking Code: ");
        string tracking1 = Console.ReadLine();

        Console.Write("Description: ");
        string description1 = Console.ReadLine();

        Console.Write("Weight: ");
        decimal weight1 = decimal.Parse(Console.ReadLine());

        Console.Write("Delivery Fee: ");
        decimal fee1 = decimal.Parse(Console.ReadLine());

        Console.Write("City: ");
        string city1 = Console.ReadLine();

        Console.Write("Street: ");
        string street1 = Console.ReadLine();

        Console.Write("Building Number: ");
        int building1 = int.Parse(Console.ReadLine());

        DeliveryAddress address1 =
            new DeliveryAddress(city1, street1, building1);

        StandardShipment standard =
            new StandardShipment(
                tracking1,
                description1,
                weight1,
                fee1,
                address1);

        Console.WriteLine();
        Console.WriteLine("===== Express Shipment =====");

        Console.Write("Tracking Code: ");
        string tracking2 = Console.ReadLine();

        Console.Write("Description: ");
        string description2 = Console.ReadLine();

        Console.Write("Weight: ");
        decimal weight2 = decimal.Parse(Console.ReadLine());

        Console.Write("Delivery Fee: ");
        decimal fee2 = decimal.Parse(Console.ReadLine());

        Console.Write("City: ");
        string city2 = Console.ReadLine();

        Console.Write("Street: ");
        string street2 = Console.ReadLine();

        Console.Write("Building Number: ");
        int building2 = int.Parse(Console.ReadLine());

        Console.Write("Extra Fee: ");
        decimal extraFee = decimal.Parse(Console.ReadLine());

        DeliveryAddress address2 =
            new DeliveryAddress(city2, street2, building2);

        ExpressShipment express =
            new ExpressShipment(
                tracking2,
                description2,
                weight2,
                fee2,
                address2,
                extraFee);

        Console.WriteLine();
        Console.WriteLine("===== International Shipment =====");

        Console.Write("Tracking Code: ");
        string tracking3 = Console.ReadLine();

        Console.Write("Description: ");
        string description3 = Console.ReadLine();

        Console.Write("Weight: ");
        decimal weight3 = decimal.Parse(Console.ReadLine());

        Console.Write("Delivery Fee: ");
        decimal fee3 = decimal.Parse(Console.ReadLine());

        Console.Write("City: ");
        string city3 = Console.ReadLine();

        Console.Write("Street: ");
        string street3 = Console.ReadLine();

        Console.Write("Building Number: ");
        int building3 = int.Parse(Console.ReadLine());

        Console.Write("Destination Country: ");
        string country = Console.ReadLine();

        Console.Write("Customs Fee: ");
        decimal customsFee = decimal.Parse(Console.ReadLine());

        DeliveryAddress address3 =
            new DeliveryAddress(city3, street3, building3);

        InternationalShipment international =
            new InternationalShipment(
                tracking3,
                description3,
                weight3,
                fee3,
                address3,
                country,
                customsFee);

        center.AddShipment(standard);
        center.AddShipment(express);
        center.AddShipment(international);

        Console.WriteLine();
        Console.WriteLine("===== All Shipments =====");

        center.PrintAllShipments();

        Console.WriteLine();
        Console.Write("Enter Tracking Code to Search: ");
        string searchCode = Console.ReadLine();

        Shipment found = center[searchCode];

        if (found != null)
        {
            Console.WriteLine();
            Console.WriteLine("Shipment Found:");
            Console.WriteLine("------------------------------");

            found.PrintShipment();
        }
        else
        {
            Console.WriteLine("Shipment not found.");
        }

        Console.WriteLine();
        Console.WriteLine("===== Integer Indexer =====");

        for (int i = 0; i < 3; i++)
        {
            Shipment shipment = center[i];

            if (shipment != null)
            {
                Console.WriteLine();
                Console.WriteLine($"Shipment at index {i}:");

                shipment.PrintShipment();
            }
        }

        Console.WriteLine();

        Console.Write("Enter Tracking Code to Remove: ");
        string removeCode = Console.ReadLine();

        bool removed = center.RemoveShipment(removeCode);

        if (removed)
        {
            Console.WriteLine("Shipment removed successfully.");
        }
        else
        {
            Console.WriteLine("Shipment not found.");
        }

        Console.WriteLine();
        Console.WriteLine("===== Remaining Shipments =====");

        center.PrintAllShipments();

        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
*/


