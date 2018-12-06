using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restart : MonoBehaviour {

    [SerializeField]
    bool_var meno;

    [SerializeField]
    client_var client;

    [SerializeField]
    event_object end_game;

    public void OnEnable()
    { 
        end_game.e.AddListener(() => Invoke("go",3));
    }

    public void go()
    {
        client.val.End_Game();
        foreach(Transform t in GetComponentInParent<Canvas>().transform)
        {
            t.gameObject.SetActive(false);
        }
        meno.val = true;
        print("hi");
        SceneManager.LoadScene(1);
    }
}
