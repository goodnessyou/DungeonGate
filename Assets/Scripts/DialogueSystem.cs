using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;



public delegate void action();
public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueWindow;
    public GameObject answers;
    public TMP_Text message;
    public TMP_Text answer;



    Dictionary<string, action> actions =  new Dictionary<string, action>();

    CDialog dialogue  = new CDialog();

    public void loadDialogue(Object xmlFile)
    {

        
        string prefabPath = "Dialogs/" + xmlFile.name;


        dialogue.Clear();
        actions.Clear();
        actions.Add("EndDialog", dialogueEnd);
        actions.Add("none", null);
        actions.Add("OpenDoor", null);
        actions.Add("CloseDoor", null);
        actions.Add("checkStrength", null);
        actions.Add("checkIntelligence", null);
        actions.Add("AddStrength", null);
        actions.Add("AddIntelligence", null);



        
        //TextAsset textAsset = (TextAsset)Resources.Load("DoorDialog");
        TextAsset textAsset = (TextAsset)Resources.Load(xmlFile.name, typeof(TextAsset));
        //Debug.Log(textAsset.text);
        //debug.text = textAsset.text;

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);



        XmlNode messages = xmlDoc.SelectSingleNode("//messages");
        XmlNodeList messageNodes = xmlDoc.SelectNodes("//messages/message");

        foreach (XmlNode messageNode in messageNodes) 
        {
            CMessage msg = new CMessage();
            msg.text = messageNode.ChildNodes[0].InnerText;
            msg.msgID = long.Parse(messageNode.Attributes["uid"].Value);
            dialogue.loadMessage(msg);

            foreach(XmlNode answerNode in messageNode.ChildNodes[1].ChildNodes)
            {
                CAnswer answ = new CAnswer();
                answ.answID = long.Parse(answerNode.Attributes["auid"].Value);
                answ.msgID = long.Parse(answerNode.Attributes["muid"].Value);
                answ.action = answerNode.Attributes["action"].Value;
                answ.text = answerNode.InnerText;
                dialogue.loadAnswer(answ);

            }
        }

        showMessage(dialogue.getMessages()[0].msgID, "none");
        dialogueWindow.SetActive(true);

    }

    public void showMessage(long uid, string act)
    {
        actions[act]?.Invoke();
        if (uid == -1) return;

        foreach(Transform child in answers.transform) 
        {
            Destroy(child.gameObject);
        }

        message.text = dialogue.selectMessage(uid);

        foreach(CAnswer ans in dialogue.getAnswers())
        {
            TMP_Text txt = Instantiate<TMP_Text>(answer);
            txt.text = ans.text;

            txt.GetComponent<Button>().onClick.AddListener(delegate { showMessage(ans.msgID, ans.action); });
            txt.transform.SetParent(answers.transform);
        }
    }

    public void dialogueEnd()
    {
        dialogueWindow.SetActive(false);
    }

    public void setAction(string name, action act)
    {
        actions[name] = act;
    }
    
}



public class CAnswer
{
    public long answID = -1;
    public string text = "";
    public long msgID = -1;
    public string action = "";
}
public class CMessage
{
    public long msgID = -1;
    public string text = "";
    public List<CAnswer> answers = new List<CAnswer>();
}

public class CDialog
{
    List<CMessage> messages = new List<CMessage>();
    long UID = 0;
    CMessage selectedMessage = null;
    CAnswer selectedAnswer = null;

    private long getUID()
    {
        UID++;
        return UID;
    }
    CMessage findMsg(long msgID)
    {
        return messages.Find(i => i.msgID == msgID);
    }
    CAnswer findAnsw(long answID)
    {
        return selectedMessage.answers.Find(i => i.answID == answID);
    }



    public string selectMessage(long msgID)
    {
        selectedMessage = findMsg(msgID);
        return selectedMessage.text;
    }
    public string selectAnswer(long msgID, long answID)
    {
        selectMessage(msgID);
        selectedAnswer = findAnsw(answID);

        return selectedAnswer.text + "[action:" + selectedAnswer.action + "]";
    }

    public List<CMessage> getMessages()
    {
        return messages;
    }
    public long linkedUID()///
    {
        return selectedAnswer.msgID;
    }
    public void Clear()////
    {
        UID = 0;
        messages.Clear();
    }
    public void loadMessage(CMessage msg)
    {
        messages.Add(msg);
        selectedMessage = msg;
    }
    public void loadAnswer(CAnswer answ)
    {
        selectedMessage.answers.Add(answ);
    }
    
    public List<CAnswer> getAnswers() 
    {
        return selectedMessage.answers;
    }
}