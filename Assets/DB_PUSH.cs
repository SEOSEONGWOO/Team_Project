using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class DB_PUSH : MonoBehaviour
{
    public class Test
    {
        public string username;
        public string email;
        public Test(string username, string email)
        {
            this.username = username;
            this.email = email;
        }
    }
    DatabaseReference reference;
    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void OnClickSave()
    {
        writeNewUser("aa", "ss", "dsad");
    }

    private void writeNewUser(string userId, string name, string email)
    {
        Test test = new Test(name, email);
        string json = JsonUtility.ToJson(test);
        reference.Child(userId).SetRawJsonValueAsync(json);
    }
}
