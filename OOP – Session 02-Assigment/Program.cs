


namespace OOP02_SmartDelivery_Classes
{

    #region PART 01: THEORETICAL QUESTIONS

    #region  Question 1
    /*
     Question 1:
    a) What is the difference between a class and a struct?
       - Class is a Reference Type(stored in the Heap), Struct is a Value Type(stored in the Stack).
       - Class supports Inheritance, Struct does not.
       - Class variables hold pointers to memory, so copying a class copies the pointer.Structs copy the entire data.

    b) Why are classes more suitable than structs for large applications?
       - Because copying large structs continuously between methods fills up the Stack and slows down performance.
         Classes pass pointers, which is extremely efficient.Classes also support polymorphism and inheritance
         which are the backbone of large software architectures. 
    */
    #endregion

    #region  Question 2
    /*
    Question 2 (Based on Inheritance context):
    a) Which class is the parent class? 
       - Shipment
    b) Which class is the child class? 
       - ExpressShipment 
    c) What members are inherited by ExpressShipment?
       - All the public and protected members of Shipment ( TrackingCode ) 
    d) Why is inheritance better than duplicating the same code in multiple classes?
       - DRY Principle (Don't Repeat Yourself). It centralizes the core logic. 
       - Easier maintenance: A bug fixed in the parent class is fixed everywhere.
       - Enables Polymorphism: We can store all child types in a single Shipment[] array.
    */
    #endregion

    #endregion



    #region PART 02: Practical QUESTIONS

    #region Shipment Class
    public class Shipment
    {

        #region Fields And Properties
        private string trackingCode;
        private string description;
        private decimal weight; // Changed to decimal based on PDF properties table
        private decimal deliveryFee;
        public DeliveryAddress Destination { get; set; }

       
        public string TrackingCode
        {
            get { return trackingCode; }
            private set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    trackingCode = value;
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    description = value;
            }
        }

        public decimal Weight
        {
            get { return weight; }
            set
            {
                if (value > 0)
                    weight = value;
            }
        }

        public decimal DeliveryFee
        {
            get { return deliveryFee; }
            private set
            {
                if (value > 0)
                    deliveryFee = value;
            }
        }

        // Virtual property to allow overriding in derived classes
        public virtual decimal EstimatedCost
        {
            get { return DeliveryFee + (Weight * 5m); }
        }

        #endregion

        #region Constructors
        // Constructor 1
        public Shipment(string trackingCode)
        {
            this.trackingCode = string.Empty;
            this.description = "Unknown";
            this.weight = 1m;
            this.deliveryFee = 50m;
            this.Destination = new DeliveryAddress("Unknown City", "Unknown Street", 0);

            this.TrackingCode = trackingCode;
        }

