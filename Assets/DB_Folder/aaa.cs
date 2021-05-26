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
        public int num;

        public User(string username,int num, string email)
        {
            this.username = username;
            this.num = num;
            this.email = email;
        }
    }
    int count = 1;



    private void Awake()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void OnClickSave()
    {
        writeNewUser("positon", "a33333", "a22222", count);
        Debug.Log("das");
        count++;
    }
    public void Loadbtn()
    {
        readUser("positon");
    }

    private void writeNewUser(string userId, string name, string email,int count)
    {

        User user = new User(name, count, email);
        string json = JsonUtility.ToJson(user);
        //databaseReference.Child(userId).SetRawJsonValueAsync(json);
        databaseReference.Child(userId).Child("num" + count.ToString()).SetRawJsonValueAsync(json);

    }

    private void readUser(string userId) //userId를 통해 검색
    {
        //databaseReference의 자식 userId를 task로 받음
        FirebaseDatabase.DefaultInstance.GetReference(userId).GetValueAsync().ContinueWith(task =>
      //databaseReference.Child(userId).GetValueAsync().ContinueWith(task =>
      {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("에러");
            }
            //성공적으로 데이터를 가져왔으면
            else if (task.IsCompleted)
            {

                //DataSnapshot 변수로 task의 결과 받음
                //데이터 데이터를 출력하고자 할때는 Snapshot 객체 사용함
                DataSnapshot snapshot = task.Result;
                // snapshot의 자식 갯수 확인
                Debug.Log(snapshot.ChildrenCount);
                

                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary presonInfo = (IDictionary)data.Value;
                    Debug.Log("email:" + presonInfo["email"] + "username:" + presonInfo["username"]
                        + "num:" + presonInfo["num"]);

                    
                    
                }

            }
        });
    }
    



}
