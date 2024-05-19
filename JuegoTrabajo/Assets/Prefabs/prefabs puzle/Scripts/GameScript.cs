using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [SerializeField]private Transform emptySpace = null;
    private Camera _camera;
    [SerializeField] private TileScript[] tiles;
    private int emptySpaceIndex = 15;
    private bool _isFinished;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Text endPanelTimeText;

    void Start()
    {
        _camera = Camera.main;
        //Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0)){
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit){
                if(Vector2.Distance(emptySpace.position,hit.transform.position) < 2){
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    TileScript thisTile = hit.transform.GetComponent<TileScript>(); 
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }  
       } 
       if(_isFinished == false){
        int correctTiles = 0;
        foreach(var a in tiles){
            if(a != null){
                if(a.inRightPlace)
                correctTiles++;
            }
        }
        if(correctTiles == tiles.Length-1){
            _isFinished = true;
            endPanel.SetActive(true);
            var a = GetComponent<TimerScript>();
            a.StopTimer();
            endPanelTimeText.text = (a.minutes<10 ? "0":"") + a.minutes + ":" + (a.seconds<10 ? "0":"") + a.seconds;
         }
      }
       
    }
    public void EndGame(){
        SceneManager.LoadScene("TaifaLS5");
    }

    public void Shuffle(){
        if(emptySpaceIndex != 15){
            var tileOn15LastPos = tiles[15].targetPosition;
            tiles[15].targetPosition = emptySpace.position;
            emptySpace.position = tileOn15LastPos;
            tiles[emptySpaceIndex] = tiles[15];
            tiles[15] = null;
            emptySpaceIndex = 15;
        }
        int invertion;
        do{
            for(int i = 0; i<=14; i++){
                var lastPos = tiles[i].targetPosition;
                int randomIndex = Random.Range(0, 14);
                tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                tiles[randomIndex].targetPosition = lastPos; 
                var tile = tiles[i];
                tiles[i] = tiles[randomIndex];
                tiles[randomIndex] = tile;
        }  
        invertion = GetInversions();
        Debug.Log("");      
        }while(invertion%2 != 0);
        }

        int GetInversions(){
            int InversionSum = 0;
            for(int i = 0; i<tiles.Length; i++){
                int thisTileInvertion = 0;
                for(int j = i; j<tiles.Length; j++){
                    if(tiles[j] != null){
                        if(tiles[i].number > tiles[j].number){
                            thisTileInvertion++;
                        }
                    }
                }
                InversionSum += thisTileInvertion;
            }
            return InversionSum;    
        }

        public int findIndex(TileScript ts){
            for(int i = 0;i < tiles.Length; i++){
                if(tiles[i]!=null){
                    if(tiles[i] == ts){
                        return i;
                    }
                }
            }
            return -1;
        }
}

