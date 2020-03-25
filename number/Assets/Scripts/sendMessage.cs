using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class sendMessage : MonoBehaviour
{
    static SocketIOComponent socket;

    public InputField number;
    public InputField name;
    public Text statusBar;
    private bool gameStart = false;
    private bool moreThan = false;
    private bool lessThan = false;
    private bool win = false;
    private bool haveWin = false;


    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("haveWin", OnhaveWin);
        socket.On("open", OnConnected);
        socket.On("userWin", OnWin);
        socket.On("moreThan", OnNumberMoreLuckeyNumber);
        socket.On("lessThan", OnNumberLessLuckeyNumber);
        
        gameStart = true;
    //socket.On("message.sent", OnListerMessage);
}

    /*private void OnListerMessage(SocketIOEvent obj)
    {
        string msg = obj.data["message"].str;
        Debug.Log(data);
        
    }*/

    private void OnConnected(SocketIOEvent obj)
    {
        Debug.Log("conected");
    }

    public void send()
    {
       
        JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
        jSONObject.AddField("name", name.text);
        jSONObject.AddField("number", number.text);
        socket.Emit("msg.send",jSONObject);
        Debug.Log("send"+jSONObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            statusBar.text = "Plase type your name and your number";
            if (moreThan == true)
            {
                statusBar.text = "is more than number please try again";
            }
            else if (lessThan == true)
            {
                statusBar.text = "is less than number please try again";
            }
            else if (win == true)
            {
                statusBar.text = "You Win !";

            }
            else if (haveWin == true) 
            {
                statusBar.text = "Have winner";
            }
        }
        
    }

    void OnWin(SocketIOEvent obj)
    {
        Debug.Log("You win");
        win = true;
        lessThan = false;
        moreThan = false;
        socket.Emit("resetnumber");
        Debug.Log("number has reset");
    }

    void OnNumberMoreLuckeyNumber(SocketIOEvent obj)
    {
        Debug.Log("is more than number");
        lessThan = false;
        moreThan = true;
        win = false;
    }

    void OnNumberLessLuckeyNumber(SocketIOEvent obj)
    {
        Debug.Log("is less than number");
        lessThan = true;
        moreThan = false;
        win = false;
    }

    void OnhaveWin(SocketIOEvent obj)
    {
        Debug.Log("Have Winner");
        haveWin = true;
        lessThan = false;
        moreThan = false;
        win = false;
    }
}
