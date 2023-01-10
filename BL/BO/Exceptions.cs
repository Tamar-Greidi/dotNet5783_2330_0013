namespace BO
{
    public class DalException : Exception
    {
        public DalException(Exception ex) : base("Exception from dal:", ex) { }
    }
    public class ObjectAlreadyExists : Exception
    {
        public override string Message => "Object already exists";
    }
    public class ArrayIsFull : Exception
    {
        public override string Message => "The array is full";
    }
    public class OutOfStock : Exception
    {
        public override string Message => "Out of stock";
    }
    public class InvalidData : Exception
    {
        public override string Message => "Invalid data";
    }
    public class ObjectNotFound : Exception
    {
        public override string Message => "Object not found";
    }
    public class OrderAlreadyShipped : Exception
    {
        public override string Message => "Order already shipped";
    }
    public class OrderNotShippedYet : Exception
    {
        public override string Message => "Order has not been shipped yet";
    }
    public class OrderAlreadyDelivered : Exception
    {
        public override string Message => "Order already delivered";
    }
    public class IncorrectDate : Exception
    {
        public override string Message => "Incorrect date";
    }
    public class Null : Exception
    {
        public override string Message => "Null";
    }
}