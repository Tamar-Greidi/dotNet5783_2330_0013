﻿namespace DalApi
{  
   /* Throwing an error message for exceptions*/
    
    public class ObjectNotFound : Exception          
    {
        public override string Message => "Object not found";
    }
    public class ObjectAlreadyExists : Exception
    {
        public override string Message => "Object already exists";
    }
    public class ArrayIsFull : Exception
    {
        public override string Message => "The array is full";
    }
    public class Null : Exception
    {
        public override string Message => "Null";
    }

}