        // Constructor 2
        public Shipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress destination)
        {
            this.trackingCode = string.Empty;
            this.description = "Unknown";
            this.weight = 1m;
            this.deliveryFee = 50m;
            this.Destination = destination;

            this.TrackingCode = trackingCode;
            this.Description = description;
            this.Weight = weight;
            this.DeliveryFee = deliveryFee;
        }

        #endregion

        #region Methods
        public void UpdateDeliveryFee(decimal newFee)
        {
            if (newFee > 0)
            {
                this.DeliveryFee = newFee;
            }
        }

        public virtual void PrintShipment()
        {
            Console.WriteLine($"Tracking Code: {TrackingCode}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Weight: {Weight} KG");
            Console.WriteLine($"Delivery Fee: {DeliveryFee} EGP");
            Console.WriteLine($"Destination: {Destination.GetFullAddress()}");
            Console.WriteLine($"Estimated Cost: {EstimatedCost} EGP");
        }
        #endregion
    }
    #endregion

    #region DeliveryAddress Struct
    // 1. DeliveryAddress Struct (Kept as Struct because it's a value object)
    public struct DeliveryAddress
    {

        #region Fields And Properties
        public string City;
        public string Street;
        public int BuildingNumber;
        #endregion

        #region Constructors
        public DeliveryAddress(string city, string street, int buildingNumber)
        {
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
        }
        #endregion

        #region Methods
        public string GetFullAddress()
        {
            return $"{BuildingNumber} {Street}, {City}";
        }
        #endregion
    }

    #endregion

    #region Derived Classes

    #region StandardShipment

    public class StandardShipment : Shipment
    {

        #region Constructor
        // Constructor chaining: passing values to the base class constructor
        public StandardShipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress destination)
            : base(trackingCode, description, weight, deliveryFee, destination)
        {
        }
        #endregion
    }


    #endregion

    #region ExpressShipment
    public class ExpressShipment : Shipment
    {

        #region Fields And Properties
        private decimal extraFee;

        public decimal ExtraFee
        {
            get { return extraFee; }
            set
            {
                if (value >= 0)
                    extraFee = value;
            }
        }


        // Override the EstimatedCost
        public override decimal EstimatedCost
        {
            get { return base.EstimatedCost + ExtraFee; }
        }

        #endregion

        #region Constructors
        public ExpressShipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress destination, decimal extraFee)
            : base(trackingCode, description, weight, deliveryFee, destination)
        {
            ExtraFee = extraFee;
        }
        #endregion

        #region Methods
    
        #endregion
    }

    #endregion

    #region InternationalShipment
    public class InternationalShipment : Shipment
    {

        #region Fields And Properties
        private string destinationCountry;
        private decimal customsFee;

        public string DestinationCountry
        {
            get { return destinationCountry; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    destinationCountry = value;
            }
        }

        public decimal CustomsFee
        {
            get { return customsFee; }
            set
            {
                if (value >= 0)
                    customsFee = value;
            }
        }

        public override decimal EstimatedCost
        {
            get { return base.DeliveryFee + CustomsFee; }
        }

        #endregion

        #region Constructors
        public InternationalShipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress destination, string destinationCountry, decimal customsFee)
               : base(trackingCode, description, weight, deliveryFee, destination)
        {
            DestinationCountry = destinationCountry;
            CustomsFee = customsFee;
        }

        #endregion

        #region Methods

        #endregion
    }

    #endregion



    #endregion

    #region DeliveryCenter Class
    public class DeliveryCenter
    {

        #region Fields And Properties

        public string CenterName { get; set; }

        // Private array for maximum 20 shipments
        private Shipment[] shipments = new Shipment[20];



        #endregion

        #region Constructors
        public DeliveryCenter(string centerName)
        {
            CenterName = centerName;
        }
        #endregion

        #region Indexers


        #region Integer indexer
        public Shipment this[int index]
        {
            get
            {
                if (index >= 0 && index < shipments.Length)
                    return shipments[index];
                return null; // returning null instead of default because it's a class
            }
            set
            {
                if (index >= 0 && index < shipments.Length)
                    shipments[index] = value;
            }
        }
        #endregion

        #region String indexer (search by tracking code)
        public Shipment this[string trackingCode]
        {
            get
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    if (shipments[i] != null && shipments[i].TrackingCode == trackingCode)
                        return shipments[i];
                }
                return null;
            }
        }

        #endregion


        #endregion

        #region Methods
        public bool AddShipment(Shipment shipment)
        {
            if (shipment == null) return false;

            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i] == null)
                {
                    shipments[i] = shipment;
                    return true;
                }
            }
            return false; // Delivery center is full
        }

        public bool RemoveShipment(string trackingCode)
        {
            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i] != null && shipments[i].TrackingCode == trackingCode)
                {
                    shipments[i] = null; // Removing it by unlinking the object (GC will collect it)
                    return true;
                }
            }
            return false;
        }

        public void PrintAllShipments()
        {
            Console.WriteLine($"\n=== {CenterName} Delivery Center Shipments ===");
            bool empty = true;
            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i] != null)
                {
                    shipments[i].PrintShipment();
                    empty = false;
                }
            }
            if (empty)
                Console.WriteLine("No shipments found.");
        }

        #endregion

    }

    #endregion


    #endregion




    class Program
    {

        static void Main(string[] args)
        {


            
        }

    }



}