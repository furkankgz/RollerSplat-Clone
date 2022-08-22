using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] _Grounds;
    public float _GroundNumbers;
    private int _CurrentLevel;

    void Start()
    {
        _Grounds = GameObject.FindGameObjectsWithTag("Ground");
        _CurrentLevel = SceneManager.GetActiveScene().buildIndex;
    }
    
    void Update()
    {
        _GroundNumbers = _Grounds.Length;
    }

    public void LevelUpdate()
    {
        SceneManager.LoadScene(_CurrentLevel + 1);
    }
}
