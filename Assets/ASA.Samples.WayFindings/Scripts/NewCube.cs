
using Com.Reseul.ASA.Samples.WayFindings.UX.Effects;
using Com.Reseul.ASA.Samples.WayFindings.UX.KeyboardHelpers;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class NewCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateNewGameobject() 
    {
        Debug.Log("new Cube called");
        GameObject go1 = new GameObject();
        go1.name = "go1";
        go1.AddComponent<Rigidbody>();
        go1.AddComponent<ManipulationHandler>();
    }

}
