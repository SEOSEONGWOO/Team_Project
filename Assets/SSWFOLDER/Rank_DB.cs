using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class Rank_DB : MonoBehaviour
{
    
    DatabaseReference reference;
    int count = 1;
    public int DB_insert= 0;
     string[] Rank = new string[100];
    //float[] Score = new float[5];
    float[] Score;
    //int[] Rank1 = new int[] {1,1,1,1,1};

    int[] Rank1;



    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public class Test
    {
        public string userid;
        public float clear_time;
        public Test(string id,float email)
        {
            this.userid = id;
            this.clear_time = email;
        }
    }
    public void Rank_save()
    {

        Rank_push("Rank",Player_Scr.player_name, Player_Scr.clear_time);
        count++;
    }

    private void Rank_push(string rank, string userId, float email)
    {
        Test test = new Test(userId, email);
        string json = JsonUtility.ToJson(test);
        reference.Child(rank).Child(userId).SetRawJsonValueAsync(json);
    }

    public void Rank_load()
    {
        readUser("Rank", Player_Scr.player_name);
    }

    public void readUser(string rank, string userId)
    {
        reference.Child(rank).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("에러");
            }
            //task 성공 시
            else if (task.IsCompleted)
            {
                //DataSnapshot 변수를 선언하여 task 경과를 받음
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.ChildrenCount);

                string[] num = new string[snapshot.ChildrenCount];
                string[] name = new string[snapshot.ChildrenCount];
                int k = 0;
                Debug.Log("왔니?");
                foreach (DataSnapshot  data in snapshot.Children)
                {
                    IDictionary presonInfo = (IDictionary)data.Value;                    
                    num[k] = presonInfo["clear_time"].ToString();
                    name[k] = presonInfo["userid"].ToString();
                    Score[k] = float.Parse(num[k]);
                    k++;
                    Debug.Log("111111");
                    
                }
                //Debug.Log(Score[].);
                Debug.Log("2222222");
                for (int j = 0; j < snapshot.ChildrenCount; j++)
                {
                    Debug.Log("333333");
                    Rank[j] = num[j] + " / " + name[j] +"/" + Score[j];
                    Debug.Log(Rank[j]);    
                }
                Debug.Log("4444444");

                for (int i = 0; i < Score.Length; i++)
                {
                    Rank1[i] = 1;
                    for (int j = 0; j < Score.Length; j++)
                    {
                        if (Score[i] > Score[j])
                        {
                            Rank1[i]++;
                        }
                    }
                }
               
                
                
                int[] index = new int[Rank1.Length]; //index 에 배열 10개 지정
                Debug.Log("왔니111?");
                for (int i = 0; i < Rank1.Length; i++)
                {
                    index[Rank1[i] - 1] = i;
                    Debug.Log("왔니?2222");
                }
                Debug.Log("왔니333");
                for (int i = 0; i < Rank1.Length; i++)
                {
                    int t = index[i];
                    Debug.Log(Rank1[t] + "등" + name[t]+ "님" +"클리어 시간: " + Score[t]);
                }
                Debug.Log("왔니?44442");
            }         
       
        });
        
    }
    private void Rank_read()
    {
       

        for (int i = 0; i < Score.Length; i++)  //랭크 10명 검색
        {
            for (int j = 0; j < Score.Length; j++)
            {
                if(Score[i] < Score[j])
                {
                    Rank1[i]++; //순위 변경
                }
            }
        }


        int[] index = new int[Rank1.Length];

        for (int i = 0; i < Rank1.Length; i++)
        {
            index[Rank1[i] - 1] = i;
        }
        for (int i = 0; i < Rank1.Length; i++)
        {
            int t = index[i];
            Debug.Log(Rank1[i] + "등" +  "클리어 시간: " + Score[t]);
        }
        
        
        
    }

}
