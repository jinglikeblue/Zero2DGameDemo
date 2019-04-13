using UnityEngine;

public class Native : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void Callback(string msg)
    {
        Debug.LogFormat("收到协议：{0}", msg);
        GameNotice.nativeCallback(msg);
    }
}
