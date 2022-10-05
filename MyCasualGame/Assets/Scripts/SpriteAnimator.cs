using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]

public class SpriteAnimator : MonoBehaviour
{
    private Sprite[] _sprites = new Sprite[0];

    private SpriteRenderer _renderer = null;
    public string spriteSheetName = string.Empty;
    public string[] spriteNames = new string[0];
    public float frameRate = 0.1f;

    private float lastChangeTime = 0.0f;

    private int spriteMaxCount = 0;

    private int currentIndex = 0;

    private const string SpriteRootFolderName = "Sprites/";

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;

        _renderer = GetComponent<SpriteRenderer>();
        if (string.IsNullOrEmpty(spriteSheetName))
        {
            _sprites = new Sprite[spriteSheetName.Length];
            for(int i = 0; i < spriteSheetName.Length; i++)
            {
                _sprites[i] = Resources.Load<Sprite>(SpriteRootFolderName + spriteNames[i]);
            }
        }
        else
        {
            _sprites = Resources.LoadAll<Sprite>(SpriteRootFolderName + spriteSheetName);
        }

        if(_sprites != null && _sprites.Length > 0)
        {
            _renderer.sprite = _sprites[0];
            spriteMaxCount = _sprites.Length - 1;
        }

        lastChangeTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_sprites == null || _sprites.Length == 0)
            return;

        if (lastChangeTime + frameRate < Time.time)
        {
            lastChangeTime = Time.time;
            ChangeCurrentIndex();
            _renderer.sprite = _sprites[currentIndex];
        }
    }

    void ChangeCurrentIndex()
    {
        if(currentIndex == spriteMaxCount)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex = Mathf.Min(currentIndex + 1, spriteMaxCount);
        }
    }

}
