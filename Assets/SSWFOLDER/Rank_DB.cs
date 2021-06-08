using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Firebase;
using Firebase.Database;
using Firebase.Unity;
using TMPro;
public class Rank_DB : MonoBehaviour
{
    
    DatabaseReference reference;
    int count = 1;
    int DB_insert= 0;


     string[] Rank = new string[100];
    float[] Score = new float[0];
    float[] Score23 = new float[1] { 1.0f};
    
   // float[] Score;
    int[] Rank1 = new int[0];
    public GameObject[] score_text = new GameObject[5];
    public GameObject[] user_text = new GameObject[5];

    public static string[] score_bd = new string[5];
    public static string[] name_db = new string[5];

    // int[] Rank1;
   // public TextMeshProUGUI score_test;


    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        //score_test = score_text[].GetComponent<TextMeshProUGUI>(); 
        //score_text[2].GetComponent<TextMeshProUGUI>().text = Score23[0].ToString();
        
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
        Debug.Log("데이터저장");
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
        Debug.Log("데이터불러오가");
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
                foreach (DataSnapshot  data in snapshot.Children)
                {
                    IDictionary presonInfo = (IDictionary)data.Value;

                    System.Array.Resize(ref Score, Score.Length + 1);
                    System.Array.Resize(ref Rank1, Rank1.Length + 1);

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
                
                for (int i = 0; i < Rank1.Length; i++)
                {
                    index[Rank1[i] - 1] = i;
                    
                }
                
                for (int i = 0; i < Rank1.Length; i++)
                {
                    int t = index[i];
                    Debug.Log(Rank1[t] + "등" + name[t]+ "님" +"클리어 시간: " + Score[t]);
                    score_bd[i] = Score[t].ToString();
                    name_db[i] = name[t].ToString();
                    Debug.Log(score_bd[i]);

                    //score_text[t].GetComponent<TextMeshProUGUI>().text = Score[t].ToString();
                    // user_text[i].GetComponent<Text>().text = name[t].ToString();

                }
                
            }         
       
        });
        
    }


}
