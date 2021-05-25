using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class aaa : MonoBehaviour
{
    DatabaseReference databaseReference;
    public class User
    {
        public string username;
        public string email;

        public User(string username, string email)
        {
            this.username = username;
            this.email = email;
        }
    }



    private void Awake()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void OnClickSave()
    {
        writeNewUser("positon", "a33333", "a22222");
        Debug.Log("das");
    }

    private void writeNewUser(string userId, string name, string email)
    {

        User user = new User(name, email);
        string json = JsonUtility.ToJson(user);
        Debug.Log(user);
        Debug.Log(json);
        databaseReference.Child(userId).SetRawJsonValueAsync(json);
        Debug.Log(databaseReference);
    }
}
