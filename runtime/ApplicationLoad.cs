using UnityEngine;

public class ApplicationLoad : MonoBehaviour {

    private void Awake()
    {

        Application.targetFrameRate = 60;
        
        //Come back to this when we have the standard unit

        //Constants.STANDARD_UNIT = Instantiate(Resources.Load("Prefabs/LevelBlocks/block 1"), new Vector3, Quaternion.identity).GetComponent<Renderer>().bounds.size;





    }
}
