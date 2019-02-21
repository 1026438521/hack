using UnityEngine;
using System.Collections;

// public abstract class Base where T : new()
// // public sealed class Singleton  
// {  
//    static Singleton instance = null;
//    public static Singleton Instance  
//    {  
//        get  
//        {  
//            if (instance == null)  
//            {  
//                instance = new Singleton();  
//            }  
//            return instance;  
//        }  
//    }
// }  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CompanionArray = System.Collections.Generic.Dictionary<string,UnityEngine.Transform>;

public abstract class Singleton<T> where T : new()
{
    public CompanionArray companions;
    
    private static T instance;

    public static T Instance
    {
        get
        {
            if(null == instance)
                instance = new T();
                
            return instance;
        }
    }   
    
    protected Singleton()
    {
        companions = new CompanionArray();
    }
}

public class SelectedMembers : Singleton<SelectedMembers>
{
    public SelectedMembers():base(){}
}





