using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class changeCameraScript : MonoBehaviour
{
    public List<Camera> Cameras;
    private VisualElement frame;
    private Button button;
    private int click = 0;

    void Start(){
        EnableCamera(0);
        click += 1;
    }
    void OnEnable(){
        var uiDocument = GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;
        frame = rootVisualElement.Q<VisualElement>("Frame");
        button = frame.Q<Button>("Button");
        button.RegisterCallback<ClickEvent>(ev => ChangeCamera());
    }


    private void ChangeCamera(){
        Debug.Log(click);
        EnableCamera(click);
        click += 1;
        if(click == 3){
            click = 0;
        }
    }

    private void EnableCamera(int index){
        Cameras.ForEach(cam => cam.gameObject.SetActive(false));
        Cameras[index].gameObject.SetActive(true);
    }
}
